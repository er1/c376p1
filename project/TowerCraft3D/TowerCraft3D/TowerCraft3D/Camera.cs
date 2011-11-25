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
        bool moveAllowed = false;
        float maxPitch = MathHelper.PiOver2; //90 degree
        float currentPitch = 0;

        public Camera(Game game, Vector3 newPosition, Vector3 direction, Vector3 up, Viewport NewViewport, bool move, int WorldSize)
            : base(game)
        {
            // TODO: Construct any child components here
            worldSize = WorldSize;
            cameraPosition = newPosition;
            cameraDirection = direction - newPosition;
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLootAt();
            viewport = NewViewport;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)viewport.Width / (float)viewport.Height, 1, 2000);
            moveAllowed = move;
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
            ////Wrap the camera around the world
            //if (cameraPosition.X >= worldSize)
            //{ cameraPosition = new Vector3(-worldSize + 1, cameraPosition.Y, cameraPosition.Z); }
            //if (cameraPosition.X <= -worldSize)
            //{ cameraPosition = new Vector3(worldSize - 1, cameraPosition.Y, cameraPosition.Z); }

            //if (cameraPosition.Y >= worldSize)
            //{ cameraPosition = new Vector3(cameraPosition.X, -worldSize + 1, cameraPosition.Z); }
            //if (cameraPosition.Y <= -worldSize)
            //{ cameraPosition = new Vector3(cameraPosition.X, worldSize - 1, cameraPosition.Z); }

            //if (cameraPosition.Z >= worldSize)
            //{ cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y, -worldSize + 1); }
            //if (cameraPosition.Z <= -worldSize)
            //{ cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y, worldSize - 1); }

            if (moveAllowed)
            {
                // TODO: Add your update code here
                //Translation and Strafing
                //if (Keyboard.GetState().IsKeyDown(Keys.W))
                //{
                //    cameraPosition += cameraDirection * move;
                //}
                //if (Keyboard.GetState().IsKeyDown(Keys.S))
                //{
                //    cameraPosition -= cameraDirection * move;
                //}
                if (cameraPosition.X >= -350)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * move;
                    }
                }
                if (cameraPosition.X <= 350)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * move;
                    }
                }
                //// Yaw rotation
                //if (Keyboard.GetState().IsKeyDown(Keys.Left))
                //{
                //    cameraDirection = Vector3.Transform(cameraDirection,
                //    Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) * -movePitchYaw));

                //}
                //if (Keyboard.GetState().IsKeyDown(Keys.Right))
                //{
                //    cameraDirection = Vector3.Transform(cameraDirection,
                //    Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) * movePitchYaw));

                //}
                ////Pitch rotation
                //if (Keyboard.GetState().IsKeyDown(Keys.Down))
                //{
                //    float pitchRotation = (MathHelper.PiOver4 / 180) * movePitchYaw;
                //    if (Math.Abs(currentPitch + pitchRotation) < maxPitch)
                //    {
                //        cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), pitchRotation));
                //        currentPitch += pitchRotation;
                //    }
                //}
                //if (Keyboard.GetState().IsKeyDown(Keys.Up))
                //{
                //    float pitchRotation = (MathHelper.PiOver4 / 180) * -movePitchYaw;
                //    if (Math.Abs(currentPitch + pitchRotation) < maxPitch)
                //    {
                //        cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), pitchRotation));
                //        currentPitch += pitchRotation;
                //    }
                //}

                CreateLootAt();
            }
            base.Update(gameTime);
        }
    }
}
