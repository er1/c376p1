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
    class StoryScreen : ThinkDraw
    {
        public static Texture2D story;
        bool lastEnterState = true;
        int timer = 0;

        public StoryScreen()
        {
        }
        public void think(GameTime gt)
        {
            timer++;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !lastEnterState)
            {
                timer = 1000000;
            }

            lastEnterState = Keyboard.GetState().IsKeyDown(Keys.Enter);
        }
        public void draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(NormalVariant.skygrad, new Rectangle(0, 0, (int)(Switcher.size.X), (int)(Switcher.size.Y)), null, Color.White);
            sb.Draw(story, new Rectangle(0, 0, (int)(Switcher.size.X), (int)(Switcher.size.Y)), null, Color.White);
            sb.End();
        }
        public ThinkDraw next()
        {
            if (timer > 2000)
            {
                NormalVariant g = new AlternateVariant();
                g.init(Switcher.game, Switcher.size);
                return g;
            }

            return null;
        }
    }
}

