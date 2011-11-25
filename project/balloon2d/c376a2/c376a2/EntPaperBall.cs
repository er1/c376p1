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
    class EntPaperBall : Ent
    {
        public static new Texture2D defaultSprite;
        double angle;

        public EntPaperBall(Vector2 p)
        {
            position = p;
            velocity = Vector2.UnitY * 3;
            resistance = 1;
            sprite = defaultSprite;
            wrapX = false;
            wrapY = false;
            resistance = 1;
            size *= 3 / 4.0f;
            angle = 0;
        }

        public override void think()
        {
            base.think();
            angle += 0.1;
        }

        public override void draw(SpriteBatch sb)
        {

            Vector2 centerdelta = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Vector2 offset = Vector2.Zero;

            sb.Draw(
                sprite, position + offset, null, Color.White,
                (float)angle,
                centerdelta,
                Vector2.One, SpriteEffects.None, 0
                );
        }

        public override bool collides()
        {
            return false;
        }

    }
}
