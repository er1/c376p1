using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace c376a2
{
    class MenuScreen : ThinkDraw
    {
        public static SpriteFont font;
        bool lastEscState = true;
        bool lastUpState = true;
        bool lastDownState = true;

        bool lastBackState = true;
        float lastYState = 0;
        int menuIndex = 0;

        public MenuScreen()
        {

        }
        public void think(GameTime gt)
        {
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) && !lastBackState) {
                Switcher.game.Exit();
            }

            float currentYstate = (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y);

            if (currentYstate < -0.5f && lastYState >= -0.5f)
            {
                menuIndex = Math.Max(menuIndex - 1, 0);
            }

            if (currentYstate > 0.5f && lastYState <= 0.5f)
            {
                menuIndex = Math.Min(menuIndex + 1, 2);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !lastEscState)
            {
                Switcher.game.Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && !lastUpState)
            {
                menuIndex = Math.Max(menuIndex - 1, 0);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && !lastDownState)
            {
                menuIndex = Math.Min(menuIndex + 1, 2);
            }

            lastEscState = Keyboard.GetState().IsKeyDown(Keys.Escape);
            lastUpState = Keyboard.GetState().IsKeyDown(Keys.Up);
            lastDownState = Keyboard.GetState().IsKeyDown(Keys.Down);
            lastBackState = (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed);
            lastYState = currentYstate;
        }
        public void draw(SpriteBatch sb)
        {
            string content;
            Color color;
            Vector2 size;

            sb.Begin();
            sb.Draw(NormalVariant.skygrad, new Rectangle(0, 0, (int)(Switcher.size.X), (int)(Switcher.size.Y)), null, Color.White);

            content = "Normal Mode";
            color = (menuIndex == 0) ? Color.Red : Color.White;
            size = font.MeasureString(content);
            sb.DrawString(font, content, (Switcher.size - size) / 2 - new Vector2(0, Switcher.size.Y / 6), color);

            content = "Alternate Mode";
            color = (menuIndex == 1) ? Color.Red : Color.White;
            size = font.MeasureString(content);
            sb.DrawString(font, content, (Switcher.size - size) / 2, color);

            content = "LOL Mode";
            color = (menuIndex == 2) ? Color.Red : Color.White;
            size = font.MeasureString(content);
            sb.DrawString(font, content, (Switcher.size - size) / 2 + new Vector2(0, Switcher.size.Y / 6), color);


            sb.End();
        }
        public ThinkDraw next()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed))
            {
                ThinkDraw g;
                switch (menuIndex)
                {
                    case 0:
                        g = new NormalVariant();
                        ((NormalVariant)g).init(Switcher.game, Switcher.size);
                        break;
                    case 1:
                        g = new StoryScreen();
                        break;
                    case 2:
                        g = new DebugVariant();
                        ((NormalVariant)g).init(Switcher.game, Switcher.size);
                        break;
                    default:
                        g = new NormalVariant();
                        ((NormalVariant)g).init(Switcher.game, Switcher.size);
                        break;
                }
                
                return g;
            }

            return null;
        }
    }
}

