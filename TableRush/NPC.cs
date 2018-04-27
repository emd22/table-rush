using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TableRush
{
  class NPC
  {
    private Texture2D squareTexture;
    private Texture2D pixel;
    private Vector2 squarePosition;
    private float npcSpeed;
    private int npcMovementCounter;
    private int npcMovementDirection;
    public PhysicsObject physicsObject;
    private int physicsStuckCounter;
    private int lastCollisionId;

    public NPC(GraphicsDeviceManager graphics)
    {
      squarePosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
      physicsObject = new PhysicsObject(0, 0, 0, 0);
      npcSpeed = 40f;
      npcMovementCounter = 0;
      physicsStuckCounter = 0;
      lastCollisionId = 0;
    }

    public void Load(Texture2D squareTexture, GraphicsDevice graphicsDevice)
    {
      this.squareTexture = squareTexture;
      pixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
      pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it
    }

    private void HasCollided(GameTime gameTime, GraphicsDeviceManager graphics, Random rnd)
    {

      //avoid the ai randomly picking a direction
      if (npcMovementCounter == 60) { npcMovementCounter = 59; }

      //up/down/left/right
      if (npcMovementDirection == 1 || npcMovementDirection == 3) {
        npcMovementDirection++;
      }
      else if (npcMovementDirection == 2 || npcMovementDirection == 4)
      {
        npcMovementDirection--;
      }
      //diagonals
      else if (npcMovementDirection == 5 || npcMovementDirection == 7)
      {
        npcMovementDirection++;
      }
      else if (npcMovementDirection == 6 || npcMovementDirection == 8)
      {
        npcMovementDirection--;
      }
    }

    //just for if you want to test it with bounding boxes on
    /*private void DrawBoundingRectangle(SpriteBatch spriteBatch, int x, int y, int w, int h)
    {
      spriteBatch.Draw(pixel, new Rectangle(x, y, w, 1), Color.Crimson);

      // Draw left line
      spriteBatch.Draw(pixel, new Rectangle(x, y, 1, h), Color.Crimson);

      // Draw right line
      spriteBatch.Draw(pixel, new Rectangle((x + w - 1),
                                      y,
                                      1,
                                      h), Color.Crimson);
      // Draw bottom line
      spriteBatch.Draw(pixel, new Rectangle(x,
                                      y + h - 1,
                                      w,
                                      1), Color.Crimson);
    }*/

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
      
      physicsObject.width = squareTexture.Width;
      physicsObject.height = squareTexture.Height;
      physicsObject.x = squarePosition.X - (physicsObject.width / 2);
      physicsObject.y = squarePosition.Y - (physicsObject.height / 2);

      int collisionId = 0;

      if ((collisionId = physicsObject.IsColliding()) != -1)
      {
        if (physicsStuckCounter++ == 0)
        {
          lastCollisionId = collisionId;
          HasCollided(gameTime, graphics, rnd);
        }
      }
      else if (collisionId != lastCollisionId)
      {
        physicsStuckCounter = 0;
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(squareTexture, squarePosition, null, Color.White, 0f, new Vector2(squareTexture.Width / 2, squareTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
      //DrawBoundingRectangle(spriteBatch, (int)physicsObject.x, (int)physicsObject.y, physicsObject.width, physicsObject.height);
    }
  }
}
