using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    class waveManager
    {
        public int maxSpawn { get; protected set; }
        public int minSpawn { get; protected set; }
        public TimeSpan levelTimer { get; protected set; }
        public TimeSpan monsterTimer { get; protected set; }
        private TimeSpan managerTimer { get; set; }
        int level{ get; protected set; }
        public bool canSpawn { get; protected set; }


        public waveManager(int currentLevel,int min, int max, TimeSpan levelTime, TimeSpan monsterTime)
        {
            level = currentLevel;
            maxSpawn = max;
            minSpawn = min;
            levelTimer = levelTime;
            monsterTimer = monsterTime;
            managerTimer = monsterTimer;
        }

        public void UpdateWave(GameTime gameTime)
        {
            levelTimer -= gameTime.ElapsedGameTime;
            if (levelTimer <= TimeSpan.Zero)
            {
                //Level is done
            }
            else
            {

                //Timer for Spawn time between monsters
                managerTimer -= gameTime.ElapsedGameTime;
                if (managerTimer <= TimeSpan.Zero)
                {
                    canSpawn = true;
                    managerTimer = monsterTimer;
                }

            }



        }




    }
}
