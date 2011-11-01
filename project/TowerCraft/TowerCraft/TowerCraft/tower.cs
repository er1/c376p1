using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft
{
    class tower : Sprite
    {
        #region tower variable declaration
        //Mine Cost

        //bullet damage
        protected double bulletDmg;
        protected double health;
        protected double currentHealth;
        protected double cost;
        protected float shootingRange;
        protected bool isDead = false;


        public tower(ref Texture2D newTexture, Vector2 newPosition) : base(ref newTexture, newPosition)
        {

        }

        public double getTowerDmg()
        {
            return bulletDmg;
        }
        public double getHealth()
        {
            return currentHealth;
        }
        public float getShootingRange()
        {
            return shootingRange;
        }

        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isDead)
            {
                Color color = Color.White;
                spriteSize = 0.4f;
                base.Draw(spriteBatch, color);
                spriteBatch.Draw(texture, center, null, color, rotationAngle, origin, spriteSize, SpriteEffects.None, 0);
            }
        }

    }
}
