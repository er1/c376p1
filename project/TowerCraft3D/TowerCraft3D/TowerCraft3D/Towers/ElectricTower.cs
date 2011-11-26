using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class ElectricTower : tower
    {

        public ElectricTower(ref Model missileModel, Vector3 pos)
            : base(ref missileModel, pos)
        {
            life = 50;
            towerDmg = 10;
            timer = TimeSpan.FromSeconds(5.0);
        }

    }
}
