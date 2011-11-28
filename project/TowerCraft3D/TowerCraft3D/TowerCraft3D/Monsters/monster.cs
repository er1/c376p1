using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerCraft3D
{
    class monster : model
    {
        float worldSize = 100f;
        protected Matrix rotation = Matrix.Identity;
        //protected Vector3 location = new Vector3(0, 0, 0);
        protected Vector3 direction;
        public Vector3 initialDirection { get; set; }
        static Random random = new Random(); //random number (used with function)
        public double minMove { get; set; }
        public double maxMove { get; set; }
        protected float move = 0.5f;
        public bool hitColony {get;set;}
        public int life { get; set; }
        public bool isDead {get; protected set;}

        public monster(ref Model temp, Vector3 location, Vector3 newDirection)
            : base(temp)
        {
            world = Matrix.CreateTranslation(location);
            direction = newDirection*move;
            //initialDirection = newDirection;
            hitColony = false;
            life = 100;

        }
        //Random Function
        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public  void Update()
        {
            if (world.M41 >= 0)
            { 
                hitColony = true;
            }

            if (life <= 0)
            {
                isDead = true;
            }

            world *= Matrix.CreateTranslation(direction);
        }
        public Vector3 getDirection()
        {
            return direction;
        }

        public Vector3 getPosition()
        {
            return new Vector3((float)world.M41, (float)world.M42, (float)world.M43);
        }

        public void setPosition(Vector3 newPos)
        {
            world = Matrix.CreateTranslation(newPos);
        }

        public void setDirection(Vector3 dir)
        {
            world *= Matrix.CreateTranslation(dir);
        }



    }
}
