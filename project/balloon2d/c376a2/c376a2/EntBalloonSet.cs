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
    class EntBalloonSet : Ent
    {

        private List<EntBalloon> balloons;

        public EntBalloonSet(Vector2 p)
        {
            balloons = new List<EntBalloon>();
            wrapX = true;
            wrapY = true;

            position = p;
            resistance = 0.999999f;
        }

        public override void join()
        {
            for (int i = 0; i < 32; ++i)
            {
                double r = rand.NextDouble() * Math.PI * 2;
                Color c = new Color((float)Math.Cos(r) * 0.5f + 0.5f, (float)Math.Cos(r + Math.PI * 2.0 / 3) * 0.5f + 0.5f, (float)Math.Cos(r - Math.PI * 2.0 / 3) * 0.5f + 0.5f);

                EntBalloon Eballoon = new EntBalloon();
                manager.add(Eballoon);
                balloons.Add(Eballoon);
                Eballoon.set = this;

                double th = rand.NextDouble() * Math.PI * 2;
                double d = rand.NextDouble() * 48 + 16;

                Eballoon.position = new Vector2((float)(Math.Cos(th) * d), (float)(Math.Sin(th) * d));
                Eballoon.velocity = Vector2.Zero;
            }
            size = 100;
        }

        /*public override void draw(SpriteBatch sb)
        {
        }*/

        public void disperse(EntPC source)
        {
            foreach (EntBalloon e in balloons)
            {
                e.set = null;
                e.position += position;
                e.velocity = (e.position - position) + velocity;
                e.velocity.Normalize();
                resistance = 0.95f;
            }
            manager.queueRemove(this);
            source.score += 1;
        }

        public override bool collides()
        {
            return true;
        }
        public override void collision(Ent target)
        {
            if (target is EntBullet && (!((EntBullet)target).used) && (!pendingRemoval))
            {
                disperse(((EntBullet)target).owner);
                manager.queueRemove(target);
                ((EntBullet)target).used = true;
            }
        }
    }
}
