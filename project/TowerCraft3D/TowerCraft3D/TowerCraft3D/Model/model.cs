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

        public bool IsCollisionBox(model model2)
        {
            for (int meshIndex1 = 0; meshIndex1 < this.getModel().Meshes.Count; meshIndex1++)
            {

                BoundingBox box1 = CalculateBoundingBox(this.getModel());
                //box1 = box1.Transform(this.getWorld());

                for (int meshIndex2 = 0; meshIndex2 < model2.getModel().Meshes.Count; meshIndex2++)
                {
                    BoundingBox box2 = CalculateBoundingBox(model2.getModel());

                    if (box1.Intersects(box2))
                        return true;
                }
            }
            return false;
        }
        public BoundingBox ComputeBoundingBox()
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            Matrix[] bones = new Matrix[this.getModel().Bones.Count];
            this.getModel().CopyAbsoluteBoneTransformsTo(bones);

            List<Vector3> vertices = new List<Vector3>();

            foreach (ModelMesh mesh in this.getModel().Meshes)
            {
                //get the transform of the current mesh
                Matrix transform = bones[mesh.ParentBone.Index];

                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    //get the current mesh info
                    part.
                    int stride = part.VertexStride;
                    int numVertices = part.NumVertices;
                    byte[] verticesData = new byte[stride * numVertices];

                    mesh.VertexBuffer.GetData(verticesData);
                    for (int i = 0; i < verticesData.Length; i += stride)
                    {
                        float x = BitConverter.ToSingle(verticesData, i);
                        float y = BitConverter.ToSingle(verticesData, i + sizeof(float));
                        float z = BitConverter.ToSingle(verticesData, i + 2 * sizeof(float));

                        Vector3 vector = new Vector3(x, y, z);
                        //apply transform to the current point
                        vector = Vector3.Transform(vector, transform);

                        vertices.Add(vector);

                        if (vector.X < min.X) min.X = vector.X;
                        if (vector.Y < min.Y) min.Y = vector.Y;
                        if (vector.Z < min.Z) min.Z = vector.Z;
                        if (vector.X > max.X) max.X = vector.X;
                        if (vector.Y > max.Y) max.Y = vector.Y;
                        if (vector.Z > max.Z) max.Z = vector.Z;
                    }
                }
            }

            //this._VerticesCount = vertices.Count;
            //this._Vertices = vertices.ToArray();

            return new BoundingBox(min, max);
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
