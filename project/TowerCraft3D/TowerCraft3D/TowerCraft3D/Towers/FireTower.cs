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

        public FireTower(ref Model missileModel, Vector3 pos, TileCoord tc)
            : base(ref missileModel, pos, tc)
        {
            timer = TimeSpan.FromSeconds(5.0);
        }

    }
}
