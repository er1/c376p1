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

namespace TowerCraft
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Test Level variable
        map testLevel = new map();
        badGuy bg1;
        tower tower1;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.graphics.PreferredBackBufferWidth = testLevel.getWidth * testLevel.getMapSize;
            this.graphics.PreferredBackBufferHeight = testLevel.getHeight * testLevel.getMapSize;
            //this.graphics.IsFullScreen = true;
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
            //Load Textures
            Texture2D grass = Content.Load<Texture2D>("MapTextures//grass_texture");
            Texture2D rocks = Content.Load<Texture2D>("MapTextures//rockTile");
            
            //Add Textures to List (order matters!)
            testLevel.AddTexture(grass);
            testLevel.AddTexture(rocks);

            Texture2D enemyTexture = Content.Load<Texture2D>("Enemy//badguy");
            bg1 = new badGuy(ref enemyTexture, new Vector2(testLevel.getMapSize / 2, testLevel.getMapSize / 2), 100, 3.0f);
            bg1.SetWaypoints(testLevel.getPathway);

            Texture2D towerTexture = Content.Load<Texture2D>("Tower//tower1");
            tower1 = new tower(ref towerTexture, (new Vector2(4.55f, 1.95f) * testLevel.getMapSize));


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
            //bg1.CurrentHealth(-1);
            bg1.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //Draw LEVEL MWHAHAAHAHAA
            spriteBatch.Begin();

            testLevel.Draw(spriteBatch);
            bg1.Draw(spriteBatch);
            tower1.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
