using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class Missile : projectile
    {

        public Missile(ref Model temp, ref Model colModel, Vector3 location, Vector3 newDirection)
            : base(ref temp, ref colModel, location, newDirection)
        {
            world = Matrix.CreateRotationZ((float)Math.PI/2) * Matrix.CreateScale(3) * Matrix.CreateTranslation(location);
        }

    }
}
