using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    public class Movement
    {
        public Bitmap ActiveCollision { get; set;}
        Color Ground = Properties.Resources.CollisionKey.GetPixel(2, 0);

        public void MovementPlayer(Survivor a)
        {
            if (a.MoveLeft && ActiveCollision.GetPixel(a.Location.X - 1, a.Location.Y) != Ground
               && ActiveCollision.GetPixel(a.Location.X - 1, a.Location.Y + a.ActiveSprite.Height) != Ground)
            {
                int i = a.Location.X - a.MoveSpeed;
                while (ActiveCollision.GetPixel(i, a.Location.Y) == Ground ||
                    ActiveCollision.GetPixel(i, a.Location.Y + a.ActiveSprite.Height) == Ground)
                {
                    i++;
                }
                a.Location.X = i;
            }
            if (a.MoveRight && ActiveCollision.GetPixel(a.Location.X + a.ActiveSprite.Width + 1, a.Location.Y) != Ground
                && ActiveCollision.GetPixel(a.Location.X + a.ActiveSprite.Width + 1, a.Location.Y + a.ActiveSprite.Height) != Ground)
            {

                int i = a.Location.X + a.MoveSpeed;
                while (ActiveCollision.GetPixel(i + a.ActiveSprite.Width, a.Location.Y) == Ground ||
                    ActiveCollision.GetPixel(i + a.ActiveSprite.Width, a.Location.Y + a.ActiveSprite.Height) == Ground)
                {
                    i--;
                }
                a.Location.X = i;

            }
            if (a.MoveDown)
            {
                a.Acceleration += 2;
            }
        }

        public void Gravity(List<Entity> a)
        {
            foreach (Entity e in a)
            {
                if (ActiveCollision.GetPixel(e.Location.X, e.Location.Y + e.ActiveSprite.Height + 1) == Ground ||
                   ActiveCollision.GetPixel(e.Location.X + e.ActiveSprite.Width, e.Location.Y + e.ActiveSprite.Height + 1) == Ground)
                {
                    e.Acceleration = 0;
                    e.Grounded = true;
                }
                else
                {
                    if (e.Acceleration < 35)
                    {
                        e.Acceleration++;
                    }
                    e.Grounded = false;
                }


                int b = e.Location.Y += e.Acceleration;

                while (ActiveCollision.GetPixel(e.Location.X, b + e.ActiveSprite.Height) == Ground ||
                    ActiveCollision.GetPixel(e.Location.X + e.ActiveSprite.Width, b + e.ActiveSprite.Height) == Ground)
                {
                    b--;
                }
                e.Location.Y = b;
            }
        }
    }
}
