
using Microsoft.Xna.Framework.Graphics;   //   for Texture2D
using Microsoft.Xna.Framework;  //  for Vector2
using System.IO;
using System;

namespace TowerCraft3D
{
    //Sprite class derived from clsSprite from lab 1 or2, basically programmed for none animated objects
    //in the game
    class Sprite
    {
        //  Sprite texture 
        public Texture2D texture { get; set; } //  sprite texture, read-only property
        public Vector2 position; //position vector
        public Vector2 size;    //  sprite size in pixels
        public Vector2 velocity { get; set; }  //  sprite velocity
        private Vector2 screenSize { get; set; } //  screen size


        //Constructor
        public Sprite(ref Texture2D newTexture, Vector2 Position)
        {
            this.texture = newTexture;
            this.position = Position;
        }
        public void setPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }

        public  void Update(ref Texture2D tex, Vector2 Position)
        {
            this.texture = tex;
            this.position = Position;
        }
        public  void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}



