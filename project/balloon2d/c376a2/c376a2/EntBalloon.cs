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
    class EntBalloon : Ent
    {
        private Color color;
        private double wobble;
        public EntBalloonSet set = null;

        public static new Texture2D defaultSprite;
        public EntBalloon()
        {
            double r = rand.NextDouble() * Math.PI * 2;
            Color c = new Color((float)Math.Cos(r) * 0.5f + 0.5f, (float)Math.Cos(r + Math.PI * 2.0 / 3) * 0.5f + 0.5f, (float)Math.Cos(r - Math.PI * 2.0 / 3) * 0.5f + 0.5f);

            resistance = 1;
            sprite = defaultSprite;
            wobble = rand.NextDouble() * Math.PI * 2f;
            color = c;
            wrapX = true;
            wrapY = true;

            resistance = 1;

            set = null;
            size *= 3 / 4.0f;
        }

        public override Vector2 Position
        {
            get { return position + ((set == null) ? Vector2.Zero : set.position); }
        }

        public override void think()
        {
            wobble += 0.1;

            if (set == null)
                base.think();

        }

        public override void draw(SpriteBatch sb)
        {

            Vector2 centerdelta = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Vector2 offset = Vector2.Zero;

            if (set != null)
                offset = set.position;

            sb.Draw(
                sprite, position + offset, null, color,
                (float)(Math.Sin(wobble)) * 0.5f,
                centerdelta,
                Vector2.One, SpriteEffects.None, 0
                );

        }

        public void pop(EntPC source)
        {
            manager.queueRemove(this);
            if (source != null)
                source.score += 2;

        }

        public override bool collides()
        {
            return (set == null);
        }
        public override void collision(Ent target)
        {
            if (set == null)
            {
                if (target is EntBullet && (!((EntBullet)target).used) && (!pendingRemoval))
                {
                    manager.queueRemove(target);
                    pop(((EntBullet)target).owner);
                    ((EntBullet)target).used = true;
                }
            }
        }
    }
}
