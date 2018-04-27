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
    public const float PLAYER_SPEED = 75f;
    public const int NPC_COUNT = 10;

    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D playerTexture;
    Vector2 playerPosition;
    PhysicsObject playerPhysics;
    Random rnd = new Random();
    NPC[] npcs = new NPC[NPC_COUNT];

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      graphics.IsFullScreen = true;
      graphics.PreferredBackBufferWidth = 1280; graphics.PreferredBackBufferHeight = 800;
      Content.RootDirectory = "Content";
    }

    protected override void OnActivated(object sender, EventArgs args)
    {
      Window.Title = "Table Rush";
      base.OnActivated(sender, args);
    }

    protected override void OnDeactivated(object sender, EventArgs args)
    {
      Window.Title = "Table Rush (Not Active)";
      base.OnDeactivated(sender, args);
    }
    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      playerPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
      for (int i = 0; i < NPC_COUNT; i++) { npcs[i] = new NPC(graphics); }
      base.Initialize();
      playerPhysics = new PhysicsObject(playerTexture.Width, playerTexture.Height, 0f, 0f);
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      playerTexture = Content.Load<Texture2D>("ball");
      foreach (NPC npc in npcs) { npc.Load(Content.Load<Texture2D>("ball"), GraphicsDevice); }
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
      if (IsActive)
      {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
          Exit();

        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Up))
          playerPosition.Y -= PLAYER_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (kstate.IsKeyDown(Keys.Down))
          playerPosition.Y += PLAYER_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (kstate.IsKeyDown(Keys.Left))
          playerPosition.X -= PLAYER_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (kstate.IsKeyDown(Keys.Right))
          playerPosition.X += PLAYER_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;

        playerPosition.X = Math.Min(Math.Max(playerTexture.Width / 2, playerPosition.X), graphics.PreferredBackBufferWidth - playerTexture.Width / 2);
        playerPosition.Y = Math.Min(Math.Max(playerTexture.Height / 2, playerPosition.Y), graphics.PreferredBackBufferHeight - playerTexture.Height / 2);

        playerPhysics.x = playerPosition.X - (playerTexture.Width / 2);
        playerPhysics.y = playerPosition.Y - (playerTexture.Height / 2);

        foreach (NPC npc in npcs) { npc.Update(gameTime, graphics, rnd); }

        base.Update(gameTime);
      }
    }
    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();
      spriteBatch.Draw(playerTexture, playerPosition, null, Color.White, 0f, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
      foreach (NPC npc in npcs) { npc.Draw(spriteBatch); }
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
