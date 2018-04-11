using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TableRush
{
  class NPC
  {
    private Texture2D squareTexture;
    private Vector2 squarePosition;
    private float squareSpeed;
    private int squareMovementCounter;
    private int squareMovementDirection;
    Random rnd = new Random();

    public NPC(GraphicsDeviceManager graphics)
    {
      squarePosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
      squareSpeed = 75f;
      squareMovementCounter = 0;
    }

    public void Load(Texture2D squareTexture)
    {
      this.squareTexture = squareTexture;
    }

    public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
    {
      if (squareMovementCounter == 60)
      {
        squareMovementDirection = rnd.Next(1, 10);
        squareMovementCounter = 0;
      }

      if (squareMovementDirection == 1)
      {
        squarePosition.Y -= squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (squareMovementDirection == 2)
      {
        squarePosition.Y += squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (squareMovementDirection == 3)
      {
        squarePosition.X -= squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (squareMovementDirection == 4)
      {
        squarePosition.X += squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      // diagonals
      if (squareMovementDirection == 5)
      {
        squarePosition.Y += squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        squarePosition.X += squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (squareMovementDirection == 6)
      {
        squarePosition.Y -= squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        squarePosition.X -= squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (squareMovementDirection == 7)
      {
        squarePosition.Y += squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        squarePosition.X -= squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (squareMovementDirection == 8)
      {
        squarePosition.Y -= squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        squarePosition.X += squareSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      squarePosition.X = Math.Min(Math.Max(squareTexture.Width / 2, squarePosition.X), graphics.PreferredBackBufferWidth - squareTexture.Width / 2);
      squarePosition.Y = Math.Min(Math.Max(squareTexture.Height / 2, squarePosition.Y), graphics.PreferredBackBufferHeight - squareTexture.Height / 2);

      squareMovementCounter++;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(squareTexture, squarePosition, null, Color.White, 0f, new Vector2(squareTexture.Width / 2, squareTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
    }
  } 
}
