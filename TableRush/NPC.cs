using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TableRush
{
  class NPC
  {
    private Texture2D squareTexture;
    private Vector2 squarePosition;
    private float npcSpeed;
    private int npcMovementCounter;
    private int npcMovementDirection;

    public NPC(GraphicsDeviceManager graphics)
    {
      squarePosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
      npcSpeed = 40f;
      npcMovementCounter = 0;
    }

    public void Load(Texture2D squareTexture)
    {
      this.squareTexture = squareTexture;
    }

    public void Update(GameTime gameTime, GraphicsDeviceManager graphics, Random rnd)
    {
      if (npcMovementCounter == 60)
      {
        npcMovementDirection = rnd.Next(1, 11);
        npcMovementCounter = 0;
      }

      if (npcMovementDirection == 1)
      {
        squarePosition.Y -= npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (npcMovementDirection == 2)
      {
        squarePosition.Y += npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (npcMovementDirection == 3)
      {
        squarePosition.X -= npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (npcMovementDirection == 4)
      {
        squarePosition.X += npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      // diagonals
      if (npcMovementDirection == 5)
      {
        squarePosition.Y += npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        squarePosition.X += npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (npcMovementDirection == 6)
      {
        squarePosition.Y -= npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        squarePosition.X -= npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (npcMovementDirection == 7)
      {
        squarePosition.Y += npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        squarePosition.X -= npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (npcMovementDirection == 8)
      {
        squarePosition.Y -= npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        squarePosition.X += npcSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      squarePosition.X = Math.Min(Math.Max(squareTexture.Width / 2, squarePosition.X), graphics.PreferredBackBufferWidth - squareTexture.Width / 2);
      squarePosition.Y = Math.Min(Math.Max(squareTexture.Height / 2, squarePosition.Y), graphics.PreferredBackBufferHeight - squareTexture.Height / 2);

      npcMovementCounter++;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(squareTexture, squarePosition, null, Color.White, 0f, new Vector2(squareTexture.Width / 2, squareTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
    }
  }
}
