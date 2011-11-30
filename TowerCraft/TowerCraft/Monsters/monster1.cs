using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerCraft3D
{
    class monster1 : monster
    {


        public monster1(ref Model temp, Vector3 location, Vector3 newDirection)
            : base(ref temp, location, newDirection)
        {
            type = 1;
            life = 100;

        }
    }
}
