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
        //Game variables
        Viewport viewport;
        Map map;
        Camera cam;
        player character;
        TileCoord chosenTile;
        Colony mainBase;

        #region Model Declaration
        Model boundingBox;
        Model MinecraftLikeModel;
        Model Monster1;
        Model bullet;
        Model tile;
        Model colony;
        Model gunTower;
        #endregion

        #region Cube World Variables
        BasicEffect effect;
        protected VertexBuffer vertexBuffer;
        Matrix world = Matrix.Identity;
        Matrix worldTranslation = Matrix.Identity;
        Matrix worldRotation = Matrix.Identity;
        world worldBox;
        Texture2D boxTexture;
        #endregion

        #region List of entities
        List<monster> monsters = new List<monster>();
        List<monster> tiles = new List<monster>();
        List<projectile> projectiles = new List<projectile>();
        List<waveManager> wavesLevel = new List<waveManager>();
        List<tower> towers = new List<tower>();
        #endregion

        #region Variables...
        int percentage;
        int currentWave;
        int worldSize;
        static Random random = new Random();
        bool collisionFlag = false;
        #endregion

        #region bool Input (bobo Input Manager
        bool SpaceBar = false;

        #endregion

        #region Particle effect stuff
        List<ParticleExplosion> explosions = new List<ParticleExplosion>();
        ParticleExplosionSettings particleExplosionSettings = new ParticleExplosionSettings();
        ParticleSettings particleSettings = new ParticleSettings();
        Texture2D explosionTexture;
        Texture2D explosionColorsTexture;
        Effect explosionEffect;

        #endregion

        public ModelManager(Game game)
            : base(game)
        {
            
        }


        public override void Initialize()
        {
            #region Initialize viewport, cam, worldsize, tile, map
            viewport = ((Game1)Game).MainScreen;
            cam = ((Game1)Game).cameraMain;
            worldSize = ((Game1)Game).worldSize;
            chosenTile = ((Game1)Game).cameraMain.getCurrentTC();
            map = new Map();
            #endregion
            base.Initialize();
        }


        protected override void  LoadContent()
        {
            
            #region world stuff
            boxTexture = Game.Content.Load<Texture2D>(@"Textures\\background\\back512");
            worldBox = new world();
            worldBox.setCubeVertices(worldSize);
            // Set vertex data in VertexBuffer
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), worldBox.getCubeVertices().Length, BufferUsage.None);
            vertexBuffer.SetData(worldBox.getCubeVertices());
            // Initialize the BasicEffect
            effect = new BasicEffect(GraphicsDevice);
            #endregion

            #region Load Models
            boundingBox = Game.Content.Load<Model>(@"Models\\BoundingBox");
            MinecraftLikeModel = Game.Content.Load<Model>(@"Models\\Char\\Char");
            Monster1 = Game.Content.Load<Model>(@"Models\\Monster1\\monster1");
            tile = Game.Content.Load<Model>(@"Models\\Map\\Tile");
            colony = Game.Content.Load<Model>(@"Models\\Map\\Colony");
            bullet = Game.Content.Load<Model>(@"Models\\Bullet\\Bullet");
            gunTower = Game.Content.Load<Model>(@"Models\\Towers\\GunTower\\GunTower");
            character = new player(ref MinecraftLikeModel, new Vector3(0, -worldSize+1, 0), worldSize);
            
            #endregion

            #region Load Game Map

            mainBase = new Colony(ref colony, new Vector3(-10, -30, -12));

            //Draw Map
            for (int i = 0; i < 20; i++)
            {
                for (int j = -4; j < 4; j++)
                {
                    tiles.Add(new monster(ref tile, new Vector3(i * -20, 0, j * 20), new Vector3(0, 0, 0)));
                }
            }
            tiles.Add(new monster(ref tile, new Vector3(chosenTile.x * 20, 2, chosenTile.y * 20), new Vector3(0, 0, 0)));
            #endregion

            #region Load incoming waves
            //LOAD WAVE information for Level1
            //SELF NOTE - ADD AN EMPTY FIRST WAVE TO HAVE TIME TO MINE AND PUT STUFF UP
            wavesLevel.Add(new waveManager(1, 20, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(3.0)));
            wavesLevel.Add(new waveManager(1, 20, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(3.0)));
            wavesLevel.Add(new waveManager(1, 20, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(3.0)));
            wavesLevel.Add(new waveManager(2, 30, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(2.0)));
            wavesLevel.Add(new waveManager(2, 30, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(2.0)));
            wavesLevel.Add(new waveManager(2, 30, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(2.0)));
            wavesLevel.Add(new waveManager(3, 40, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0)));
            wavesLevel.Add(new waveManager(3, 40, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0)));
            wavesLevel.Add(new waveManager(3, 40, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0)));
            wavesLevel.Add(new waveManager(4, 50, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(0.75)));
            wavesLevel.Add(new waveManager(4, 50, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(0.75)));
            wavesLevel.Add(new waveManager(4, 50, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(0.75)));
            wavesLevel.Add(new waveManager(5, 60, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(0.50)));
            wavesLevel.Add(new waveManager(5, 60, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(0.50)));
            wavesLevel.Add(new waveManager(5, 60, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(0.50)));
            
            #endregion

            #region Load particle effect stuff
            // Load explosion textures and effect
            explosionTexture = Game.Content.Load<Texture2D>(@"Effect\\Particle");
            explosionColorsTexture = Game.Content.Load<Texture2D>(@"Effect\\ParticleColors");
            explosionEffect = Game.Content.Load<Effect>(@"Effect\\particlefx");

            // Set effect parameters that don't change per particle
            explosionEffect.CurrentTechnique = explosionEffect.Techniques["Technique1"];
            explosionEffect.Parameters["theTexture"].SetValue(explosionTexture);

            #endregion


            base.LoadContent();
        }
        
        
        public override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
           
            #region Update Level1
            //Level 1
            if (currentWave < wavesLevel.Count)
            {
                wavesLevel[currentWave].UpdateWave(gameTime);
                ((Game1)Game).spriteManager.drawHUD(wavesLevel[currentWave].levelTimer, wavesLevel[currentWave].level);
                //Check if this Waves Timer is done or monsters are all dead (so wave is done)
                if ((wavesLevel[currentWave].levelDone && wavesLevel[currentWave].spawn <= 1))
                   
                {
                    //Game.Exit();
                    currentWave++;
                }
                //If Level isn't done then check the Timer to add monsters at invervals
                else if (wavesLevel[currentWave].canSpawn && wavesLevel[currentWave].spawn > 0)
                {
                    int z = RandomNumber(-80, 60);
                    wavesLevel[currentWave].spawn--;
                    wavesLevel[currentWave].canSpawn = false;
                    monsters.Add(new monster(ref Monster1, new Vector3(-390 + 1, 0, z), new Vector3(1, 0, 0)));
                    ((Game1)Game).spriteManager.addLifeBarsMonsters(new Vector2(-390 + 1, z));
                }
            }
            #endregion

            #region Update Drawing the Map
            //update Map
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                tiles[i].Update();
            }
            map.Update();
            mainBase.Update();
            //Selected tile
            chosenTile = ((Game1)Game).cameraMain.getCurrentTC();
            tiles[tiles.Count - 1].Update();

            #endregion

            RemoveDeadEntities();
            UpdateExplosions(gameTime);
            //Has some key input here
            #region Tower Adding
            //Temporary way to add towers.


            if (((Keyboard.GetState().IsKeyDown(Keys.Space)) && (!map.GetTile(chosenTile).anyTower()) && !SpaceBar) ||
                gamePadState.Buttons.A == ButtonState.Pressed)
            {
                SpaceBar = true;
                map.GetTile(chosenTile).addEntity(new resource(ref bullet, 0));

                int resourceValue = map.GetTile(chosenTile).towerConstruction();

                if (resourceValue == 0)
                {

                }
                else if ((resourceValue >= 3) && (resourceValue <= 5))
                {
                    towers.Add(new GunTower(ref gunTower, (new Vector3(chosenTile.x * 20, 0, chosenTile.y * 20)), chosenTile));
                    map.GetTile(chosenTile).addEntity(new tower(ref gunTower, (new Vector3(chosenTile.x * 20, 0, chosenTile.y * 20)), chosenTile));
                }
                else if ((resourceValue >= 3) && (resourceValue <= 5))
                {

                }
                else if ((resourceValue >= 3) && (resourceValue <= 5))
                {

                }
                
            }
            if ((Keyboard.GetState().IsKeyUp(Keys.Space)))
            {
                SpaceBar = false;
            }
            #endregion

            #region Update Monster, Tower, bullets + a little logic
            for (int i = 0; i < monsters.Count; i++)
            {
                percentage = 0;

                if ((monsters[i].life / 100) * 100 == 100)
                {
                    percentage = 100;
                }
                else if ((monsters[i].life / 100) * 100 >= 75 && (monsters[i].life / 100) * 100 <= 99)
                {
                    percentage = 100;
                }
                else if ((monsters[i].life / 100) * 100 >= 50 && (monsters[i].life / 100) * 100 <= 74)
                {
                    percentage = 75;
                }
                else if ((monsters[i].life / 100) * 100 >= 25 && (monsters[i].life / 100) * 100 <= 49)
                {
                    percentage = 50;
                }
                else if ((monsters[i].life / 100) * 100 <= 25)
                {
                    percentage = 25;
                }


                monsters[i].Update();
                ((Game1)Game).spriteManager.updateLifeBarsMonsters(i, percentage, monsters[i].getPosition(), ((Game1)Game).cameraMain, viewport);
                TileCoord monsterLocation 
                    =
                    new TileCoord((int)Math.Floor((monsters[i].getPosition().X+10) / 20.0), (int)Math.Floor((monsters[i].getPosition().Z+10) / 20.0));

                map.GetTile(monsterLocation).addEntity(monsters[i]);
                // HIT THE COLONY NOT FINISHED ( REMOVE LIFE AND BLAH BLAH)
                if (monsters[i].hitColony)
                {
                    monsters.RemoveAt(i);
                    ((Game1)Game).spriteManager.removeLifeBarsMonsters(i);
                    i--;
                    ((Game1)Game).LIFE -= 10;
                }
            }
             //Draws Tower list
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].Update();
                //Shoots a projectile based on a Timer from tower
                if ((towers[i].iWantToShoot(gameTime)) && (towers[i].lookForTarget(map)))
                {
                    if (towers[i].shooting)
                    {
                        towers[i].shooting = false;
                        towers[i].timer = TimeSpan.FromSeconds(1.0);
                        addProject(towers[i].getPosition() + new Vector3(0, 25, 0), new Vector3(-1, 0, 0));
                    }
                }
            }
            //updates projectile list
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update();

                TileCoord projectileLocation
                    =
                    new TileCoord((int)Math.Floor((projectiles[i].getPosition().X + 10) / 20.0), (int)Math.Floor((projectiles[i].getPosition().Z + 10) / 20.0));

                 map.GetTile(projectileLocation).addEntity(projectiles[i]);

                //Check if projectile Timer is at zero to remove
                if (projectiles[i].removeProject(gameTime))
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
            #endregion

            //CheckBoxCollision();
            foreach (KeyValuePair<TileCoord, Tile> pair in map.getDictionary())
            {

                if ((pair.Value.anyMonsters()) && (pair.Value.anyProjectile()))
                {
                    CheckTileCollision(pair.Value.getEntities());
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
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
            tiles[tiles.Count - 1].setPosition(new Vector3(chosenTile.x*20, 2, chosenTile.y*20));

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
        public void addProject(Vector3 position, Vector3 direction)
        {
            //Remember to change Model for diff bullets
            projectiles.Add(new projectile(ref bullet, ref boundingBox, position, direction));
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
        }

        #endregion

        #region XNA Wiki Random Function
        //XNA WIKI Random stuff
        private  int RandomNumber(int min, int max)
        {
             
            return random.Next(min, max);
        }
        public  float RandomBetween(double min, double max)
        {
            
            return (float)(min + (float)random.NextDouble() * (max - min));
        }
        //public static Vector3 RandomPosition(Vector3 minBoxPos, Vector3 maxBoxPos)
        //{
        //    return new Vector3(
        //             RandomBetween(minBoxPos.X, maxBoxPos.X),
        //             RandomBetween(minBoxPos.Y, maxBoxPos.Y),
        //             RandomBetween(minBoxPos.Z, maxBoxPos.Z));
        //}
        #endregion
    }


}
