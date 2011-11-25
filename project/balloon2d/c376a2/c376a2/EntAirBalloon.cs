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
    class EntAirBalloon : Ent
    {
        public static new Texture2D defaultSprite;
        double wanderangle = 0;
        EntPC target;

        public EntAirBalloon()
        {
            sprite = defaultSprite;
            wrapX = false;
            wrapY = true;
        }

        public override void join()
        {
            base.join();
            position = new Vector2(1 - size, ((float)rand.NextDouble() * (2.0f / 3.0f) + (1.0f / 6.0f)) * (manager.gameHeight));
            velocity = Vector2.Zero;
            resistance = 0.707f;

            List<Ent> ents = manager.Ents;
            foreach (Ent e in ents)
            {
                if (e is EntPC)
                {
                    target = (EntPC)e;
                    break;
                }
            }
        }

        public override void think()
        {
            velocity += new Vector2((float)Math.Cos(Math.Sin(wanderangle) / 2), (float)Math.Sin(Math.Sin(wanderangle) / 2));
            wanderangle += 0.1;
            base.think();

            if (rand.NextDouble() < 0.01)
            {
                Vector2 d = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());
                if (target != null)
                    d = target.position - position;
                d.Normalize();

                EntBalloon b = new EntBalloon();
                b.position = position + (d * (size / 2));
                b.velocity = d * 4;

                manager.queueAdd(b);
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
                ((EntBullet)target).owner.score += 10;
            }
        }
    }
}
