using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace c376a2
{
    class Switcher
    {
        public static Game game;
        public static Vector2 size;

        ThinkDraw current;
        
        public Switcher(ThinkDraw td)
        {
            current = td;
        }

        public void think(GameTime gt)
        {
            current.think(gt);
            ThinkDraw next = current.next();
            if (next != null)
                current = next;
        }

        public void draw(SpriteBatch sb)
        {
            current.draw(sb);
        }
    }
}
