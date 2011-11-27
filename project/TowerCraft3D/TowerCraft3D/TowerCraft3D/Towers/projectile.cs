using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerCraft3D
{
    class projectile : model
    {
        float worldSize = 100f;
        protected Matrix rotation = Matrix.Identity;
        //protected Vector3 location = new Vector3(0, 0, 0);
        protected Vector3 direction;
        protected float moveBalloons = 0.5f;
        static Random random = new Random(); //random number (used with function)
        protected TimeSpan projectileDistanceTime;
        public TimeSpan projectileTimer { get; set; }
        public Model collisionModel { get; set; }


        public projectile(ref Model temp, ref Model colModel, Vector3 location, Vector3 newDirection)
            : base(temp)
        {
            collisionModel = colModel;
            world = Matrix.CreateTranslation(location);
            direction = newDirection;
            projectileDistanceTime = TimeSpan.FromSeconds(4);
            projectileTimer = projectileDistanceTime;

        }
        public void setSpeed(float move)
        {
            moveBalloons = move;
        }
        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public override void Update()
        {
            //World wrapping
            //X coordinates
            //if (world.M41 >= worldSize)
            //{ world = Matrix.CreateTranslation(new Vector3(-worldSize + 1, world.M42, world.M43)); }
            //if (world.M41 <= -worldSize)
            //{ world = Matrix.CreateTranslation(new Vector3(worldSize - 1, world.M42, world.M43)); }
            ////Y coordinates
            //if (world.M42 >= worldSize)
            //{ world = Matrix.CreateTranslation(new Vector3(world.M41, -worldSize + 1, world.M43)); }
            //if (world.M42 <= -worldSize)
            //{ world = Matrix.CreateTranslation(new Vector3(world.M41, worldSize - 1, world.M43)); }

            //// Z coordinates
            //if (world.M43 >= worldSize)
            //{ world = Matrix.CreateTranslation(new Vector3(world.M41, world.M42, -worldSize + 1)); }
            //if (world.M43 <= -worldSize)
            //{ world = Matrix.CreateTranslation(new Vector3(world.M41, world.M42, worldSize - 1)); }

            world *= Matrix.CreateTranslation(direction);
            //rotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / 60);
        }
        public bool removeProject(GameTime gameTime)
        {
            projectileTimer -= gameTime.ElapsedGameTime;
            if (projectileTimer <= TimeSpan.Zero)
            {
                return true;
            }
            else
                return false;
        }

        public void setDirection(Vector3 dir)
        {
            world *= Matrix.CreateTranslation(dir);
        }



    }
}
