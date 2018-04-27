using System;
using System.Collections.Generic;

namespace TableRush
{
  static class Physics
  {
    public static List<PhysicsObject> Objects = new List<PhysicsObject>();
  }
  public class PhysicsObject
  {
    public int width, height;
    public float x, y;

    public PhysicsObject(int width_, int height_, float x_, float y_)
    {
      width = width_;
      height = height_;
      x = x_;
      y = y_;
      Physics.Objects.Add(this);
    }

    public int IsColliding()
    {
      //check whether physics object is colliding with other physics objects
      for (int i = 0; i < Physics.Objects.Count; i++)
      {
        if (IsCollidingWithObject(Physics.Objects[i]))
        {
          //return index of physics object if colliding
          return i;
        }
      }
      return -1;
    }

    public bool IsCollidingWithObject(PhysicsObject obj)
    {
      //check whether it is colliding with a singular physics object; item to pick up, etc.
      if (obj.x < x + width && obj.x + obj.width > x && obj.y < y + height && obj.height + obj.y > y)
      {
        return true;
      }
      return false;
    }
  }
}