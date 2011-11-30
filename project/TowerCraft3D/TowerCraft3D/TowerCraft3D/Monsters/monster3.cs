using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerCraft3D
{
    class monster3 : monster
    {

        public monster3(ref Model temp, Vector3 location, Vector3 newDirection)
            : base(ref temp, location, newDirection)
        {
            type = 3;
            life = 300;

        }
    }
}
