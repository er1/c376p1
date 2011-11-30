using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerCraft3D
{
    class monster4 : monster
    {

        public monster4(ref Model temp, Vector3 location, Vector3 newDirection)
            : base(ref temp, location, newDirection)
        {
            type = 4;
            life = 400;

        }
    }
}
