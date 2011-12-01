using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TowerCraft3D;

namespace TowerCraft
{
    public class ResourceManager
    {
        public int resourceA = 1000;
        public int resourceB = 0;
        public int resourceC = 0;
        public int resourceD = 0;

        public void gather(Mineral m) {
            Random random = new Random();

            int alpha = random.Next(10);

            switch (alpha)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    resourceA++;
                    break;
                case 4:
                case 5:
                case 6:
                    resourceB++;
                    break;
                case 7:
                case 8:
                    resourceC++;
                    break;
                case 9:
                    resourceD++;
                    break;
            }

        }
    }
}
