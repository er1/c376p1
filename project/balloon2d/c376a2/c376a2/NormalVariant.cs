using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace c376a2
{
    class NormalVariant : ThinkDraw
    {
        public static Texture2D skygrad;
        public static Texture2D cloud;

        protected Game caller;
        protected Vector2 windowSize;

        protected EntManager entities;
        protected EntPC Epc;

        protected double lastFired = 0;

        protected bool isOver
        {
            get { return (Epc.lives <= 0); }
        }

        protected bool endShown = false;

        protected bool doNextScreen = false;

        public int startballoons;
        public bool hab1 = true;
        public bool hab2 = true;
        public bool hab3 = true;

        protected List<Vector2> clouds;

        public NormalVariant()
        {
        }

        public NormalVariant(Game g, Vector2 v)
        {
            init(g, v);
        }

        public void init(Game g, Vector2 v)
        {
            caller = g;
            windowSize = v;


            entities = new EntManager();
            entities.gameWidth = ((int)windowSize.X);
            entities.gameHeight = ((int)windowSize.Y);

            Epc = new EntPC();
            Epc.position = windowSize / 2;
            Epc.resistance = 0.8f;
            Epc.font = EntPC.defaultFont;
            entities.add(Epc);

            int numsets = 7;
            for (int i = 0; i < numsets; ++i)
            {
                Vector2 p = new Vector2((float)Math.Cos(Math.PI * 2 * i / numsets), (float)Math.Sin(Math.PI * 2 * i / numsets));
                EntBalloonSet e = new EntBalloonSet(p * 256 + windowSize / 2);
                e.velocity = new Vector2(p.Y, -p.X) * 4;
                entities.add(e);
            }

            startballoons = 0;
            foreach (Ent e in entities.Ents)
            {
                if (e is EntBalloon) startballoons++;
            }

            clouds = new List<Vector2>();

            for (int i = 0; i < 20; ++i) {
                clouds.Add(new Vector2((float)Ent.rand.NextDouble(), (float)Ent.rand.NextDouble()) * (windowSize - new Vector2(cloud.Width, cloud.Height)));
            }

        }

        public virtual void think(GameTime gt)
        {
            double currentTime = gt.TotalGameTime.TotalMilliseconds;
            if (lastFired == 0)
            {
                lastFired = gt.TotalGameTime.TotalMilliseconds;
            }

            // Allows the game to exit
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                doNextScreen = true;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Epc.velocity += new Vector2(0, -2);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Epc.velocity += new Vector2(0, 2);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                Epc.velocity += new Vector2(-2, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Epc.velocity += new Vector2(2, 0);

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Length() > 0.25f)
                Epc.velocity += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left * 2;

            if ((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if ((currentTime - lastFired) > 200 && Epc.CanFire)
                {
                    lastFired = currentTime;
                    // create bullet and fire it;
                    EntBullet Ebullet = new EntBullet(Epc, Epc.direction);
                    entities.add(Ebullet);
                }
            }

            entities.thinkAll();
            entities.collisionAll();

            int numballoons = 0;
            int numsets = 0;
            int numairballoons = 0;
            foreach (Ent e in entities.Ents)
            {
                if (e is EntBalloon) numballoons++;
                if (e is EntAirBalloon) numairballoons++;
                if (e is EntBalloonSet) numsets++;
            }

            if (!endShown)
            {
                if (numballoons == 0)
                {
                    EntText e = new EntText("YOU WIN", Color.White);
                    entities.add(e);
                    endShown = true;
                }
                if (Epc.lives == 0)
                {
                    EntText e = new EntText("GAME OVER", Color.Red);
                    entities.add(e);
                    endShown = true;
                }
            }

            if (((float)numballoons / (float)startballoons <= 0.9) && hab1)
            {
                hab1 = false;
                EntAirBalloon e = new EntAirBalloon();
                entities.add(e);
            }
            if (((float)numballoons / (float)startballoons <= 0.6) && hab2)
            {
                hab2 = false;
                EntAirBalloon e = new EntAirBalloon();
                entities.add(e);
            }
            if (((float)numballoons / (float)startballoons <= 0.3) && hab3)
            {
                hab3 = false;
                EntAirBalloon e = new EntAirBalloon();
                entities.add(e);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();// (SpriteBlendMode.AlphaBlend);

            spriteBatch.Draw(skygrad, new Rectangle(0, 0, (int)windowSize.X, (int)windowSize.Y), null, Color.White);

            //foreach (Vector2 v in clouds)
            //{
            //    spriteBatch.Draw(cloud, v, new Color(1,1,1,0.2f));
            //}

            entities.drawAll(spriteBatch);

            spriteBatch.DrawString(Epc.font, "Score: " + Epc.score.ToString(), new Vector2(64, 32), Color.Red);
            spriteBatch.DrawString(Epc.font, "Lives: " + Epc.lives.ToString(), new Vector2(64, 64), Color.YellowGreen);

            spriteBatch.End();
        }

        public ThinkDraw next()
        {
            if (doNextScreen)
            {
                return new MenuScreen();
            }
            return null;
        }
    }
}
