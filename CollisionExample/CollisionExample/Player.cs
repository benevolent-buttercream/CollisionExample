using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CollisionDetectionExample
{
    internal class Player
    {
        const int size = 32;
        const float speed = 220f;

        Vector2 position;

        public Rectangle Bounds => new Rectangle((int)MathF.Round(position.X), (int)MathF.Round(position.Y), size,size);

        public bool IsColliding { get; private set; }

        public Player(Vector2 startPosition)
        {
            this.position = startPosition;
        }

        public void Update(float deltaTime, Wall[] walls, Rectangle playArea)
        {
            //Button Inputs
            KeyboardState keyboard = Keyboard.GetState();
           
            Vector2 direction = Vector2.Zero;
            if (keyboard.IsKeyDown(Keys.W)) direction.Y -= 1;
            if (keyboard.IsKeyDown(Keys.S)) direction.Y += 1;
            if (keyboard.IsKeyDown(Keys.A)) direction.X -= 1;
            if (keyboard.IsKeyDown(Keys.D)) direction.X += 1;

            //Normalize direction
            if (direction != Vector2.Zero)
                direction.Normalize();

            IsColliding = false;
            //Move player Clamp to play area
            Vector2 movement = direction * speed * deltaTime;

            //Movement plus Collsion Detection
            int steps = Math.Max(1, (int)MathF.Ceiling(MathF.Max(MathF.Abs(movement.X), MathF.Abs(movement.Y))/8f));
            Vector2 step = movement / steps;


            for(int i = 0; i < steps; i++)
            {
                position.X += step.X;

                foreach (Wall wall in walls)
                {
                    if(step.X != 0 && Bounds.Intersects(wall.Bounds))
                    {
                        IsColliding = true;
                        position.X = step.X >0 ? wall.Bounds.Left - size : wall.Bounds.Right;
                    }
                }

                position.Y += step.Y;

                foreach (Wall wall in walls)
                {
                    if (step.Y != 0 && Bounds.Intersects(wall.Bounds))
                    {
                        IsColliding = true;
                        position.Y = step.Y > 0 ? wall.Bounds.Top - size : wall.Bounds.Bottom;
                    }
                }
            }


            //position += movement;
            position.X = MathHelper.Clamp(position.X, playArea.Left, playArea.Right - size);
            position.Y = MathHelper.Clamp(position.Y, playArea.Top, playArea.Bottom - size);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D whitePixel)
        {
            spriteBatch.Draw(whitePixel, Bounds, IsColliding ? Color.Tomato : Color.LimeGreen);
        }
    }
}
