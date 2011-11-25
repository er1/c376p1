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
    class EntText : Ent
    {
        public static SpriteFont font;
        public Color color;
        public string text;

        public EntText(string t, Color c)
        {
            sprite = defaultSprite;
            wrapX = false;
            wrapY = true;
            text = t;
            color = c;
        }

        public override void join()
        {
            base.join();
            position = (new Vector2(manager.gameWidth, manager.gameHeight)) / 2;
            velocity = Vector2.Zero;
            resistance = 0;
        }

        public override void think()
        {

            //color = new Color((float)Math.Cos(r) * 0.5f + 0.5f, (float)Math.Cos(r + Math.PI * 2.0 / 3) * 0.5f + 0.5f, (float)Math.Cos(r - Math.PI * 2.0 / 3) * 0.5f + 0.5f);

        }

        public override void draw(SpriteBatch sb)
        {
            Vector2 centerdelta = (Switcher.size - font.MeasureString(text)) / 2;

            sb.DrawString(font, text, centerdelta, color);

        }
    }
}
