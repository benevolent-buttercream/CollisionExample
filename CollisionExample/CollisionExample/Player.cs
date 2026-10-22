using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace CollisionExample
{
    internal class Player
    {
        const int size = 32;

        const float speed = 220f;

        Vector2 position;

        public Rectangle Bounds => new Rectangle((int)MathF.Round(position.X), (int)MathF.Round(position.Y), size, size);

        public bool IsCollidingWith { get; private set; }

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

            //Normalize Direction
            if (direction != Vector2.Zero)
                direction.Normalize();

            IsCollidingWith 

            //Move Player and Clamp to Play Area
            Vector2 movement = direction * speed * deltaTime;
            position += movement;
            position.X = MathHelper.Clamp(position.X, playArea.Left, playArea.Right - size);
            position.Y = MathHelper.Clamp(position.Y, playArea.Top, playArea.Bottom - size);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D whitePixel)
        {
            spriteBatch.Draw(whitePixel, position, Color.LimeGreen);
        }

    }
}
