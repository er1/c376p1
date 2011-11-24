﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{
    public struct TileCoord
    {
        public int x, y;

        public TileCoord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Map
    {
        public Dictionary<TileCoord, Tile> map { get; set; }
        
        public Map()
        {
            map = new Dictionary<TileCoord, Tile>();
        }

        public Tile GetTile(TileCoord tc)
        {
            return map[tc];
        }

        public void SetTile(TileCoord tc, Tile tile)
        {
            map[tc] = tile;
        }

        public bool doesTileExist(TileCoord tc)
        {
            if (map.ContainsKey(tc))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
