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
    class DebugVariant : NormalVariant
    {

        public DebugVariant() {
        }

        public override void think(GameTime gt)
        {
            base.think(gt);
            foreach (Ent e in entities.Ents)
            {
                if (e is EntText)
                    entities.queueRemove(e);
            }
            entities.dequeAll();


            Epc.lives = float.PositiveInfinity;
            //hab1 = hab2 = hab3 = true;

            double currentTime = gt.TotalGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Z) || (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed))
            {
                if ((currentTime - lastFired) > 200)
                {
                    lastFired = currentTime;

                    for (double i = 0; i < 2 * Math.PI; i += Math.PI / 20)
                    {
                        // create bullet and fire it;
                        EntBullet Ebullet = new EntBullet(Epc, i);
                        entities.add(Ebullet);
                    }
                }

            }

            if (entities.Ents.Count < 100)
            {
                if (Ent.rand.NextDouble() < 0.01)
                {
                    double angle = Ent.rand.NextDouble() * Math.PI * 2;

                    EntBalloonSet Eballoonset = new EntBalloonSet(Vector2.One * -EntBalloonSet.defaultSprite.Width);
                    Eballoonset.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 4;
                    entities.add(Eballoonset);
                }

                if (Ent.rand.NextDouble() < 0.01)
                {
                    EntAirBalloon e = new EntAirBalloon();
                    entities.add(e);
                }
            }



            /*
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {

                EntBalloon Eballoon = new EntBalloon();
                entities.add(Eballoon);

                Eballoon.position = Epc.position;
                Eballoon.velocity = Vector2.Zero;

            }


            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                if ((currentTime - lastFired) > 200)
                {
                    lastFired = currentTime;

                    EntBalloonSet Eballoonset = new EntBalloonSet(Epc.position);
                    entities.add(Eballoonset);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                if ((currentTime - lastFired) > 200)
                {
                    lastFired = currentTime;

                    EntAirBalloon e = new EntAirBalloon();
                    entities.add(e);

                }
            }
            */

            if (Keyboard.GetState().IsKeyDown(Keys.G) || (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed))
            {
                if ((currentTime - lastFired) > 200)
                {
                    lastFired = currentTime;
                    EntPaperPlane e = new EntPaperPlane();
                    entities.add(e);

                }
            }

        }
    }
}
