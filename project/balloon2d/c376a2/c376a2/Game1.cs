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

namespace c376a2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public int width = 1024;
        public int height = 768;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Switcher inst;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Switcher.game = this;
            Switcher.size = new Vector2(width, height);

            inst = new Switcher(new MenuScreen());
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D Tundef = Content.Load<Texture2D>("undef");
            Texture2D TPC = Content.Load<Texture2D>("PC");
            SpriteFont Ftahoma = Content.Load<SpriteFont>("tahoma");
            Texture2D Tbullet = Content.Load<Texture2D>("bullet");
            Texture2D Tballoon = Content.Load<Texture2D>("balloon");
            Texture2D Tairballoon = Content.Load<Texture2D>("airballoon");
            Texture2D Tskygrad = Content.Load<Texture2D>("skygrad");
            Texture2D Tcloud = Content.Load<Texture2D>("cloud");
            SpriteFont Fmenu = Content.Load<SpriteFont>("menufont");
            Texture2D Tstory = Content.Load<Texture2D>("story");
            SpriteFont Fend = Content.Load<SpriteFont>("endfont");
            Texture2D Tpaperplane = Content.Load<Texture2D>("paper1");
            Texture2D Tpaperball = Content.Load<Texture2D>("paper2");

            Ent.defaultSprite = Tundef;
            EntPC.defaultSprite = TPC;
            EntPC.defaultFont = Ftahoma;
            EntBullet.defaultSprite = Tbullet;
            EntBalloon.defaultSprite = Tballoon;
            EntAirBalloon.defaultSprite = Tairballoon;
            NormalVariant.skygrad = Tskygrad;
            NormalVariant.cloud = Tcloud;
            MenuScreen.font = Fmenu;
            StoryScreen.story = Tstory;
            EntText.font = Fend;
            EntPaperBall.defaultSprite = Tpaperball;
            EntPaperPlane.defaultSprite = Tpaperplane;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            inst.think(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.YellowGreen);
            inst.draw(spriteBatch);


            base.Draw(gameTime);
        }
    }
}
