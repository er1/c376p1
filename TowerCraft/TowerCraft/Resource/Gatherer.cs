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
    public class Gatherer
    {
        public static Model model;

        public const float speed = 1f;
        public const float size = 8.0f;
        public const float viewRadius = 10.0f;

        public GatherZone gatherzone;
        public Vector3 position = Vector3.Zero;
	    public Vector3 direction = Vector3.UnitX;

	    public bool mining = false;
	    public Mineral targetMineral = null;
        public bool targetSet = false;
	    public Vector3 targetPosition = Vector3.Zero;

	    public Gatherer(GatherZone _gatherzone, Vector3 _position) {
            gatherzone = _gatherzone;
		    position = _position;
	    }

	    public void update() {

		    if (mining) {
                if (targetMineral != null)
                {
                    targetMineral.life--;

                    if (targetMineral.life <= 0)
                        gatherzone.manager.gather(targetMineral);
                    targetMineral.remove();
                }
                else
                {
                    mining = false;
                }

		    } else {
			    if (targetMineral == null) {
				    //... go toward targetPosition ...

                    if ((targetPosition - position).Length() < size)
                    {
                        if (gatherzone.minerals.Count > 0)
                        {
                            Random rand = new Random();
                            targetPosition = gatherzone.minerals[rand.Next(gatherzone.minerals.Count)].position;
                        }
                    }
                    else
                    {
                        direction = targetPosition - position;
                        direction.Normalize();
                        position += direction * speed;

                        List<Mineral> nearby = gatherzone.getNearbyMinerals(viewRadius);
                        if (nearby.Count > 0)
                        {
                            targetMineral = nearby[0];
                            targetMineral.addFollower(this);
                        }
                    }


			    } else {
				    // if (... close enough to targetMineral ...)
				    if ((targetMineral.position - position).Length() < size) {
					    if (targetMineral.myGatherer == null) {
						    targetMineral.myGatherer = this;
                            targetMineral.addFollower(this);
                            mining = true;
					    } else {
						    targetMineral = null;
                            mining = false;
					    }
				    } else {
					    //... go toward targetMineral ...
					    direction = targetMineral.position - position;
					    direction.Normalize();
					    position += direction * speed;
				    }
			    }
		    }

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
