using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerCraft3D
{
    class Tile
    {
        List<model> entities;
        int resourceCount;

        public Tile()
        {
            entities = new List<model>();
            resourceCount = 0;
        }

        public List<model> getEntities()
        {
            return entities;
        }


        public void addEntity(model model)
        {
            if ((model is resource) && (resourceCount >= 3))
            {
            }
            else if ((model is resource) && (resourceCount < 3))
            {
            }
            else
            {
                entities.Add(model);
            }
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

        public void towerConstruction()
        {
            if (resourceCount == 3)
            {

            }
        }



    }
}
