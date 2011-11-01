using System.IO;
using System;
using Microsoft.Xna.Framework.Graphics;   //   for Texture2D
using Microsoft.Xna.Framework;  //  for Vector2

namespace TowerCraft
{
    //Sprite class derived from clsSprite from lab 1 or2, basically programmed for none animated objects
    //in the game
    class Sprite
    {
        //  Sprite texture 
        protected Texture2D texture { get; set; } //  sprite texture, read-only property
        protected Vector2 position; //position vector
        protected Vector2 size { get; set; }      //  sprite size in pixels
        protected Vector2 velocity { get; set; }  //  sprite velocity
        protected Vector2 screenSize { get; set; } //  screen size
        protected Vector2 origin { get; set; }
        protected Vector2 center { get; set; }
        protected float rotationAngle { get; set; }
        protected bool stopDrawing = true; // define if we need this or not
        protected float spriteSize = 1.0f;
        


        //Constructor
        public Sprite(ref Texture2D newTexture, Vector2 newPosition)
        {
            this.texture = newTexture;
            this.position = newPosition;
            this.origin = (new Vector2(texture.Width / 2, texture.Height / 2));
            this.center = (new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2));

        }
        public Vector2 getCenterPosition
        {
            get { return center; }
        }


        //Check colision
        public virtual bool Collides(Sprite otherSprite)
        {
            // check if two sprites intersect
            if (this.position.X + this.size.X > otherSprite.position.X &&

                this.position.X < otherSprite.position.X + otherSprite.size.X &&
                    this.position.Y + this.size.Y > otherSprite.position.Y &&
                    this.position.Y < otherSprite.position.Y + otherSprite.size.Y)
            {
                this.stopDrawing = true;
                return true;
            }
            else
            {
                this.stopDrawing = false;
                return false;
            }
        }
        //Virtual update method
        public virtual void Update(GameTime gameTime)
        { 
            this.center = (new Vector2(position.X + texture.Width/2, position.Y + texture.Height/2));
        }
        //Virtual draw method
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, center, null, color, rotationAngle, origin, spriteSize, SpriteEffects.None, 0);
        }
        //Virtual draw method
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White, rotationAngle, origin,spriteSize,SpriteEffects.None,0); 
        }
    }
}



