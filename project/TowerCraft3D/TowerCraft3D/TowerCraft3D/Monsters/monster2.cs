using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerCraft3D
{
    class monster2 : monster
    {


        public monster2(ref Model temp, Vector3 location, Vector3 newDirection)
            : base(ref temp, location, newDirection)
        {
            type = 2;
            life = 200;

        }
    }
}
