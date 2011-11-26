using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class GunTower : tower
    {

        public GunTower(ref Model gunModel, Vector3 pos) : base(ref gunModel,pos)
        {
            life = 50;
            towerDmg = 10;
            timer =  TimeSpan.FromSeconds(2.0);
        }

    }
}
