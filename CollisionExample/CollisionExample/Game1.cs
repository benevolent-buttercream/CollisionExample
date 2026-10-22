using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CollisionExample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D whitePixel;
        Player player;
        Wall[] Walls;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "WASD to Move Collision Example - ESC to Quit";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player(new Vector2(80, 80));

            //Set up walls
            Walls = new Wall[]
            {
                new Wall (new Rectangle(200, 100, 40, 300)),
                new Wall (new Rectangle(360, 220, 260, 40)),
                new Wall (new Rectangle(400, 300, 40, 150)),
                new Wall (new Rectangle(480, 80, 200, 40))
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            whitePixel.SetData(new Color[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.Update(deltaTime, GraphicsDevice.Viewport.Bounds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            player.Draw(_spriteBatch, whitePixel);

            //draw the walls
            foreach (var wall in Walls)
            {
                wall.Draw(_spriteBatch, whitePixel);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
