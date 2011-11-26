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
        Viewport viewport;
        Map map;
        Camera cam;
        player character;
        Model MinecraftLikeModel;
        Model Monster1;
        Model bullet;
        Model tile;
        Model colony;
        Model gunTower;
        int currentWave;
        int worldSize;
        static Random random = new Random();
        #region Cube World Variables
        BasicEffect effect;
        protected VertexBuffer vertexBuffer;
        Matrix world = Matrix.Identity;
        Matrix worldTranslation = Matrix.Identity;
        Matrix worldRotation = Matrix.Identity;
        world worldBox;
        Texture2D boxTexture;
        #endregion
        List<monster> monsters = new List<monster>();
        List<monster> tiles = new List<monster>();
        List<projectile> projectiles = new List<projectile>();
        //First level Waves
        List<waveManager> wavesLevel1 = new List<waveManager>();
        List<tower> towers = new List<tower>();

        TileCoord chosenTile;

        Colony mainBase;

        public ModelManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            viewport = ((Game1)Game).MainScreen;
            cam = ((Game1)Game).cameraMain;
            worldSize = ((Game1)Game).worldSize;

            chosenTile = ((Game1)Game).cameraMain.getCurrentTC();

            base.Initialize();
        }
        protected override void  LoadContent()
        {
            boxTexture = Game.Content.Load<Texture2D>(@"Textures\\background\\back512");
            worldBox = new world();
            
            #region world stuff
            worldBox.setCubeVertices(worldSize);
            // Set vertex data in VertexBuffer
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), worldBox.getCubeVertices().Length, BufferUsage.None);
            vertexBuffer.SetData(worldBox.getCubeVertices());
            // Initialize the BasicEffect
            effect = new BasicEffect(GraphicsDevice);
            #endregion
            MinecraftLikeModel = Game.Content.Load<Model>(@"Models\\Char\\Char");
            Monster1 = Game.Content.Load<Model>(@"Models\\Monster1\\monster1");
            tile = Game.Content.Load<Model>(@"Models\\Map\\Tile");
            colony = Game.Content.Load<Model>(@"Models\\Map\\Colony");
            bullet = Game.Content.Load<Model>(@"Models\\Bullet\\bullet");
            gunTower = Game.Content.Load<Model>(@"Models\\Towers\\GunTower\\GunTower");
            character = new player(ref MinecraftLikeModel, new Vector3(0, -worldSize+1, 0), worldSize);

            //LOAD WAVE information for Level1
            wavesLevel1.Add(new waveManager(1,20,TimeSpan.FromMinutes(2.0),TimeSpan.FromSeconds(3.0)));

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
            base.LoadContent();
        }
        
        
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            #region Update Level1
            //Level 1
            if (currentWave < wavesLevel1.Count)
            {
                wavesLevel1[currentWave].UpdateWave(gameTime);

                //Check if this Waves Timer is done or monsters are all dead (so wave is done)
                if ((wavesLevel1[currentWave].levelTimer <= TimeSpan.Zero && wavesLevel1[currentWave].spawn <= 0)
                    || wavesLevel1[currentWave].spawn <= 0)
                {
                    //Game.Exit();
                    currentWave++;
                }
                //If Level isn't done then check the Timer to add monsters at invervals
                else if (wavesLevel1[currentWave].canSpawn && wavesLevel1[currentWave].spawn > 0)
                {
                    wavesLevel1[currentWave].spawn--;
                    wavesLevel1[currentWave].canSpawn = false;
                    monsters.Add(new monster(ref Monster1, new Vector3(-390 + 1, 0, RandomNumber(-80, 80)), new Vector3(1, 0, 0)));
                }
            }
            #endregion

            //update Map
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                tiles[i].Update();
            }
            mainBase.Update();
            //Selected tile
            chosenTile = ((Game1)Game).cameraMain.getCurrentTC();
            tiles[tiles.Count - 1].Update();
            //KeyboardState keyStatus = Keyboard.GetState();
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                towers.Add(new tower(ref gunTower, (new Vector3(chosenTile.x*20, 0, chosenTile.y*20))));
            }

            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].Update();
                // HIT THE COLONY NOT FINISHED ( REMOVE LIFE AND BLAH BLAH)
                if (monsters[i].hitColony)
                {
                    monsters.RemoveAt(i);
                    i--;
                }
            }
             //Draws Tower list
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].Update();
                if (towers[i].iWantToShoot(gameTime))
                {
                    addProject(towers[i].getPosition()+ new Vector3(0,1,0), new Vector3(-1, 0, 0));
                }
            }
            //updates projectile list
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Viewport = viewport;
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
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
            //Draw Player
            character.DrawModel(cam);

            //Draw Map
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].DrawModel(cam);
            }
            mainBase.DrawModel(cam);
            //Selected tile
            chosenTile = ((Game1)Game).cameraMain.getCurrentTC();
            tiles[tiles.Count - 1].setPosition(new Vector3(chosenTile.x*20, 2, chosenTile.y*20));

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
            
            base.Draw(gameTime);
        }

        //Add projectiles to the List(to update - check collision - draw)
        public void addProject(Vector3 position, Vector3 direction)
        {
            //Remember to change Model for diff bullets
            projectiles.Add(new projectile(ref bullet, position, direction));
        }
        public void CheckCollision()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                for (int j = 0; j < monsters.Count; j++)
                {
                    if (projectiles[i].IsCollision(monsters[j]))
                    {
                        projectiles.RemoveAt(i);
                        monsters.RemoveAt(j);
                        i--;
                        j--;
                    }
                }
            }
        }


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
