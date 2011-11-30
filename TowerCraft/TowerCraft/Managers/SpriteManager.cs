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


        
        public SpriteManager(Game game)
            : base(game)
        {
            
        }


        public override void Initialize()
        {

       
            base.Initialize();
        }
        protected override void LoadContent()
        {

            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {


            //base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            
        }


    }
}
