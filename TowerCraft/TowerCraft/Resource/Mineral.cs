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
    public class Mineral
    {
        public static Model model;
        
        GatherZone gatherzone;

	    public Vector3 position;
	    public double life = 100;
        public Gatherer myGatherer = null;

        public Mineral(GatherZone _gatherzone, Vector3 _position) {
            gatherzone = _gatherzone;
            position = _position;
        }

	    public Dictionary<Gatherer, bool> followers;

	    public void addFollower(Gatherer g) {
            followers.Add(g, true);
	    }

        public void removeFollower(Gatherer g)
        {
            followers.Remove(g);
        }

	    public void remove() {
		    foreach (Gatherer g in followers.Keys) {
			    g.targetMineral = null;
		    }
	    }

        public void update() {
        
        }

        public void draw(Camera cam)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateTranslation(position);
                    effect.View = cam.view;
                    effect.Projection = cam.projection;
                }
                mesh.Draw();
            }
        }
    }
}
