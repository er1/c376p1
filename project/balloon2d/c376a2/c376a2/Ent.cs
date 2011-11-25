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
    class Ent
    {
        public static Texture2D defaultSprite;
        public static Random rand = new Random();

        protected Texture2D sprite;
        public Vector2 position = new Vector2();
        public Vector2 velocity = new Vector2();
        public float resistance = 0.9f;
        public EntManager manager;
        public float size = 0;
        public bool wrapX = false;
        public bool wrapY = false;
        public bool pendingRemoval;

        public Ent()
        {
            sprite = defaultSprite;
            size = Math.Max(defaultSprite.Width, defaultSprite.Height);
            pendingRemoval = false;
        }

        public virtual Vector2 Position
        {
            get { return position; }
        }

        public virtual void draw(SpriteBatch sb)
        {
            sb.Draw(sprite, position - new Vector2(sprite.Width / 2, sprite.Height / 2), Color.White);
        }

        public virtual void think()
        {
            position += velocity;
            velocity *= resistance;


            if ((position.X < -size) || (position.X > manager.gameWidth + size))
            {
                if (wrapX)
                {
                    position.X = (position.X + manager.gameWidth + size * 3) % (manager.gameWidth + size * 2) - size;
                }
                else
                {
                    manager.queueRemove(this);
                }
            }
            if ((position.Y < -size) || (position.Y > manager.gameHeight + size))
            {
                if (wrapY)
                {
                    position.Y = (position.Y + manager.gameHeight + size * 3) % (manager.gameHeight + size * 2) - size;
                }
                else
                {
                    manager.queueRemove(this);
                }
            }


        }

        public virtual bool collides()
        {
            return false;
        }

        public virtual void collision(Ent target)
        {

        }

        public virtual void join() { }
    }
}
