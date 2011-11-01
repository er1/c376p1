using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft
{
    //Map class, towercraft uses tile based maps for drawing new levels and logic.
    class map
    {
        //List of Textures for tiles on any level
        private List<Texture2D> tile = new List<Texture2D>();
        //Map Size
        private int MapSize = 75;

        //Testing level 1 with Multi-dimentional array mapping (10x10)
        int[,] Level1 = new int[,] 
        {
            {0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,1,1,1,},
            {0,0,0,0,0,0,0,1,0,1,},
            {1,1,1,1,1,1,1,1,1,1,},
            {0,0,0,0,0,0,0,1,0,0,},
            {0,0,0,0,0,0,0,1,1,1,},
            {0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,},
            //{0,0,0,0,0,0,0,0,0,0,},
        };
        private Queue<Vector2> pathways = new Queue<Vector2>();
        //Constructor for Map
        public map()
        { 
            //TODO later, maybe add stuff here
            pathways.Enqueue(new Vector2(0.05f, 4.05f) * MapSize);
            pathways.Enqueue(new Vector2(7.05f, 4.05f) * MapSize);
            pathways.Enqueue(new Vector2(7.05f, 2.05f) * MapSize);
            pathways.Enqueue(new Vector2(9.05f, 2.05f) * MapSize);
            pathways.Enqueue(new Vector2(9.05f, 4.05f) * MapSize);
            pathways.Enqueue(new Vector2(7.05f, 4.05f) * MapSize);
            pathways.Enqueue(new Vector2(7.05f, 6.05f) * MapSize);
            pathways.Enqueue(new Vector2(9.05f, 6.05f) * MapSize);
        }
        public Queue<Vector2> getPathway
        {
            get { return pathways; }
        }
        //Send new textures to the map class
        public void AddTexture(Texture2D texture)
        {
            tile.Add(texture);
        }
        //Getter for Width 
        public int getWidth
        {
            get { return Level1.GetLength(1); }
        }
        //Getter for Height
        public int getHeight
        {
            get { return Level1.GetLength(0); }
        }
        //Getter for map size
        public int getMapSize
        {
            get { return MapSize; }
        }

        //Draw Function
        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < getWidth; x++)
            {
                for (int y = 0; y < getHeight; y++)
                {
                    int currentIndex = Level1[y, x];
                    //if (currentIndex == -1)
                    //    break;

                    Texture2D texture = tile[currentIndex];
                    batch.Draw(texture, new Rectangle(x * MapSize, y * MapSize, MapSize, MapSize), Color.White);
                }
            }
        }
    }
}
