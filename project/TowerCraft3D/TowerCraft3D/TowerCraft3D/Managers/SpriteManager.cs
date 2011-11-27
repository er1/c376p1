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

        public SpriteManager(Game game)
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
            batch = new SpriteBatch(Game.GraphicsDevice);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            life100 = Game.Content.Load<Texture2D>(@"Textures\\life\life100");
            life75 = Game.Content.Load<Texture2D>(@"Textures\\life\life75");
            life50 = Game.Content.Load<Texture2D>(@"Textures\\life\life50");
            life25 = Game.Content.Load<Texture2D>(@"Textures\\life\life25");
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            batch.Begin();
            for (int i = 0; i < monstersLife.Count; i++)
            {
                monstersLife[i].Draw(batch);
            }
            batch.End();
            base.Draw(gameTime);
        }


        public void addLifeBarsMonsters(Vector2 pos)
        {
            monstersLife.Add(new Sprite(ref life100,  pos));
        }
        public void updateLifeBarsMonsters(int i,int percentage, Vector3 position, Camera cam, Viewport viewport)
        {
            Vector3 posi = viewport.Project(position, cam.projection, cam.view, Matrix.Identity);
            Vector2 pos = new Vector2(posi.X, posi.Y);
            pos = new Vector2(0, 0);
            if (percentage == 100)
                monstersLife[i].Update(ref life100, pos);
            if (percentage == 75)
                monstersLife[i].Update(ref life75, pos);
            if (percentage == 50)
                monstersLife[i].Update(ref life50, pos);
            if (percentage == 25)
                monstersLife[i].Update(ref life25, pos);
        }
        public void removeLifeBarsMonsters(int i)
        {
            monstersLife.RemoveAt(i);
        }

    }
}
