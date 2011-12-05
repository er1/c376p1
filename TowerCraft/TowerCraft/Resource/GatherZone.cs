using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TowerCraft3D;

namespace TowerCraft
{
    public class GatherZone
    {
        public ResourceManager manager;
        public List<Gatherer> gatherers;
        public List<Mineral> minerals;

        public Game1 game;

        protected Random rand = new Random();

        protected List<Gatherer> gatherersAddQueue;
        protected List<Mineral> mineralsAddQueue;

        protected List<Gatherer> gatherersDeleteQueue;
        protected List<Mineral> mineralsDeleteQueue;

        public GatherZone(ResourceManager _rm, Game1 _game)
        {
            game = _game;
            manager = _rm;

            gatherers = new List<Gatherer>();
            minerals = new List<Mineral>();
            gatherersAddQueue = new List<Gatherer>();
            mineralsAddQueue = new List<Mineral>();
            gatherersDeleteQueue = new List<Gatherer>();
            mineralsDeleteQueue = new List<Mineral>();

            
            //minerals
            for (int i = 0; i < 400; ++i)
            {
                Vector3 p = new Vector3(250, 8, 0);
                p += new Vector3((float)rand.NextDouble(), 0.5f, (float)rand.NextDouble()) * 200f - Vector3.One * 80f;

                Mineral m = new Mineral(this, p);

                add(m);
            }
            //gatherers
            for (int i = 0; i < 10; ++i)
            {
                Gatherer g = new Gatherer(this, new Vector3(100, 8, -40 + 8 * i));

                g.targetPosition = new Vector3(250, 8, 0);

                add(g);
            }

            updateLists();
        }

        public void update()
        {
             if (rand.NextDouble() < 0.01) {
                Vector3 p = new Vector3(250, 8, 0);
                p += new Vector3((float)rand.NextDouble(), 0.5f, (float)rand.NextDouble()) * 160f - Vector3.One * 80f;

                Mineral m = new Mineral(this, p);

                add(m);
            }


            if ((rand.NextDouble() < 0.05) && (minerals.Count < gatherers.Count * 2))
            {
                for (int i = 0; i < 200; ++i)
                {
                    Vector3 p = new Vector3(250, 8, 0);
                    p += new Vector3((float)rand.NextDouble(), 0.5f, (float)rand.NextDouble()) * 160f - Vector3.One * 80f;

                    Mineral m = new Mineral(this, p);

                    add(m);
                }
            }


            foreach (Gatherer g in gatherers)
                g.update();
            foreach (Mineral m in minerals)
                m.update();
            updateLists();

            game.LIFE = gatherers.Count() * 100;

        }

        public void draw(Camera cam)
        {

            foreach (Gatherer g in gatherers)
                g.draw(cam);
            foreach (Mineral m in minerals)
                m.draw(cam);
        }

        protected void updateLists()
        {
            foreach (Gatherer g in gatherersAddQueue)
                gatherers.Add(g);
            foreach (Mineral m in mineralsAddQueue)
                minerals.Add(m);
            foreach (Gatherer g in gatherersDeleteQueue)
                gatherers.Remove(g);
            foreach (Mineral m in mineralsDeleteQueue)
                minerals.Remove(m);

            gatherersAddQueue.Clear();
            mineralsAddQueue.Clear();
            gatherersDeleteQueue.Clear();
            mineralsDeleteQueue.Clear();
        }

        public void add(Gatherer g)
        {
            gatherersAddQueue.Add(g);
        }

        public void add(Mineral m)
        {
            mineralsAddQueue.Add(m);
        }

        public void remove(Gatherer g)
        {
            gatherersDeleteQueue.Add(g);
        }

        public void remove(Mineral m)
        {
            mineralsDeleteQueue.Add(m);
        }

        //public List<Gatherer> getNearbyGatherers(double radius)
        //{
        //    return new List<Gatherer>();
        //}

        public List<Mineral> getNearbyMinerals(double radius)
        {
            List<Mineral> r = new List<Mineral>();

            if (minerals.Count > 0)
            {
                r.Add(minerals[rand.Next(minerals.Count)]);
            }

            return r;
        }
    }
}
