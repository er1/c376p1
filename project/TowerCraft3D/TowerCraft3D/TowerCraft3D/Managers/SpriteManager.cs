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
    public class SpriteManager : DrawableGameComponent
    {
        SpriteBatch batch;

        List<Sprite> monstersLife = new List<Sprite>();
        List<Sprite> towersLife = new List<Sprite>();

        Texture2D life100;
        Texture2D life75;
        Texture2D life50;
        Texture2D life25;
        
        Texture2D HUDL;
        Texture2D HUDM1;
        Texture2D HUDM2;
        Texture2D HUDR;
        
        SpriteFont font;

        int worldHeight;
        int worldWidth;

        int currentDay;
        TimeSpan timer;
        
        public SpriteManager(Game game)
            : base(game)
        {
            
        }


        public override void Initialize()
        {
            worldHeight = ((Game1)Game).worldHeight;
                worldWidth = ((Game1)Game).worldWidth;
            batch = new SpriteBatch(Game.GraphicsDevice);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            life100 = Game.Content.Load<Texture2D>(@"Textures\\life\life100");
            life75 = Game.Content.Load<Texture2D>(@"Textures\\life\life75");
            life50 = Game.Content.Load<Texture2D>(@"Textures\\life\life50");
            life25 = Game.Content.Load<Texture2D>(@"Textures\\life\life25");
            HUDL = Game.Content.Load<Texture2D>(@"Textures\\SCHUD\\Starcraft1");
            HUDM1 = Game.Content.Load<Texture2D>(@"Textures\\SCHUD\\Starcraft2");
            HUDM2 = Game.Content.Load<Texture2D>(@"Textures\\SCHUD\\Starcraft3");
            HUDR = Game.Content.Load<Texture2D>(@"Textures\\SCHUD\\Starcraft4");
            font = Game.Content.Load<SpriteFont>(@"Font\\GameFont");
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
           
            //base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            batch.Begin();
            base.Draw(gameTime);
            batch.Draw(HUDL, new Vector2(0, worldHeight/3*(1.40f)), null, Color.White, 0f, new Vector2(0,0), 0.75f, SpriteEffects.None, 0);
            batch.Draw(HUDM1, new Vector2(worldWidth / 4, worldHeight / 3 * (1.40f)), null, Color.White, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0);
            batch.Draw(HUDM2, new Vector2(worldWidth/4*2f, worldHeight / 3 * (1.40f)), null, Color.White, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0);
            batch.Draw(HUDR, new Vector2(worldWidth / 4 * 3f, worldHeight / 3 * (1.40f)), null, Color.White, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0);
            batch.DrawString(font, "Day " + currentDay, new Vector2(worldWidth / 4 * 1.75f, worldHeight / 3 * 2.40f), Color.Green);
            batch.DrawString(font, "Life: " + ((Game1)Game).LIFE, new Vector2(worldWidth/4*1.75f, worldHeight / 3 *2.50f), Color.Green);
            batch.DrawString(font, "Time " + timer.Minutes.ToString() +" m "+timer.Seconds.ToString() + " s", new Vector2(worldWidth / 4 * 1.75f, worldHeight / 3 * 2.60f), Color.Green);
            
            for (int i = 0; i < monstersLife.Count; i++)
            {
                monstersLife[i].Draw(batch);
            }

            batch.End();
            
        }

        #region Lifebar stuff for Monsters
        //Add a life bar to the monster life var list
        public void addLifeBarsMonsters(Vector2 pos)
        {
            monstersLife.Add(new Sprite(ref life100,  pos));
        }
        //Updates the life bar texture
        public void updateLifeBarsMonsters(int i,int percentage, Vector3 position, Camera cam, Viewport viewport)
        {
            Vector3 posi = viewport.Project(position, cam.projection, cam.view, Matrix.Identity);
            Vector2 pos = new Vector2(posi.X, posi.Y) + new Vector2(-20,-20);
            //pos = new Vector2(0, 0);
            if (percentage == 100)
                monstersLife[i].Update(ref life100, pos);
            if (percentage == 75)
                monstersLife[i].Update(ref life75, pos);
            if (percentage == 50)
                monstersLife[i].Update(ref life50, pos);
            if (percentage == 25)
                monstersLife[i].Update(ref life25, pos);
        }
        //Removes the life bar sprite from the list when the monster died
        public void removeLifeBarsMonsters(int i)
        {
            monstersLife.RemoveAt(i);
        }
        #endregion

        #region UPDATE HUD STUFF
        public void drawHUD(TimeSpan time, int day)
        {
            timer = time;
            currentDay = day;
        }
        #endregion
    }
}
