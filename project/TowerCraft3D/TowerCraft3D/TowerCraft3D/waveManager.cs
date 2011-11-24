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
        public int spawn { get;  set; }
        public TimeSpan levelTimer { get; protected set; }
        public TimeSpan monsterTimer { get; protected set; }
        private TimeSpan managerTimer { get; set; }
        public int level{ get; protected set; }
        public bool canSpawn { get;  set; }


        public waveManager(int currentLevel,int numSpawn, TimeSpan levelTime, TimeSpan monsterTime)
        {
            level = currentLevel;
            spawn = numSpawn;
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
