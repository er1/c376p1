using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class Bullet : projectile
    {
        public Bullet(ref Model temp, ref Model colModel, Vector3 location, Vector3 newDirection)
            : base(ref temp, ref colModel, location, newDirection)
        {
            world = Matrix.CreateRotationX(angle) * Matrix.CreateRotationZ((float)Math.PI / 2) * Matrix.CreateScale(10) * Matrix.CreateTranslation(location);
        }
    }
}
