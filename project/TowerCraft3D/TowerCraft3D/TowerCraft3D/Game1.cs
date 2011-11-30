using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TowerCraft3D
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public ModelManager modelManager;

        public SpriteManager spriteManager { get; protected set; }
        public Camera cameraMain { get; protected set; }
        public Viewport MainScreen;
       
        public int worldWidth;
        public int worldHeight;
        public int worldSize{get;protected set;}
        public int LIFE {get;set;}

        bool started = false;
        int gameState = 1;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            //this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
        }
        protected override void Initialize()
        {
           
            LIFE = 1000;
            worldSize = 390;
            worldHeight = graphics.PreferredBackBufferHeight;
            worldWidth = graphics.PreferredBackBufferWidth;
            
            #region Viewport stuff
            MainScreen = new Viewport();
            MainScreen.X = 0;
            MainScreen.Y = 0;
            MainScreen.Width = worldWidth;
            MainScreen.Height = worldHeight;
            MainScreen.MinDepth = 0;
            MainScreen.MaxDepth = 1;
            #endregion

            Components.Remove(cameraMain);
            Components.Remove(modelManager);
            Components.Remove(spriteManager);

            cameraMain = new Camera(this, new Vector3(0, 200, 199), new Vector3(0,-5,1), Vector3.Up, MainScreen, true,worldSize);
            modelManager = new ModelManager(this);
            spriteManager = new SpriteManager(this);
            Components.Add(cameraMain);
            Components.Add(modelManager);
            Components.Add(spriteManager);

            this.IsFixedTimeStep = false;
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 1);  
            base.Initialize();

        }     
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }      
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }     
        protected override void Update(GameTime gameTime)
        {
             

            //menu
            if (gameState == 0)
            {
                if (!started)
                {
                    Components.Remove(cameraMain);
                    Components.Remove(modelManager);
                    Components.Remove(spriteManager);

                    cameraMain = new Camera(this, new Vector3(0, 200, 199), new Vector3(0, -5, 1), Vector3.Up, MainScreen, true, worldSize);
                    modelManager = new ModelManager(this);
                    spriteManager = new SpriteManager(this);

                    Components.Add(cameraMain);
                    Components.Add(modelManager);
                    Components.Add(spriteManager);
                    started = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.U))
                {
                    gameState = 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }
            }
                // game
            else if (gameState == 1)
            {
                base.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    gameState = 2;
                }
            }
                //pause
            else if (gameState == 2)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Y))
                {
                    gameState = 0;
                    started = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.I))
                {
                    gameState = 1;
                }
            }
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            if (gameState == 0)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else if (gameState == 2)
            {
                GraphicsDevice.Clear(Color.Green);
            }
            else
            {
                base.Draw(gameTime);
            }
        }

    }
}
