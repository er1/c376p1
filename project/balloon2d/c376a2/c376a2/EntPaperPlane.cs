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
    class EntPaperPlane : Ent
    {
        public static new Texture2D defaultSprite;

        public EntPaperPlane()
        {
            sprite = defaultSprite;
            wrapX = false;
            wrapY = true;
        }

        public override void join()
        {
            base.join();
            position = new Vector2(1 - size, ((float)rand.NextDouble() * (1.0f / 4.0f) + (1.0f / 8.0f)) * (manager.gameHeight));
            velocity = Vector2.UnitX * 2;
            resistance = 1;

        }

        public override void think()
        {
            base.think();
            if (rand.NextDouble() < 0.01) {
                Ent e = new EntPaperBall(position + Vector2.UnitY * size * 0.5f);
                manager.queueAdd(e);
            }
        }

        public override void draw(SpriteBatch sb)
        {

            Vector2 centerdelta = new Vector2(sprite.Width / 2, sprite.Height / 2);

            sb.Draw(
                sprite, position, null, Color.White,
                0,
                centerdelta,
                Vector2.One, SpriteEffects.None, 0
                );

        }

        public override bool collides()
        {
            return true;
        }
        public override void collision(Ent target)
        {
            if (target is EntBullet && (!((EntBullet)target).used) && (!pendingRemoval))
            {
                manager.queueRemove(target);
                manager.queueRemove(this);
                ((EntBullet)target).owner.lives += 1;
            }
        }
    }
}
