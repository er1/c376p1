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

namespace TowerCraft.Resource
{
    public class GatherZone
    {
        public ResourceManager manager;
        public List<Gatherer> gatherers;
        public List<Mineral> minerals;

        protected List<Gatherer> gatherersAddQueue;
        protected List<Mineral> mineralsAddQueue;

        protected List<Gatherer> gatherersDeleteQueue;
        protected List<Mineral> mineralsDeleteQueue;

        public GatherZone(ResourceManager _rm)
        {
            manager = _rm;

            gatherers = new List<Gatherer>();
            minerals = new List<Mineral>();
            gatherersAddQueue = new List<Gatherer>();
            mineralsAddQueue = new List<Mineral>();
            gatherersDeleteQueue = new List<Gatherer>();
            mineralsDeleteQueue = new List<Mineral>();

            for (int i = 0; i < 20; ++i)
            {
                add(new Mineral(this, new Vector3(100, 8, -64 + 2 * i)));
                add(new Gatherer(this, new Vector3(120, 8, -64 + 2 * i)));
            }
            updateLists();
        }

        public void update()
        {
            foreach (Gatherer g in gatherers)
                g.update();
            foreach (Mineral m in minerals)
                m.update();
            updateLists();
        }

        public void draw(Camera cam)
        {
            int i = 5;

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
            gatherersDeleteQueue.Remove(g);
        }

        public void remove(Mineral m)
        {
            mineralsDeleteQueue.Remove(m);
        }

        public List<Gatherer> getNearbyGatherers(double radius)
        {
            return new List<Gatherer>();
        }

        public List<Mineral> getNearbyMinerals(double radius)
        {
            return new List<Mineral>();
        }
    }
}
