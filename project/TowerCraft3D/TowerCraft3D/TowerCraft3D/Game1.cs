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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Camera cameraMain { get; protected set; }
        public Viewport MainScreen;
        private int worldWidth;
        private int worldHeight;
        ModelManager modelManager;
        public int worldSize{get;protected set;}
        public SpriteManager spriteManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
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
            //Camera component
            Components.Remove(cameraMain);
            Components.Remove(modelManager);
            Components.Remove(spriteManager);
            cameraMain = new Camera(this, new Vector3(0, 140, 95), new Vector3(0,-5,1), Vector3.Up, MainScreen, true,worldSize);
            Components.Add(cameraMain);
            
            this.IsFixedTimeStep = false;

            
            //Model Manager
            modelManager = new ModelManager(this);
            Components.Add(modelManager);
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);  
            
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}
