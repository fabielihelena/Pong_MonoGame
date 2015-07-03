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

namespace First
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        const int PADDLE_OFFSET = 70;
        const float BALL_START_SPEED = 6f;
        Player player1;
        Player player2;

        Input input;

        Ball ball;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            input = new Input();

            player1 = new Player();
            player2 = new Player();
            ball = new Ball();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player1.Texture = Content.Load<Texture2D>("Content/Paddle");
            player2.Texture = Content.Load<Texture2D>("Content/Paddle");

            player1.Position = new Vector2(PADDLE_OFFSET, ScreenHeight/2 - player1.Texture.Height/2);
            player2.Position = new Vector2(ScreenWidth - player2.Texture.Width - PADDLE_OFFSET, ScreenHeight / 2 - player2.Texture.Height / 2);

            ball.Texture = Content.Load<Texture2D>("Content/Ball");
            font = Content.Load<SpriteFont>("Content/font");
            ball.Launch(BALL_START_SPEED);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //KeyboardState input_1 = Keyboard.GetState();
            input.Update();

            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ball.Move(ball.Velocity);
            ball.CheckWallCollision();

            player1.Behaviour(input.Left_axis);
            player2.Behaviour(input.Right_axis);

            if (player1.AABBIntersect(ball.Position.X, ball.Position.Y, ball.Texture.Width, ball.Texture.Height) || player2.AABBIntersect(ball.Position.X, ball.Position.Y, ball.Texture.Width, ball.Texture.Height))
            {
                ball.Velocity.X *= -1;
            }

            if(ball.Position.X + ball.Texture.Width < 0)
            {
                ball.Launch(BALL_START_SPEED);
				player2.score++;
            }

            if (ball.Position.X > ScreenWidth)
            {
                ball.Launch(BALL_START_SPEED);
				player1.score++;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.DrawString(font, ("Player 1: " + player1.score + "	||	Player 2: " + player2.score), Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
