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

        public CanonTower(ref Model canonModel, Vector3 pos)
            : base(ref canonModel, pos)
        {
            timer = TimeSpan.FromSeconds(10.0);
        }

    }
}
