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
        private TileCoord tc;
        private int upgradeLevel;
        private float range;
        private monster target;
        protected TimeSpan timer { get; set; }
        public Map map;
        private const float TILESIZE = 10;
        public int life { get; set; }
        public bool isDead {get; protected set;}
        protected int towerDmg { get; set; }
        public bool shooting { get; set; }

        public tower(ref Model temp, Vector3 location, TileCoord currentTC)
            : base(temp)
        {
            
            position = location;
            tc = currentTC;
            upgradeLevel = 0;
            isDead = false;
            range = 10;
            //usually set a different timer depending on different types of tower
            //timer = TimeSpan.FromSeconds(2.0);
        }

        public  override void  Update()
        {
            if (life <= 0)
            {
                isDead = true;
            }

           // world = Matrix.CreateTranslation(position);
        }

        public bool lookForTarget(Map mainMap)
        {
            map = mainMap;
            for (int i = 0; i < range; i++)
            {
                TileCoord test = tc;
                test.x -= i;
                if (map.GetTile(test).anyMonsters())
                {
                     return true;
                }
            }
            return false;
        }

        public bool iWantToShoot(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime;
            if (timer <= TimeSpan.Zero)
            {
                //Reset Timer
                timer = TimeSpan.FromSeconds(2.0);
                shooting = true;
                return true;
            }
            else
                shooting = false;
                return false;
        }

        public void Shoot()
        {

        }

        public Vector3 getPosition()
        {
            return new Vector3(world.M41, world.M42, world.M43);
        }
    }
}