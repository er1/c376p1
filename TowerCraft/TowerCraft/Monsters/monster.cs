﻿using System;
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
        protected Vector3 direction { get; set; }
        protected Vector3 initialDirection { get; set; }
        static Random random = new Random(); 
        protected float move = 0.00002f;
        public bool hitColony;
        public int life;
        public bool isDead {get; protected set;}
        public int type;
        public TileCoord tc { get; set; }
        double distance = 0.02f;

        public monster(ref Model temp, Vector3 location, Vector3 newDirection)
            : base(temp)
        {
            direction = newDirection;
            initialDirection = newDirection;
            world = Matrix.CreateRotationY((float)Math.PI/2) * Matrix.CreateTranslation(location);
            //world = Matrix.CreateTranslation(direction);
            
            hitColony = false;
            //life = 100;

        }
        //Random Function
        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public  void Update(GameTime time)
        {
            if (world.M41 >= 0)
            { 
                hitColony = true;
            }

            if (life <= 0)
            {
                isDead = true;
            }
            double elapsedTime = time.ElapsedGameTime.TotalMilliseconds;
            direction += initialDirection* (move*(float) elapsedTime);
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
