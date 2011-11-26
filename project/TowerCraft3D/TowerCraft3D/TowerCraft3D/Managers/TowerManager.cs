using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerCraft3D
{
    class TowerManager
    {
        /*
         * Tower Documentation (number association)
         * 1- Gun Tower
         * 2- Canon Tower
         * 3- Missile Tower
         * 4- Fire Tower
         * 5- Electric Tower
         * 6- Chicken Tower
         */
        int TowerType;
        int GunTower    = 1;
        int CanonTower = 2;
        int MissileTower = 3;
        int FireTower = 4;
        int ElectricTower = 5;
        int ChickenTower = 6;

        //Ressource
        int Rock = 1;
        int Metal = 3;
        int Fuel = 6;
        int Crystal = 9;


        public TowerManager()
        {
            TowerType = 0;
        }



    }
}
