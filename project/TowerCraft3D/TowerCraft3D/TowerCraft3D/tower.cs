using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{

    class tower : model
    {
        public Vector3 position { get; protected set; }
        private int upgradeLevel;
        private int radius;
        private monster target;
        private TimeSpan timer;
        

        public tower(ref Model temp, Vector3 location)
            : base(temp)
        {
            world = Matrix.CreateTranslation(location);
            position = location;
            upgradeLevel = 0;
            //usually set a different timer depending on different types of tower
            timer = TimeSpan.FromSeconds(10.0);

        }

        public  override void  Update()
        {
           
            if (lookForTarget())
            {
                Shoot();
            }
            world *= Matrix.CreateTranslation(position);
        }

        public bool iWantToShoot(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime;
            if (timer <= TimeSpan.Zero)
            {
                 //Reset Timer
                timer = TimeSpan.FromSeconds(10.0);
                return true;
            }
            else
                return false;
        }

        public bool lookForTarget()
        {
            if (target.getPosition().X > 0)
            {
                return true;
            }
            return false;
        }

        public void Shoot()
        {

        }


    }
}