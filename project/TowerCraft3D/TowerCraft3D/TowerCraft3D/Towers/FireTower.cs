using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class FireTower : tower
    {

        public FireTower(ref Model missileModel, Vector3 pos)
            : base(ref missileModel, pos)
        {
            timer = TimeSpan.FromSeconds(5.0);
        }

    }
}
