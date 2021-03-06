﻿using System;
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
        public TileCoord tc { get; set; }
        //private int upgradeLevel;
        protected float range;
        protected TileCoord currentTargetTC;

        //private monster target;
        public TimeSpan timer { get; set; }
        public Map map;
        private const float TILESIZE = 10;
        public int life { get; set; }
        public bool isDead {get; protected set;}
        protected int towerDmg { get; set; }
        public bool shooting { get; set; }
        public Game game {get; set;}

        public tower(ref Model temp, Vector3 location, TileCoord currentTC)
            : base(temp)
        {

            position = location;
            tc = currentTC;
            //upgradeLevel = 0;
            isDead = false;
            range = 10;
            life = 20;
            //usually set a different timer depending on different types of tower
            //timer = TimeSpan.FromSeconds(2.0);
        }

        public   void  Update()
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
                if (map.GetTile(test).anyMonster())
                {
                    currentTargetTC = test;
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
                
                shooting = true;
                return shooting;
            }
            else
                shooting = false;
            return shooting;
        }
        public virtual void Shoot()
        {
            timer = TimeSpan.FromSeconds(0.5);
            ((Game1)game).addProject(this.getPosition() + new Vector3(0, 25, 0), new Vector3(-1, 0, 0), 0);
        }

        public Vector3 getPosition()
        {
            return new Vector3(world.M41, world.M42, world.M43);
        }
    }
}