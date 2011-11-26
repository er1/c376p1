using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{

    class tower : model
    {

        public int towerType { get; set; }
        private Vector3 position;
        private int upgradeLevel;
        private float radius;
        private monster target;
        private TimeSpan timer;
        public Map map;
        private const float TILESIZE = 10;

        

        public tower(ref Model temp, Vector3 location)
            : base(temp)
        {
            world = Matrix.CreateTranslation(location);
            position = location;
            upgradeLevel = 0;
            //usually set a different timer depending on different types of tower
            timer = TimeSpan.FromSeconds(10.0);
        }

        public  override void  Update()
        {
            //if (lookForTarget())
            {
                Shoot();
            }
            world = Matrix.CreateTranslation(position);
        }

        public bool iWantToShoot(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime;
            if (timer <= TimeSpan.Zero)
            {
                 //Reset Timer
                timer = TimeSpan.FromSeconds(10.0);
                return true;
            }
            else
                return false;
        }

        public bool lookForTarget()
        {
            int numberOfTileRange = (int)Math.Ceiling(radius/TILESIZE);
            List<model> targets = new List<model>();
            for (int i = 0; i < numberOfTileRange; i++)
            {
                for (int j = 0; j < numberOfTileRange; j++)
                {
                    if (map.doesTileExist(new TileCoord((int)position.X + i, (int)position.Z + j)))
                    {
                        targets.AddRange(map.GetTile(new TileCoord(i, j)).getEntities().ToList() );
                    }
                }
            }

            return false;
        }

        public void Shoot()
        {

        }


    }
}