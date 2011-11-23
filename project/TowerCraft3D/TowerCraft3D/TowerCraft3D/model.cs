using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerCraft3D
{
    //Model class based on code from both the lab, Learning XNA by Aaron Reed and myself.
    //Draw is from lab and learning xna
    // Collision from lab
    // What I did was slap it toghether and modified to my need to make it easy to use.
    class model
    {
        Model currentModel;
        //Vector3 location;
        protected Matrix world = Matrix.Identity;

        public model(Model newModel)
        {
            this.currentModel = newModel;
            //this.location = newLocation;
        }

        public bool IsCollision(model model2)
        {
            for (int meshIndex1 = 0; meshIndex1 < this.getModel().Meshes.Count; meshIndex1++)
            {
                BoundingSphere sphere1 = this.getModel().Meshes[meshIndex1].BoundingSphere;
                sphere1 = sphere1.Transform(this.getWorld());

                for (int meshIndex2 = 0; meshIndex2 < model2.getModel().Meshes.Count; meshIndex2++)
                {
                    BoundingSphere sphere2 = model2.getModel().Meshes[meshIndex2].BoundingSphere;
                    sphere2 = sphere2.Transform(model2.getWorld());

                    if (sphere1.Intersects(sphere2))
                        return true;
                }
            }
            return false;
        }

        public virtual Matrix getWorld()
        {
            return world;
        }
        protected virtual Model getModel()
        {
            return currentModel;
        }

        public virtual void Update()
        { }

        public void DrawModel(Camera cam)
        {
            Matrix[] transforms = new Matrix[currentModel.Bones.Count];
            currentModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in this.currentModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.LightingEnabled = true;
                    effect.DirectionalLight1.DiffuseColor = new Vector3(1f, 1f, 1f);
                    effect.DirectionalLight1.Direction = new Vector3(0, 1, 0);
                    effect.DirectionalLight1.SpecularColor = new Vector3(1, 1, 1);
                    effect.AmbientLightColor = new Vector3(0.4f, 0.4f, 0.4f);
                    effect.World = getWorld() * mesh.ParentBone.Transform;
                    effect.View = cam.view;
                    effect.Projection = cam.projection;
                }

                mesh.Draw();
            }
        }
    }
}
