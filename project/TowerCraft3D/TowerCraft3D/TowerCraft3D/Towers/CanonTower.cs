using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class CanonTower : tower
    {

        public CanonTower(ref Model canonModel, Vector3 pos, TileCoord tc)
            : base(ref canonModel, pos, tc)
        {
            life = 50;
            towerDmg = 10;
            timer = TimeSpan.FromSeconds(10.0);
        }

    }
}
