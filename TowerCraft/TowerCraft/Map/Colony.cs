using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class Colony : model
    {
        //float worldSize = 100f;
        protected Matrix rotation = Matrix.Identity;
        //int lives;

        public Colony(ref Model temp, Vector3 location)
            : base(temp)
        {
            //
            world = Matrix.CreateTranslation(location) * Matrix.CreateRotationY((float)-Math.PI / 2) * Matrix.CreateRotationZ((float)Math.PI / 2); 
            //initialDirection = newDirection;
            //lives = 100;
        }

 

        public Vector3 getPosition()
        {
            return new Vector3((float)world.M41, (float)world.M42, (float)world.M43);
        }
    }
}
