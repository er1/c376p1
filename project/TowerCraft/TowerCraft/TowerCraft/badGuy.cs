using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace TowerCraft
{
    class badGuy : Sprite
    {
        bool isDead = false;
        protected double health;
        protected double currentHealth;
        protected float moveBadGuy = 1.0f;
        private Queue<Vector2> pathway = new Queue<Vector2>();

        public badGuy(ref Texture2D newTexture, Vector2 newPosition, double newHealth, float moveSpeed)
            : base(ref newTexture, newPosition)
        {
            this.health = newHealth;
            this.currentHealth = health;
            this.moveBadGuy = moveSpeed;
            this.spriteSize = 2.0f;

        }
        public bool IsDead()
        {
            if (currentHealth <= 0)
            {
                isDead = true;
            }
            else
                isDead = false;

            return isDead;

        }
        public double CurrentHealth(double life)
        {
            currentHealth = life;
            return currentHealth;
        }

        #region pathFinding stuff
        public void SetWaypoints(Queue<Vector2> pathway)
        {
            foreach (Vector2 waypoint in pathway)
                this.pathway.Enqueue(waypoint);

            this.position = this.pathway.Dequeue();
        }
        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, pathway.Peek()); }
        }
        #endregion


        public override void Update(GameTime gameTime)
        {
            #region Checking if bad guy is still Alive
            if (currentHealth <= 0)
            {
                isDead = true;
            }
           
            #endregion

            #region moveToWaypoint
            if (pathway.Count > 0)
            {
                if (DistanceToDestination < 1f)
                {
                    this.position = pathway.Peek();
                    pathway.Dequeue();
                }

                else
                {
                    Vector2 direction = pathway.Peek() - position;
                    direction.Normalize();

                    this.velocity = Vector2.Multiply(direction, moveBadGuy);

                    this.position += velocity;
                }
            }
            else
                this.isDead = true;
            #endregion
            base.Update(gameTime);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isDead)
            {
                double healthPercentage = currentHealth / health;
                //Color color = new Color(new Vector3((float)(1 - healthPercentage), (float)(1 - healthPercentage), (float)(1 - healthPercentage)));
                Color color = Color.White;
                base.Draw(spriteBatch, color);
                //spriteBatch.Draw(texture, center, null, color, rotationAngle, origin, spriteSize, SpriteEffects.None, 0);
            }
        }
    }
}
