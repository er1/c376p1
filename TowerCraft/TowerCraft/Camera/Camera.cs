using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


//XNA camera class modified from the class learned from book called Learning XNA 4.0 by Aaron Reed
// basically what this creates is a camera based a viewport that is sent to it, 
// Movememnts modified to keyboard for "1st person" type.
namespace TowerCraft3D
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        //Camera variables
        int worldSize;
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }
        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;
        private float move = 5.0f;
        private float movePitchYaw = 8.0f;
        Viewport viewport;
        float maxPitch = MathHelper.PiOver2; //90 degree
        float currentPitch = 0;
        TileCoord currentTC;
        private TimeSpan timer;
        bool moveable;


        public BoundingFrustum Frustum;

        public Camera(Game game, Vector3 newPosition, Vector3 direction, Vector3 up, Viewport NewViewport, bool move, int WorldSize)
            : base(game)
        {
            // TODO: Construct any child components here
            currentTC = new TileCoord(0,0);
            worldSize = WorldSize;
            cameraPosition = newPosition;
            cameraDirection = direction - newPosition;
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLootAt();
            viewport = NewViewport;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)viewport.Width / (float)viewport.Height, 1, 1500);
            timer = TimeSpan.FromSeconds(0.3);
            moveable = true;
            


        }
        // Create look at function
        private void CreateLootAt()
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
        //get camera direction function
        public Vector3 getCamDirection()
        {
            return cameraDirection;
        }
        //get camera position function
        public Vector3 getCamPosition()
        {
            return cameraPosition;
        }
        //get camera up function
        public Vector3 getCamUp()
        {
            return cameraUp;
        }
        public TileCoord getCurrentTC()
        {
            return currentTC;
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            

            base.Initialize();
        }
        public void setCamPosition(Vector3 pos)
        {
            cameraPosition = pos;
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
           
                // TODO: Add your update code here

            if (((Game1)Game).gameState == 1)
            {
                //Translation and Strafing
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    cameraPosition += cameraDirection * move;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    cameraPosition -= cameraDirection * move;
                }
                if (cameraPosition.X >= -200)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * move;
                    }
                    int test = (int)((gamePadState.ThumbSticks.Left.X * 3));
                    cameraPosition += new Vector3(1, 0, 0) * test;
                }
                if (cameraPosition.X <= 200)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * move;
                    }
                    int test = (int)((gamePadState.ThumbSticks.Left.X * 3));
                    cameraPosition += new Vector3(1, 0, 0) * test;
                }

                timer -= gameTime.ElapsedGameTime;
                if ((timer <= TimeSpan.Zero) && (!moveable))
                {
                    //Reset Timer
                    timer = TimeSpan.FromSeconds(0.3);
                    moveable = true;
                }

                if (((Keyboard.GetState().IsKeyDown(Keys.W)) || (gamePadState.DPad.Up == ButtonState.Pressed)) && (currentTC.y > -4) && moveable)
                {
                    currentTC.y--;
                    moveable = false;
                }
                if (((Keyboard.GetState().IsKeyDown(Keys.S)) || (gamePadState.DPad.Down == ButtonState.Pressed)) && (currentTC.y < 3) && moveable)
                {
                    currentTC.y++;
                    moveable = false;

                }
                if (((Keyboard.GetState().IsKeyDown(Keys.D)) || (gamePadState.DPad.Right == ButtonState.Pressed)) && (currentTC.x < 0) && moveable)
                {
                    currentTC.x++;
                    moveable = false;

                }
                if (((Keyboard.GetState().IsKeyDown(Keys.A)) || (gamePadState.DPad.Left == ButtonState.Pressed)) && (currentTC.x > -19) && moveable)
                {
                    currentTC.x--;
                    moveable = false;

                }


                // Yaw rotation
                if (Keyboard.GetState().IsKeyDown(Keys.J))
                {
                    cameraDirection = Vector3.Transform(cameraDirection,
                    Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) * -movePitchYaw));

                }
                if (Keyboard.GetState().IsKeyDown(Keys.L))
                {
                    cameraDirection = Vector3.Transform(cameraDirection,
                    Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) * movePitchYaw));

                }
                //Pitch rotation
                if (Keyboard.GetState().IsKeyDown(Keys.K))
                {
                    float pitchRotation = (MathHelper.PiOver4 / 180) * movePitchYaw;
                    if (Math.Abs(currentPitch + pitchRotation) < maxPitch)
                    {
                        cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), pitchRotation));
                        currentPitch += pitchRotation;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.I))
                {
                    float pitchRotation = (MathHelper.PiOver4 / 180) * -movePitchYaw;
                    if (Math.Abs(currentPitch + pitchRotation) < maxPitch)
                    {
                        cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), pitchRotation));
                        currentPitch += pitchRotation;
                    }
                }
            }
                    CreateLootAt();
                
            
            base.Update(gameTime);
        }
    }
}
