using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TableRush.Desktop
{


  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D ballTexture;
    Vector2 ballPosition;
    float ballSpeed;
    NPC firstNPC;
    NPC secondNPC;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      //graphics.IsFullScreen = true;
      //graphics.PreferredBackBufferHeight = 800;
      //graphics.PreferredBackBufferWidth = 1280;
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
      ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
      ballSpeed = 100f;

      firstNPC = new NPC(graphics);
      secondNPC = new NPC(graphics);

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

      ballTexture = Content.Load<Texture2D>("ball");

      firstNPC.Load(Content.Load<Texture2D>("ball"));
      secondNPC.Load(Content.Load<Texture2D>("ball"));
         
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
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
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      var kstate = Keyboard.GetState();

      if (kstate.IsKeyDown(Keys.Up))
        ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (kstate.IsKeyDown(Keys.Down))
        ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (kstate.IsKeyDown(Keys.Left))
        ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (kstate.IsKeyDown(Keys.Right))
        ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

      ballPosition.X = Math.Min(Math.Max(ballTexture.Width / 2, ballPosition.X), graphics.PreferredBackBufferWidth - ballTexture.Width / 2);
      ballPosition.Y = Math.Min(Math.Max(ballTexture.Height / 2, ballPosition.Y), graphics.PreferredBackBufferHeight - ballTexture.Height / 2);

      firstNPC.Update(gameTime, graphics);
      secondNPC.Update(gameTime, graphics);

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();

      spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);

      firstNPC.Draw(spriteBatch);
      secondNPC.Draw(spriteBatch);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
