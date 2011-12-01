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
using TowerCraft;

namespace TowerCraft3D
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public SpriteManager spriteManager { get; protected set; }
        public Camera cameraMain { get; protected set; }
        public Viewport MainScreen;
       
        public ResourceManager resourcemanager;
        public GatherZone gatherzone;

        public int worldWidth;
        public int worldHeight;
        public int worldSize{get;protected set;}
        public int LIFE {get;set;}

        bool started = false;
        public int gameState = 0;

        //Game variables
        //Viewport viewport;
        Map map;

        TileCoord chosenTile;
        Colony mainBase; 

        #region Model Declaration
        Model boundingBox;
        Model MinecraftLikeModel;
        Model Monster1;
        Model Monster2;
        Model Monster3;
        Model Monster4;
        Model bullet, missile, egg, explosion;
        Model tile;
        Model colony;
        Model gunTower, cannonTower, missileTower, fireTower, electricTower, chickenTower;
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
        Dictionary<monster, bool> monsters = new Dictionary<monster, bool>();
        Dictionary<projectile, bool> projectiles = new Dictionary<projectile, bool>();
        Dictionary<int, waveManager> wavesLevel = new Dictionary<int, waveManager>();
        Dictionary<tower, bool> towers = new Dictionary<tower, bool>();

        Dictionary<monster, bool> tiles = new Dictionary<monster, bool>();
        monster SelectionTile;
        #endregion

        #region Variables...
        int percentage;
        int currentWave;
        int chance;
        static Random random = new Random();
        bool collisionFlag = false;
        int curResource = 0;
        #endregion

        #region bool Input (bobo Input Manager
        bool SpaceBar = false;

        #endregion

        #region Particle effect stuff
        Dictionary<ParticleExplosion, bool> explosions = new Dictionary<ParticleExplosion, bool>();
        ParticleExplosionSettings particleExplosionSettings = new ParticleExplosionSettings();
        ParticleSettings particleSettings = new ParticleSettings();
        Texture2D explosionTexture;
        Texture2D explosionColorsTexture;
        Effect explosionEffect;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = false;
            //this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
        }
        protected override void Initialize()
        {
            
            LIFE = 1000;
            
            worldHeight = graphics.PreferredBackBufferHeight;
            worldWidth = graphics.PreferredBackBufferWidth;
            map = new Map();
            #region Viewport stuff
            MainScreen = new Viewport();
            MainScreen.X = 0;
            MainScreen.Y = 0;
            MainScreen.Width = worldWidth;
            MainScreen.Height = worldHeight;
            MainScreen.MinDepth = 0;
            MainScreen.MaxDepth = 1;
            #endregion

            #region init Game components
            Components.Remove(cameraMain);
            Components.Remove(spriteManager);
            cameraMain = new Camera(this, new Vector3(0, 200, 199), new Vector3(0, -5, 1), Vector3.Up, MainScreen, true, worldSize);
            spriteManager = new SpriteManager(this);
            Components.Add(cameraMain);
            Components.Add(spriteManager);
            #endregion

            chosenTile = cameraMain.getCurrentTC();
            this.IsFixedTimeStep = true;
            //this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 60);  

            // resource management
            resourcemanager = new ResourceManager();
            gatherzone = new GatherZone(resourcemanager);

            base.Initialize();

        }     
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

                #region world stuff
                boxTexture = Content.Load<Texture2D>(@"Textures\\background\\back512");
                worldBox = new world();
                worldBox.setCubeVertices(worldSize);
                // Set vertex data in VertexBuffer
                vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), worldBox.getCubeVertices().Length, BufferUsage.None);
                vertexBuffer.SetData(worldBox.getCubeVertices());
                // Initialize the BasicEffect
                effect = new BasicEffect(GraphicsDevice);
                #endregion

                #region Load Models
                boundingBox = Content.Load<Model>(@"Models\\BoundingBox");
                MinecraftLikeModel = Content.Load<Model>(@"Models\\Char\\Char");
                Monster1 = Content.Load<Model>(@"Models\\Monster1");
                Monster2 = Content.Load<Model>(@"Models\\Monster2");
                Monster3 = Content.Load<Model>(@"Models\\Monster3");
                Monster4 = Content.Load<Model>(@"Models\\Monster4");
                tile = Content.Load<Model>(@"Models\\Map\\Tile");
                colony = Content.Load<Model>(@"Models\\Map\\Colony");
                bullet = Content.Load<Model>(@"Models\\Bullet\\Bullet");
                egg = Content.Load<Model>(@"Models\\Bullet\\Egg");
                missile = Content.Load<Model>(@"Models\\Bullet\\Missile");
                explosion = Content.Load<Model>(@"Models\\Bullet\\Explosion");
                gunTower = Content.Load<Model>(@"Models\\Towers\\GunTower\\GunTower");
                cannonTower = Content.Load<Model>(@"Models\\Towers\\CannonTower\\CannonTower");
                missileTower = Content.Load<Model>(@"Models\\Towers\\MissileTower\\MissileTower");
                electricTower = Content.Load<Model>(@"Models\\Towers\\Electric\\ElectricTower");
                fireTower = Content.Load<Model>(@"Models\\Towers\\FireTower\\FireTower");
                chickenTower = Content.Load<Model>(@"Models\\Towers\\ChickenTower\\ChickenTower");


                #endregion

                #region Load Game Map

                mainBase = new Colony(ref colony, new Vector3(-10, -30, -12));

                //Draw Map
                for (int i = 0; i < 20; i++)
                {
                    for (int j = -4; j < 4; j++)
                    {
                        tiles.Add(new monster(ref tile, new Vector3(i * -20, 0, j * 20), new Vector3(0, 0, 0)), true);
                    }
                }
                SelectionTile = new monster(ref tile, new Vector3(chosenTile.x * 20, 2, chosenTile.y * 20), new Vector3(0, 0, 0));
                #endregion

                #region Load incoming waves
                //LOAD WAVE information for Level1
                //SELF NOTE - ADD AN EMPTY FIRST WAVE TO HAVE TIME TO MINE AND PUT STUFF UP
                wavesLevel.Add(0, new waveManager(1, 20, TimeSpan.FromMinutes(0.5), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(1, new waveManager(1, 20, TimeSpan.FromMinutes(0.5), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(2, new waveManager(1, 20, TimeSpan.FromMinutes(0.5), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(3, new waveManager(2, 30, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(4, new waveManager(2, 30, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(5, new waveManager(2, 30, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(6, new waveManager(3, 40, TimeSpan.FromMinutes(1.5), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(7, new waveManager(3, 40, TimeSpan.FromMinutes(1.5), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(8, new waveManager(3, 40, TimeSpan.FromMinutes(1.5), TimeSpan.FromSeconds(1.0)));
                wavesLevel.Add(9, new waveManager(4, 50, TimeSpan.FromMinutes(1.5), TimeSpan.FromSeconds(0.75)));
                wavesLevel.Add(10, new waveManager(4, 50, TimeSpan.FromMinutes(1.5), TimeSpan.FromSeconds(0.75)));
                wavesLevel.Add(11, new waveManager(4, 50, TimeSpan.FromMinutes(1.5), TimeSpan.FromSeconds(0.75)));
                wavesLevel.Add(12, new waveManager(5, 60, TimeSpan.FromMinutes(2.0), TimeSpan.FromSeconds(0.50)));
                wavesLevel.Add(13, new waveManager(5, 60, TimeSpan.FromMinutes(2.0), TimeSpan.FromSeconds(0.50)));
                wavesLevel.Add(14, new waveManager(5, 60, TimeSpan.FromMinutes(2.0), TimeSpan.FromSeconds(0.50)));

                #endregion

                #region Load particle effect stuff
                // Load explosion textures and effect
                explosionTexture = Content.Load<Texture2D>(@"Effect\\Particle");
                explosionColorsTexture = Content.Load<Texture2D>(@"Effect\\ParticleColors");
                explosionEffect = Content.Load<Effect>(@"Effect\\particlefx");

                // Set effect parameters that don't change per particle
                explosionEffect.CurrentTechnique = explosionEffect.Techniques["Technique1"];
                explosionEffect.Parameters["theTexture"].SetValue(explosionTexture);

            #endregion
        }      
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }     
        protected override void Update(GameTime gameTime)
        {
            
            #region Menu (GAMESTATES)
            //menu
            if (gameState == 0)
            {
                if (!started)
                {
                    Components.Remove(cameraMain);
                    Components.Remove(spriteManager);

                    cameraMain = new Camera(this, new Vector3(0, 200, 199), new Vector3(0, -5, 1), Vector3.Up, MainScreen, true, worldSize);
                    spriteManager = new SpriteManager(this);

                    Components.Add(cameraMain);
                    Components.Add(spriteManager);
                    started = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState = 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }
            }
                // game
            else if (gameState == 1)
            {
                base.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    gameState = 2;
                }
            }
                //pause
            else if (gameState == 2)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    gameState = 0;
                    started = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState = 1;
                }
            }
            #endregion

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //GAMESTATE 1 = playing
            if (gameState == 1)
            {
                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                #region Update Level
                //Level 1
                if (currentWave < wavesLevel.Count)
                {
                    wavesLevel[currentWave].UpdateWave(gameTime);
                    spriteManager.drawHUD(wavesLevel[currentWave].levelTimer, wavesLevel[currentWave].level, curResource);
                    //Check if this Waves Timer is done or monsters are all dead (so wave is done)
                    if ((wavesLevel[currentWave].levelDone && wavesLevel[currentWave].spawn <= 1))
                    {
                        //Game.Exit();
                        //System.GC.Collect();
                        currentWave++;
                    }
                    //If Level isn't done then check the Timer to add monsters at invervals
                    else if (wavesLevel[currentWave].canSpawn && wavesLevel[currentWave].spawn > 0)
                    {
/*
                        monsters.Add(new monster4(ref Monster4, new Vector3(-390 + 1, 5, z), new Vector3(1, 0, 0)), true);
                        spriteManager.addLifeBarsMonsters(new Vector2(-390 + 1, z));
                    }
                }
            }
            else
            {
                // YOU WIN
                gameState = 0;
            }
            #endregion
            
            #region Update Drawing the Map
            //update Map
            map.Update();
            //Selected tile
            chosenTile = cameraMain.getCurrentTC();
            SelectionTile.Update();
*/
                        int z = RandomNumber(-80, 60);
                        wavesLevel[currentWave].spawn--;
                        wavesLevel[currentWave].canSpawn = false;
                        chance = RandomNumber(1, wavesLevel[currentWave].level);

                        if (chance == 1)
                        {
                            monsters.Add(new monster1(ref Monster1, new Vector3(-390 + 1, 5, z), new Vector3(1, 0, 0)), true);
                            spriteManager.addLifeBarsMonsters(new Vector2(-390 + 1, z));
                        }
                        if (chance == 2)
                        {
                            monsters.Add(new monster2(ref Monster2, new Vector3(-390 + 1, 5, z), new Vector3(1, 0, 0)), true);
                            spriteManager.addLifeBarsMonsters(new Vector2(-390 + 1, z));
                        }
                        if (chance == 3)
                        {
                            monsters.Add(new monster3(ref Monster3, new Vector3(-390 + 1, 5, z), new Vector3(1, 0, 0)), true);
                            spriteManager.addLifeBarsMonsters(new Vector2(-390 + 1, z));
                        }
                        if (chance >= 4)
                        {
                            monsters.Add(new monster4(ref Monster4, new Vector3(-390 + 1, 5, z), new Vector3(1, 0, 0)), true);
                            spriteManager.addLifeBarsMonsters(new Vector2(-390 + 1, z));
                        }
                    }
                }
                #endregion
//

                #region Update Drawing the Map
                //update Map
                map.Update();
                //Selected tile
                chosenTile = cameraMain.getCurrentTC();
                SelectionTile.Update();

                #endregion

                RemoveDeadEntities();
                UpdateExplosions(gameTime);
                //Has some key input here
                //Selecting Resources
                if (curResource > 0)
                {
                    if (((gamePadState.Triggers.Left == 1) || (Keyboard.GetState().IsKeyDown(Keys.Q))) && !SpaceBar)// 
                    {
                        curResource--;
                        SpaceBar = true;
                    }
                }
                if (curResource < 5)
                {
                    if (((gamePadState.Triggers.Right == 1) || (Keyboard.GetState().IsKeyDown(Keys.E))) && !SpaceBar)// 
                    {
                        curResource++;
                        SpaceBar = true;
                    }
                }


                #region Update Monster, Tower, bullets + a little logic

                #region monster
                foreach (KeyValuePair<monster, bool> pair in monsters)
                {
                    percentage = 0;

                    if ((pair.Key.life / 100) * 100 == 100)
                    {
                        percentage = 100;
                    }
                    else if ((pair.Key.life / 100) * 100 > 75 && (pair.Key.life / 100) * 100 <= 99)
                    {
                        percentage = 100;
                    }
                    else if ((pair.Key.life / 100) * 100 > 50 && (pair.Key.life / 100) * 100 <= 75)
                    {
                        percentage = 75;
                    }
                    else if ((pair.Key.life / 100) * 100 > 25 && (pair.Key.life / 100) * 100 <= 50)
                    {
                        percentage = 50;
                    }
                    else if ((pair.Key.life / 100) * 100 <= 25)
                    {
                        percentage = 25;
                    }


                    pair.Key.Update();
                    //spriteManager.updateLifeBarsMonsters(0, percentage, pair.Key.getPosition(), cameraMain, MainScreen);
                    TileCoord monsterLocation
                        =
                        new TileCoord((int)Math.Floor((pair.Key.getPosition().X + 10) / 20.0), (int)Math.Floor((pair.Key.getPosition().Z + 10) / 20.0));

                    map.GetTile(monsterLocation).addEntity(pair.Key);
                    // HIT THE COLONY NOT FINISHED ( REMOVE LIFE AND BLAH BLAH)
                    if (pair.Key.hitColony)
                    {
                        pair.Key.life -= 100;
                        //spriteManager.removeLifeBarsMonsters(i);
                        LIFE -= 10;
                    }
                }
                #endregion

                #region towers
                //Draws Tower list
                foreach (KeyValuePair<tower, bool> pair in towers)
                {
                    pair.Key.Update();
                    pair.Key.game = this;

                    TileCoord towerLocation
                        =
                        new TileCoord((int)Math.Floor((pair.Key.getPosition().X + 10) / 20.0), (int)Math.Floor((pair.Key.getPosition().Z + 10) / 20.0));

                    map.GetTile(towerLocation).addEntity(pair.Key);

                    //Shoots a projectile based on a Timer from tower
                    if ((pair.Key.iWantToShoot(gameTime)) && (pair.Key.lookForTarget(map)))
                    {
                        if (pair.Key.shooting)
                        {
                            pair.Key.shooting = false;
                            //towers[i].timer = TimeSpan.FromSeconds(3.0);
                            pair.Key.Shoot();
                            //addProject(towers[i].getPosition() + new Vector3(0, 25, 0), new Vector3(-1, 0, 0));
                        }
                    }
                }
                #endregion

                #region projectiles
                //updates projectile list
                foreach (KeyValuePair<projectile, bool> pair in projectiles)
                {
                    pair.Key.Update(gameTime);

                    TileCoord projectileLocation
                        =
                        new TileCoord((int)Math.Floor((pair.Key.getPosition().X + 10) / 20.0), (int)Math.Floor((pair.Key.getPosition().Z + 10) / 20.0));

                    map.GetTile(projectileLocation).addEntity(pair.Key);

                    //Check if projectile Timer is at zero to remove
                    if (pair.Key.removeProject(gameTime))
                    {
                        projectiles.Remove(pair.Key);
                        break;
                    }
                }
                #endregion

                #endregion

                #region Collision detection
                //CheckBoxCollision();
                foreach (KeyValuePair<TileCoord, Tile> pair in map.getDictionary())
                {
                    if ((pair.Value.anyMonster()) && (pair.Value.anyTower()))
                    {
                        List<model> test = pair.Value.getEntities();
                        for (int i = 0; i < test.Count(); i++)
                        {
                            if (test[i] is tower)
                            {
                                foreach (KeyValuePair<tower, bool> pairA in towers)
                                {
                                    if (pairA.Key.getID() == test[i].getID())
                                    {
                                        ((tower)pairA.Key).life -= 25;
                                        break;
                                    }
                                }
                            }
                            if (test[i] is monster)
                            {
                                ((monster)test[i]).life -= 1000;
                            }

                        }
                    }

                    if ((pair.Value.anyMonster()) && (pair.Value.anyProjectile()))
                    {
                        List<model> test = pair.Value.getEntities();
                        for (int i = 0; i < test.Count(); i++)
                        {
                            if (test[i] is monster)
                            {
                                ((monster)test[i]).life -= 25;
                            }
                            if (test[i] is projectile)
                            {
                                foreach (KeyValuePair<projectile, bool> pairA in projectiles)
                                {
                                    if (pairA.Key.getID() == test[i].getID())
                                    {
                                        projectiles.Remove(pairA.Key);
                                        break;
                                    }
                                }
                            }
                        }
                        //CheckTileCollision(pair.Value.getEntities());
                    }
                }
                #endregion

                #region Tower Adding
                //Temporary way to add towers.
                if (((Keyboard.GetState().IsKeyDown(Keys.Space)) && (!map.GetTile(chosenTile).anyTower()) && !SpaceBar) ||
                    ((gamePadState.Buttons.A == ButtonState.Pressed) && (!map.GetTile(chosenTile).anyTower()) && !SpaceBar))
                {
                    SpaceBar = true;
                    //map.GetTile(chosenTile).addEntity(new resource(ref bullet, curResource));

                    int resourceValue = map.GetTile(chosenTile).towerConstruction();
                    resourceValue = 1;

                    tower towerToAdd;
                    if (resourceValue == 0)
                    {

                    }
                    else if (((resourceValue >= 3) && (resourceValue <= 5)) || (curResource == 0))
                    {
                        towerToAdd = new GunTower(ref gunTower, (new Vector3(chosenTile.x * 20, 5, chosenTile.y * 20)), chosenTile);
                        towers.Add(towerToAdd, true);
                        map.GetTile(chosenTile).addEntity(towerToAdd);
                    }
                    else if (((resourceValue >= 6) && (resourceValue <= 9)) || (curResource == 1))
                    {
                        towerToAdd = new CanonTower(ref cannonTower, (new Vector3(chosenTile.x * 20, 5, chosenTile.y * 20)), chosenTile);
                        towers.Add(towerToAdd, true);
                        map.GetTile(chosenTile).addEntity(towerToAdd);
                    }
                    else if (((resourceValue >= 10) && (resourceValue <= 14)) || (curResource == 2))
                    {
                        towerToAdd = new MissileTower(ref missileTower, (new Vector3(chosenTile.x * 20, 5, chosenTile.y * 20)), chosenTile);
                        towers.Add(towerToAdd, true);
                        map.GetTile(chosenTile).addEntity(towerToAdd);
                    }
                    else if (((resourceValue >= 15) && (resourceValue <= 19)) || (curResource == 3))
                    {
                        towerToAdd = new FireTower(ref fireTower, (new Vector3(chosenTile.x * 20, 5, chosenTile.y * 20)), chosenTile);
                        towers.Add(towerToAdd, true);
                        map.GetTile(chosenTile).addEntity(towerToAdd);
                    }
                    else if (((resourceValue >= 20) && (resourceValue <= 23)) || (curResource == 4))
                    {
                        towerToAdd = new ElectricTower(ref electricTower, (new Vector3(chosenTile.x * 20, 5, chosenTile.y * 20)), chosenTile);
                        towers.Add(towerToAdd, true);
                        map.GetTile(chosenTile).addEntity(towerToAdd);
                    }
                    else if (((resourceValue >= 24) && (resourceValue <= 27)) || (curResource == 5))
                    {
                        towerToAdd = new ChickenTower(ref chickenTower, (new Vector3(chosenTile.x * 20, 5, chosenTile.y * 20)), chosenTile);
                        towers.Add(towerToAdd, true);
                        map.GetTile(chosenTile).addEntity(towerToAdd);
                    }

                }
                if ((Keyboard.GetState().IsKeyUp(Keys.Space)) && (Keyboard.GetState().IsKeyUp(Keys.Q)) && (Keyboard.GetState().IsKeyUp(Keys.E)) && (gamePadState.Buttons.A == ButtonState.Released))
                {
                    SpaceBar = false;
                }
                #endregion

                #region Update Ressource Manager
                //gatherzone.update();
                #endregion
            }

            if ((Keyboard.GetState().IsKeyUp(Keys.Space)) && (Keyboard.GetState().IsKeyUp(Keys.Q)) && (Keyboard.GetState().IsKeyUp(Keys.E)) && (gamePadState.Buttons.A == ButtonState.Released))
            {
                SpaceBar = false;
            }
            #endregion

            #region Update Ressource Manager
            gatherzone.update();
            #endregion
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Viewport = MainScreen;

            if (gameState == 0)
            {
                //GraphicsDevice.Clear(Color.Black);
            }
            else if (gameState == 2)
            {
                GraphicsDevice.Clear(Color.Green);
            }
            else if (gameState == 1)
            {


                GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                #region Draw Game Map
                //Draw the Texture world around the game.

                #region Draw Cube World
                // Set the vertex buffer on the GraphicsDevice
                GraphicsDevice.SetVertexBuffer(vertexBuffer);
                //Set object and camera info
                //effect.World = Matrix.Identity;
                effect.World = world;
                //effect.World = worldRotation * worldTranslation * worldRotation;
                effect.View = cameraMain.view;
                effect.Projection = cameraMain.projection;
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
                foreach (KeyValuePair<monster, bool> pair in tiles)
                {
                    pair.Key.DrawModel(cameraMain);
                }
                //Draw the left base
                mainBase.DrawModel(cameraMain);
                //Selected tile
                chosenTile = cameraMain.getCurrentTC();
                SelectionTile.setPosition(new Vector3(chosenTile.x * 20, 2, chosenTile.y * 20));
                SelectionTile.DrawModel(cameraMain);
                #endregion

                #region Draw Monsters, towers, bullets

                gatherzone.draw(cameraMain);

                //Draw Monsters
                foreach (KeyValuePair<monster, bool> pair in monsters)
                {
                    pair.Key.DrawModel(cameraMain);
                }
                //Draws projectiles list
                foreach (KeyValuePair<projectile, bool> pair in projectiles)
                {
                    pair.Key.DrawModel(cameraMain);
                }
                //Draws Tower list
                foreach (KeyValuePair<tower, bool> pair in towers)
                {
                    pair.Key.DrawModel(cameraMain);
                }
                #endregion

                foreach (KeyValuePair<ParticleExplosion, bool> temp in explosions)
                {
                    temp.Key.Draw(cameraMain);
                }
            }
>>>>>>> d44e2d3f26f4791c423e0f7b2474a095103e7b4c
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
                    projectiles.Add(new Bullet(ref bullet, ref boundingBox, position, direction), true);
                    break;
                //missile tower
                case 1:
                    projectiles.Add(new Missile(ref missile, ref boundingBox, position, direction), true);
                    break;
                //chicken tower
                case 2:
                    projectiles.Add(new Egg(ref egg, ref boundingBox, position, direction), true);
                    break;
                //explosions!
                case 3:
                    projectiles.Add(new Explosion(ref explosion, ref boundingBox, position, direction), true);
                    break;

            }

        }
        #endregion

        #region function to check collision
        //Checks for collision between projectile list and monster list
        /*
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
        */
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
                            foreach(KeyValuePair<projectile, bool> pair in projectiles)
                            {
                                if (pair.Key.getID() == listOnTile[i].getID())
                                {
                                    projectiles.Remove(pair.Key);
                                    break;
                                }
                            }

                            foreach (KeyValuePair<monster, bool> pair in monsters)
                            {
                                if (pair.Key.getID() == listOnTile[j].getID())
                                {
                                    pair.Key.life -= 25;
                                    break;
                                }
                            }
                            //monsters.RemoveAt(j);
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
            foreach(KeyValuePair<monster, bool> pair in monsters)
            {
                if (pair.Key.isDead)
                {
                    explosions.Add(new ParticleExplosion(GraphicsDevice,
                               pair.Key.getWorld().Translation,
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
                               explosionEffect), true);
                    monsters.Remove(pair.Key);
                    break;
                    //spriteManager.removeLifeBarsMonsters(j);
                }
            }

            foreach(KeyValuePair<tower, bool> pair in towers)
            {
                if (pair.Key.isDead)
                {
                    explosions.Add(new ParticleExplosion(GraphicsDevice,
                               pair.Key.getWorld().Translation,
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
                               explosionEffect), true);
                    towers.Remove(pair.Key);
                    break;
                }
                
            }
        }


        #endregion

        #region Particle effect Update
        protected void UpdateExplosions(GameTime gameTime)
        {
            // Loop through and update explosions
            foreach(KeyValuePair<ParticleExplosion, bool> pair in explosions)
            {
                pair.Key.Update(gameTime);
                // If explosion is finished, remove it
                if (pair.Key.IsDead)
                {
                    explosions.Remove(pair.Key);
                    break;
                }
            }
        }

        #endregion

        #region XNA Wiki Random Function
        //XNA WIKI Random stuff
        private static int RandomNumber(int min, int max)
        {

            return random.Next(min, max);
        }
        public float RandomBetween(double min, double max)
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
