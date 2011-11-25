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
    class EntBullet : Ent
    {
        public int life = 200;
        public EntPC owner;
        public bool used;

        public static new Texture2D defaultSprite;
        public EntBullet(EntPC o, double angle)
        {
            sprite = defaultSprite;
            wrapX = true;
            wrapY = true;
            used = false;

            owner = o;

            position = owner.position;
            double rangle = angle + rand.NextDouble() / 10;

            velocity = new Vector2((float)Math.Cos(rangle), (float)Math.Sin(rangle)) * 5;
            velocity += owner.velocity;
            resistance = 1;
        }

        public override void think()
        {
            base.think();
            life--;
            if (life < 0)
                manager.queueRemove(this);
        }

        public override void draw(SpriteBatch sb)
        {
            if (life <= 50)
            {
                if (rand.Next(2) == 0)
                {
                    return;
                }
            }

            sb.Draw(
                sprite, position, null, Color.White,
                (float)(Math.Atan2(velocity.Y, velocity.X)),
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                Vector2.One, SpriteEffects.None, 0
            );
        }

        public override void collision(Ent target)
        {

        }
    }
}
