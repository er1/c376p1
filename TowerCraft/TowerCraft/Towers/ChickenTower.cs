using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class ChickenTower : tower
    {

        public ChickenTower(ref Model missileModel, Vector3 pos, TileCoord tc)
            : base(ref missileModel, pos, tc)
        {
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world *= Matrix.CreateTranslation(pos);
            life = 50;
            towerDmg = 10;
            timer = TimeSpan.FromSeconds(2.0);
        }

        public override void Shoot()
        {
            timer = TimeSpan.FromSeconds(2.0);
            ((Game1)game).addProject(this.getPosition() + new Vector3(0, 25, 0), new Vector3(-2, 0, 0), 2);
        }
    }
}
