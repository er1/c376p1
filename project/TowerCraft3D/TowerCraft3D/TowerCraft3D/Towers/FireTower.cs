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
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world *= Matrix.CreateTranslation(pos);
            life = 50;
            towerDmg = 10;
            range = 4;
            timer = TimeSpan.FromSeconds(5.0);
        }

        public override void Shoot()
        {
            timer = TimeSpan.FromSeconds(0.5);
            ((Game1)game).modelManager.addProject(this.getPosition() + new Vector3(-20, 25, 0), new Vector3(0, 0, 0));
            ((Game1)game).modelManager.addProject(this.getPosition() + new Vector3(-40, 25, 0), new Vector3(0, 0, 0));
            ((Game1)game).modelManager.addProject(this.getPosition() + new Vector3(-40, 25, 20), new Vector3(0, 0, 0));
            ((Game1)game).modelManager.addProject(this.getPosition() + new Vector3(-40, 25, -20), new Vector3(0, 0, 0));

        }
    }
}
