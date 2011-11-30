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


namespace TowerCraft3D
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ModelManager : DrawableGameComponent
    {


        public ModelManager(Game game)
            : base(game)
        {
            
        }

        public override void Initialize()
        {
            #region Initialize viewport, cam, worldsize, tile, map

            
            
            #endregion
            base.Initialize();
        }

        protected override void  LoadContent()
        {
            


            base.LoadContent();
        }
             
        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
<<<<<<< HEAD
            GraphicsDevice.Viewport = viewport;
            
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            #region Draw Game Map
            //Draw the Texture world around the game.

            #region Draw Cube World
            // Set the vertex buffer on the GraphicsDevice
            GraphicsDevice.SetVertexBuffer(vertexBuffer);
            //Set object and camera info
            //effect.World = Matrix.Identity;
            effect.World = world;
            //effect.World = worldRotation * worldTranslation * worldRotation;
            effect.View = cam.view;
            effect.Projection = cam.projection;
            //effect.VertexColorEnabled = true;
            effect.Texture = boxTexture;
            effect.TextureEnabled = true;


            // Begin effect and draw for each pass
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                    (PrimitiveType.TriangleStrip, worldBox.getCubeVertices(), 0, 24);
            }
            #endregion


            //Draw Map
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].DrawModel(cam);
            }
            //Draw the left base
            mainBase.DrawModel(cam);
            //Selected tile
            chosenTile = ((Game1)Game).cameraMain.getCurrentTC();
            tiles[tiles.Count - 1].setPosition(new Vector3(chosenTile.x * 20, 2, chosenTile.y * 20));

            #endregion

            #region Draw Monsters, towers, bullets

            //Draw Player
            character.DrawModel(cam);

            //Draw Monsters
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].DrawModel(cam);
            }
            //Draws projectiles list
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].DrawModel(cam);
            }
            //Draws Tower list
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].DrawModel(cam);
            }
            #endregion

            foreach (ParticleExplosion temp in explosions)
            {
                temp.Draw(((Game1)Game).cameraMain);
            }
            
            base.Draw(gameTime);
        }

        #region function to shoot
        //Add projectiles to the List(to update - check collision - draw)
        public void addProject(Vector3 position, Vector3 direction, int type)
        {

            switch (type)
            {
                    //guntower
                case 0:
                    projectiles.Add(new Bullet(ref bullet, ref boundingBox, position, direction));
                    break;
                    //missile tower
                case 1:
                    projectiles.Add(new Missile(ref missile, ref boundingBox, position, direction));
                    break;
                    //chicken tower
                case 2:
                    projectiles.Add(new Egg(ref egg, ref boundingBox, position, direction));
                    break;
                    //explosions!
                case 3:
                    projectiles.Add(new Explosion(ref explosion, ref boundingBox, position, direction));
                    //explosions.Add(new ParticleExplosion(GraphicsDevice,
                    //           position,
                    //           random.Next(
                    //               particleExplosionSettings.minLife,
                    //               particleExplosionSettings.maxLife),
                    //           random.Next(
                    //               particleExplosionSettings.minRoundTime,
                    //               particleExplosionSettings.maxRoundTime),
                    //           random.Next(
                    //               particleExplosionSettings.minParticlesPerRound,
                    //               particleExplosionSettings.maxParticlesPerRound),
                    //           random.Next(
                    //               particleExplosionSettings.minParticles,
                    //               particleExplosionSettings.maxParticles),
                    //           explosionColorsTexture, particleSettings,
                    //           explosionEffect));
                    break;                    

            }

        }
        #endregion

        #region function to check collision
        //Checks for collision between projectile list and monster list
        public void CheckCollision()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (!collisionFlag)
                {
                    for (int j = 0; j < monsters.Count; j++)
                    {
                        if (projectiles[i].IsCollision(monsters[j]))
                        {
                            projectiles.RemoveAt(i);
                            monsters[j].life -= 25;
                            if (i != 0)
                                i--;
                            
                            collisionFlag = true;
                            break;
                        }
                    }
                }
                collisionFlag = false;
            }
        }

        public void CheckBoxCollision()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (!collisionFlag)
                {
                    for (int j = 0; j < monsters.Count; j++)
                    {
                        if (projectiles[i].IsCollisionBox(monsters[j]))
                        {
                            projectiles.RemoveAt(i);
                            monsters[j].life -= 25;
                            //monsters.RemoveAt(j);
                            if (i != 0)
                                i--;
                            //if (j != 0)
                            //    j--;
                            collisionFlag = true;
                            break;
                        }
                    }
                }
                collisionFlag = false;
            }
        }

        private void CheckTileCollision(List<model> listOnTile)
        {
            for (int i = 0; i < listOnTile.Count; i++)
            {
                if (!collisionFlag)
                {
                    for (int j = 0; j < listOnTile.Count; j++)
                    {
                        if (listOnTile[i].IsCollisionBox(listOnTile[j]))
                        {
                            //projectiles.RemoveAt(i);
                            for (int z = 0; z < projectiles.Count; z++)
                            {
                                if (projectiles[z].getID() == listOnTile[i].getID())
                                {
                                    projectiles.RemoveAt(z);
                                    break;
                                }
                            }
                            
                            monsters[j].life -= 25;
                            //monsters.RemoveAt(j);
                            //if (i != 0)
                            //    i--;
                            //if (j != 0)
                            //    j--;
                            collisionFlag = true;
                            break;
                        }
                    }
                    break;
                }
                collisionFlag = false;
            }
        }
        #endregion

        #region function to remove object when dead (no life) + particle effect call
        public void RemoveDeadEntities()
        {
            for (int j = 0; j < monsters.Count; j++)
            {
                if (monsters[j].isDead)
                {
                    explosions.Add(new ParticleExplosion(GraphicsDevice,
                               monsters[j].getWorld().Translation,
                               random.Next(
                                   particleExplosionSettings.minLife,
                                   particleExplosionSettings.maxLife),
                               random.Next(
                                   particleExplosionSettings.minRoundTime,
                                   particleExplosionSettings.maxRoundTime),
                               random.Next(
                                   particleExplosionSettings.minParticlesPerRound,
                                   particleExplosionSettings.maxParticlesPerRound),
                               random.Next(
                                   particleExplosionSettings.minParticles,
                                   particleExplosionSettings.maxParticles),
                               explosionColorsTexture, particleSettings,
                               explosionEffect));
                    monsters.RemoveAt(j);
                    
                    ((Game1)Game).spriteManager.removeLifeBarsMonsters(j);
                    if (j != 0)
                        j--;
                }
            }

            for (int j = 0; j < towers.Count; j++)
            {
                if (towers[j].isDead)
                {
                    explosions.Add(new ParticleExplosion(GraphicsDevice,
                               towers[j].getWorld().Translation,
                               random.Next(
                                   particleExplosionSettings.minLife,
                                   particleExplosionSettings.maxLife),
                               random.Next(
                                   particleExplosionSettings.minRoundTime,
                                   particleExplosionSettings.maxRoundTime),
                               random.Next(
                                   particleExplosionSettings.minParticlesPerRound,
                                   particleExplosionSettings.maxParticlesPerRound),
                               random.Next(
                                   particleExplosionSettings.minParticles,
                                   particleExplosionSettings.maxParticles),
                               explosionColorsTexture, particleSettings,
                               explosionEffect));
                    towers.RemoveAt(j);
                    if (j != 0)
                        j--;
                }
            }
        }


        #endregion

        #region Particle effect Update
        protected void UpdateExplosions(GameTime gameTime)
        {
            // Loop through and update explosions
            for (int i = 0; i < explosions.Count; ++i)
            {
                explosions[i].Update(gameTime);
                // If explosion is finished, remove it
                if (explosions[i].IsDead)
                {
                    explosions.RemoveAt(i);
                    --i;
                }
            }
=======
           
>>>>>>> bf7448f9bd6abc68b8e442274e626a2c36a797de
        }


    }


}
