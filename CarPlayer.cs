using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollisionRacer.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CollisionRacer
{
    class CarPlayer : SpriteObject
    {
        private KeyboardState _keyboard;

        public float Direction { get; set; }
        public float Speed { get; set; }

       // private Texture2D SpriteTexture;

        public CarPlayer(GameHost game)
            : base(game)
        {
            // Set the default scale and color
            ScaleX = 1;
            ScaleY = 1;
            SpriteColor = Color.White;
            Speed = 0f;
        }

        public CarPlayer(GameHost game, Vector2 position)
            : this(game)
        {
            // Store the provided position
            Position = position;
        }

        public CarPlayer(GameHost game, Vector2 position, Texture2D texture)
            : this(game, position)
        {
            // Store the provided texture
            SpriteTexture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            _keyboard = Keyboard.GetState();

            if (_keyboard.IsKeyDown(Keys.Up))
            {
                if (Speed <= 0f) Speed += 1f;
                Speed += 0.03f;
                //PositionX += Speed*(float) Math.Cos(Direction);
                //PositionY += Speed*(float) Math.Sin(Direction);

                if (_keyboard.IsKeyDown(Keys.Left))
                {
                    Direction -= 0.04f;
                }
                if (_keyboard.IsKeyDown(Keys.Right))
                {
                    Direction += 0.04f;
                }
            }

            else if (_keyboard.IsKeyDown(Keys.Down))
            {
                Speed -= 0.1f;
                //PositionX -= Speed * (float)Math.Cos(Direction);
                //PositionY -= Speed * (float)Math.Sin(Direction);
                if (_keyboard.IsKeyDown(Keys.Left))
                {
                    Direction += 0.04f;
                }
                if (_keyboard.IsKeyDown(Keys.Right))
                {
                    Direction -= 0.04f;
                }
            }
            else
            {
                if (Speed > 0) Speed -= 0.05f;
                if (_keyboard.IsKeyDown(Keys.Left))
                {
                    Direction -= 0.04f;
                }
                if (_keyboard.IsKeyDown(Keys.Right))
                {
                    Direction += 0.04f;
                }
            }
            PositionX += Speed*(float) Math.Cos(Direction);

            PositionY += Speed*(float) Math.Sin(Direction);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(SpriteTexture, Position, new Rectangle(0,0,SpriteTexture.Width, SpriteTexture.Height), Color.White, Direction, Origin, Scale, SpriteEffects.FlipVertically, 0f );
            //spriteBatch.Draw(SpriteTexture, Position, new Rectangle((int)PositionX, (int)PositionY, SpriteTexture.Width, SpriteTexture.Height), Color.White, Direction, new Vector2(SpriteTexture.Width / 4, SpriteTexture.Height / 2), 1f, SpriteEffects.None, 0f);
            //base.Draw(gameTime, spriteBatch);
        }
    }
}
