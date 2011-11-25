using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace c376a2
{
    class EntPC : Ent
    {
        public static new Texture2D defaultSprite;
        public static SpriteFont defaultFont;
        public SpriteFont font;
        public bool facingLeft = false;
        public bool flipped = false;
        public bool dying = false;
        public int score;
        public float lives;
        public double direction;
        public int flashing;

        public bool CanFire
        {
            get { return ((!dying) && (lives > 0)); }
        }

        public EntPC()
        {
            sprite = defaultSprite;
            wrapX = true;
            wrapY = true;
            score = 0;
            lives = 3;

            direction = 0;

            flashing = 0;
        }

        public override void think()
        {
            if (lives <= 0)
                return;

            base.think();

            if (!dying)
            {

                if (velocity.Length() > 0.2f)
                {
                    direction = Math.Atan2(velocity.Y, velocity.X);
                }

                if (flashing > 0)
                    flashing--;
            }
            else
            {
                flashing = 0;
                direction += 0.2;

                velocity += Vector2.UnitY * 2;

                if (position.Y >= manager.gameHeight)
                {
                    dying = false;

                    flashing = 100;
                    lives--;
                    position = new Vector2(manager.gameWidth / 2, manager.gameHeight / 2);
                }
            }
        }

        public override void draw(SpriteBatch sb)
        {
            if (lives <= 0)
                return;

            if (!dying)
            {
                if ((flashing == 0) || (rand.Next(2) == 0))
                {
                    sb.Draw(sprite, position, null, Color.White, 0, new Vector2(sprite.Width / 2, sprite.Height / 2), Vector2.One, (velocity.X < 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                    sb.Draw(EntBullet.defaultSprite, position, null, Color.White, (float)direction, new Vector2(EntBullet.defaultSprite.Width / 2, EntBullet.defaultSprite.Height / 2), Vector2.One, SpriteEffects.None, 0);
                }
            }
            else
            {
                sb.Draw(sprite, position, null, Color.White, (float)direction, new Vector2(sprite.Width / 2, sprite.Height / 2), Vector2.One, (velocity.X < 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            }
        }

        public override bool collides()
        {
            return true;
        }

        public override void collision(Ent target)
        {
            if ((flashing == 0) && !dying &&
                (target is EntBalloon) ||
                (target is EntAirBalloon) ||
                (target is EntPaperBall))
            {
                dying = true;
            }

        }
    }
}
