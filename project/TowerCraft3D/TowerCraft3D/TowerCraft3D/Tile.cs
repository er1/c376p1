using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerCraft3D
{
    class Tile
    {
        HashSet<model> entities;

        public Tile()
        {
            entities = new HashSet<model>();
        }

        public HashSet<model> getEntities()
        {
            return entities;
        }


        public void addEntity(model model)
        {
            entities.Add(model);
        }

        public void removeEntity(model model)
        {
            entities.Remove(model);
        }



    }
}
