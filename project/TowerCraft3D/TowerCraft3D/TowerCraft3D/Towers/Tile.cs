using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerCraft3D
{
    class Tile
    {
        List<model> entities;

        public Tile()
        {
            entities = new List<model>();
        }

        public List<model> getEntities()
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

        public bool anyTower()
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] is tower)
                    return true;
            }

            return false;
        }



    }
}
