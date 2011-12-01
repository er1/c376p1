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
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world *= Matrix.CreateTranslation(pos);
            life = 75;
            towerDmg = 10;
            timer = TimeSpan.FromSeconds(1.5);
        }

        public override void Shoot()
        {
            timer = TimeSpan.FromSeconds(1.5);
            ((Game1)game).addProject(new Vector3(currentTargetTC.x * 20, 0, currentTargetTC.y * 20) + new Vector3(0, 25, 0), new Vector3(0, 0, 0), 3);

        }
    }
}
