using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class resource : model
    {
        private int type;
        private int value;

        public resource(ref Model temp, int typeOfResource)
            : base(ref temp)
        {
            type = typeOfResource;
            if (type == 0)
            {
                value = 1;
            }
            else if (type == 1)
            {
                value = 3;
            }
            else if (type == 2)
            {
                value = 6;
            }
            else if (type == 3)
            {
                value = 9;
            }

        }

        public int getValue()
        {
            return value;
        }

    }
}
