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
                //for upgrading?
            }
            else if ((model is resource) && (resourceCount < 3))
            {
                resourceCount++;
                entities.Add(model);
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

        public void removeAllMonstersAndProjectiles()
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                if ((entities[i] is monster) || (entities[i] is projectile))
                {
                    entities.RemoveAt(i);
                    i--;
                }
            }
        }
        public bool anyProjectile()
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] is projectile)
                {
                    return true;
                }
            }
            return false;
        }
        public bool anyMonster()
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] is monster)
                {
                    return true;
                }
            }
            return false;
        }

        public bool anyMonsters()
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] is monster)
                {
                    return true;
                }
            }
            return false;
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

        public int getResourceCount()
        {
            return resourceCount;
        }

        public int towerConstruction()
        {
            if (resourceCount == 3)
            {
                int totalValue = 0;
                for (int i = 0; i < entities.Count(); i++)
                {
                    if (entities[i] is resource)
                    {
                        totalValue += ((resource)entities[i]).getValue();
                    }
                }
                return totalValue;
            }
            else
                return 0;
        }

    }
}
