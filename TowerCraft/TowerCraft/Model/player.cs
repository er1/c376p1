using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerCraft3D
{
    class player : model
    {
        #region player variables
        int worldSize;
        Quaternion rotation = Quaternion.Identity;

        protected float yaw = 0;
        protected float pitch = 0;
        protected float roll = 0;
        protected float move = 2.0f;
        protected float movePitch = 2.0f;
        protected float moveYaw = 8.0f;
        protected float maxPitch = MathHelper.PiOver4 / 2;
        protected float currentPitch = 0;
        //protected Vector3 location = new Vector3(0, 0, 0);
        protected Vector3 direction;
        Vector3 position;
        //protected float moveBalloons = 0.5f;
        static Random random = new Random(); //random number (used with function)
        #endregion
        public player(ref Model temp, Vector3 location, int WorldSize)
            : base(temp)
        {
            position = location;
            worldSize = WorldSize;
            //direction = newDirection;
        }

        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public void Update(Camera cam)
        {
            #region world Wrap
            //World wrapping 
            //X coordinates
            if (world.M41 >= worldSize)
            { world = Matrix.CreateTranslation(new Vector3(-worldSize + 1, world.M42, world.M43)); }
            if (world.M41 <= -worldSize)
            { world = Matrix.CreateTranslation(new Vector3(worldSize - 1, world.M42, world.M43)); }

            //Y coordinates
            if (world.M42 >= worldSize)
            { world = Matrix.CreateTranslation(new Vector3(world.M41, -worldSize + 1, world.M43)); }
            if (world.M42 <= -worldSize)
            { world = Matrix.CreateTranslation(new Vector3(world.M41, worldSize - 1, world.M43)); }

            // Z coordinates
            if (world.M43 >= worldSize)
            { world = Matrix.CreateTranslation(new Vector3(world.M41, world.M42, -worldSize + 1)); }
            if (world.M43 <= -worldSize)
            { world = Matrix.CreateTranslation(new Vector3(world.M41, world.M42, worldSize - 1)); }
            #endregion

            #region UBER rotations with Quaternions!!!
            yaw = 0;
            pitch = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                yaw += ((MathHelper.PiOver4 / 150) * moveYaw);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                yaw += ((MathHelper.PiOver4 / 150) * -moveYaw);
            }

            // Pitch rotation
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                float pitchRotation = (MathHelper.PiOver4 / 180) * movePitch;
                if (Math.Abs(currentPitch + pitchRotation) < maxPitch)
                {
                    pitch += MathHelper.ToRadians(movePitch);
                    currentPitch += pitchRotation;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                float pitchRotation = (MathHelper.PiOver4 / 180) * -movePitch;
                if (Math.Abs(currentPitch + pitchRotation) < maxPitch)
                {
                    pitch += MathHelper.ToRadians(-movePitch);
                    currentPitch += pitchRotation;
                }
            }
            Quaternion rot = Quaternion.CreateFromAxisAngle(Vector3.Right, pitch) * Quaternion.CreateFromAxisAngle(Vector3.Up, yaw) * Quaternion.CreateFromAxisAngle(Vector3.Backward, roll);
            rotation *= rot;
            position += Vector3.Transform(position, Matrix.CreateFromQuaternion(rotation));
            world = Matrix.CreateTranslation(position);

            #endregion
        }
        //GETTERS
        public Vector3 getDirection()
        {
            return direction;
        }

        public Vector3 getPosition()
        {
            return new Vector3((float)world.M41, (float)world.M42, (float)world.M43);
        }
        // SETTERS
        public void setPosition(Vector3 location)
        {
            world = Matrix.CreateTranslation(location);
        }
        //GETTER for world
        //public override Matrix getWorld()
        //{
        //    //Important lesson learned! Matrix multiplication order matters.
        //    return Matrix.CreateFromQuaternion(rotation) * world;
        //}


    }
}
