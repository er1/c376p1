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
    class EntManager
    {
        private List<Ent> ents = new List<Ent>();
        private List<Ent> addqueue = new List<Ent>();
        private List<Ent> deletequeue = new List<Ent>();

        public List<Ent> Ents
        {
            get { return ents; }
            private set { }
        }

        public int gameWidth = 0;
        public int gameHeight = 0;

        public void add(Ent e)
        {
            ents.Add(e);
            e.manager = this;
            e.join();
        }

        public void remove(Ent e)
        {
            ents.Remove(e);
        }

        public void drawAll(SpriteBatch sb)
        {
            foreach (Ent e in ents)
            {
                e.draw(sb);
            }
        }

        public void dequeAll()
        {
            foreach (Ent e in deletequeue)
            {
                remove(e);
            }
            deletequeue.Clear();
            foreach (Ent e in addqueue)
            {
                add(e);
            }
            addqueue.Clear();
        }

        public void thinkAll()
        {
            foreach (Ent e in ents)
            {
                e.think();
            }
            dequeAll();
        }

        public void collisionAll()
        {
            foreach (Ent a in ents)
            {
                if (!a.collides())
                    continue;
                foreach (Ent b in ents)
                {
                    if (a != b)
                    {
                        if ((a.Position - b.Position).Length() < ((a.size + b.size) / 2))
                        {
                            a.collision(b);
                        }
                    }
                }
            }
            dequeAll();
        }

        public void queueAdd(Ent e)
        {
            addqueue.Add(e);
        }

        public void queueRemove(Ent e)
        {
            e.pendingRemoval = true;
            deletequeue.Add(e);
        }
    }
}
