using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class Explosion : projectile
    {
        public Explosion(ref Model temp, ref Model colModel, Vector3 location, Vector3 newDirection)
            : base(ref temp, ref colModel, location, newDirection)
        {
            projectileTimer = TimeSpan.FromSeconds(0.5);
            world = Matrix.CreateRotationZ((float)Math.PI/2) * Matrix.CreateScale(10) * Matrix.CreateTranslation(location);
        }
    }
}
