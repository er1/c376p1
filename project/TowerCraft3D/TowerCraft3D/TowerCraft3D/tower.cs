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
        private Vector3 position;
        private int upgradeLevel;
        private int radius;
        private monster target;

        public tower(ref Model temp, Vector3 location)
            : base(temp)
        {
            world = Matrix.CreateTranslation(location);
            position = location;
            upgradeLevel = 0;
        }

        public void Update()
        {
            if (lookForTarget())
            {
                Shoot();
            }
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
