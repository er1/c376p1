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
        //Number of spaws
        public int spawn { get;  set; }
        //Timer for this wave
        public TimeSpan levelTimer { get; protected set; }
        //Timer between spawning of each monsters
        public TimeSpan monsterTimer { get; protected set; }
        //Timer used internally in the waveManager
        private TimeSpan managerTimer { get; set; }
        //Integer to store current level
        public int level{ get; protected set; }
        //Bool to check if timer is at zero and can Spawn a monster
        public bool canSpawn { get;  set; }

        //Constructor
        public waveManager(int currentLevel,int numSpawn, TimeSpan levelTime, TimeSpan monsterTime)
        {
            level = currentLevel;
            spawn = numSpawn;
            levelTimer = levelTime;
            monsterTimer = monsterTime;
            managerTimer = monsterTimer;
        }

        //Update function (for Timers)
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
