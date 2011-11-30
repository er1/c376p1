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
        protected Model currentModel;
        //Vector3 location;
        protected Matrix world = Matrix.Identity;
        protected BoundingBox box;

        private static int totalID = 0;
        private int modelID;

        public model(Model newModel)
        {
            this.currentModel = newModel;

            modelID = totalID;
            totalID++;
            //this.location = newLocation;
            box = UpdateBoundingBox(this.getModel(), this.getWorld());
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
        public bool IsCollisionBox(model model2)
        {
            if (this is projectile)
            {
                for (int meshIndex1 = 0; meshIndex1 < ((projectile)this).collisionModel.Meshes.Count; meshIndex1++)
                {
                    BoundingBox box1 = UpdateBoundingBox(((projectile)this).collisionModel, this.getWorld());

                    for (int meshIndex2 = 0; meshIndex2 < model2.getModel().Meshes.Count; meshIndex2++)
                    {
                        BoundingBox box2 = UpdateBoundingBox(model2.getModel(), model2.getWorld());

                        if (box1.Intersects(box2))
                            return true;
                    }
                }
            }
            else
            {
                for (int meshIndex1 = 0; meshIndex1 < this.getModel().Meshes.Count; meshIndex1++)
                {

                    BoundingBox box1 = UpdateBoundingBox(this.getModel(), this.getWorld());

                    for (int meshIndex2 = 0; meshIndex2 < model2.getModel().Meshes.Count; meshIndex2++)
                    {
                        BoundingBox box2 = UpdateBoundingBox(model2.getModel(), model2.getWorld());

                        if (box1.Intersects(box2))
                            return true;
                    }
                }
            }
            return false;
        }

        public int getID()
        {
            return modelID;
        }
        
        public  Matrix getWorld()
        {
            return world;
        }
        protected  Model getModel()
        {
            return currentModel;
        }
        protected  BoundingBox getCollisionBox()
        {
            return box;
        }
        protected BoundingBox UpdateBoundingBox(Model model, Matrix worldTransform)
        {
            // Initialize minimum and maximum corners of the bounding box to max and min values
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // Vertex buffer parameters
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]), worldTransform);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }

            // Create and return bounding box
            return new BoundingBox(min, max);
        }
        //public virtual void Update()
        //{ }
        public void DrawModel(Camera cam)
        {

            //Inside your draw method
            ContainmentType currentContainmentType = ContainmentType.Disjoint;

            //For each gameobject
            //(If you have more than one mesh in the model, this wont work. Use BoundingSphere.CreateMerged() to add them together)
            BoundingSphere meshBoundingSphere = currentModel.Meshes[0].BoundingSphere;
            currentContainmentType = cam.Frustum.Contains(meshBoundingSphere);
            if (currentContainmentType != ContainmentType.Disjoint)
            {
                //Draw gameobject

                Matrix[] transforms = new Matrix[currentModel.Bones.Count];
                currentModel.CopyAbsoluteBoneTransformsTo(transforms);

                foreach (ModelMesh mesh in this.currentModel.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        //effect.LightingEnabled = true;
                        //effect.DirectionalLight1.DiffuseColor = new Vector3(1f, 1f, 1f);
                        //effect.DirectionalLight1.Direction = new Vector3(0, 1, 0);
                        //effect.DirectionalLight1.SpecularColor = new Vector3(1, 1, 1);
                        //effect.AmbientLightColor = new Vector3(0.4f, 0.4f, 0.4f);
                        effect.World = getWorld() * mesh.ParentBone.Transform;
                        effect.View = cam.view;
                        effect.Projection = cam.projection;
                    }

                    mesh.Draw();
                }
            }
        }
    }
}
