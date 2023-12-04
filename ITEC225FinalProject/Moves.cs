using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    public class MoveHitbox : Entity
    {
        public Survivor Owner { get; set; }
        public bool left { get; set; }
        public int activeticks { get; set; }

        public MoveHitbox(Survivor owner, bool lefty, int locX, int locY, Bitmap[] Bitmaps) : base(locX, locY, Bitmaps)
        {
            left = lefty;
            Owner = owner;
        }

        public virtual void Collision(Survivor a)
        {

        }

        public virtual void UpdatePosition()
        {

        }


    }

    public class DragonTail : MoveHitbox
    {

        public DragonTail(Survivor owner, bool lefty, int locX, int locY, Bitmap[] Bitmaps) : base(owner, lefty, locX, locY, Bitmaps)
        {
            activeticks = 20;
        }
        public override void Collision(Survivor a)
        {

            if (left)
            {
                a.VelocityX = -30;

            }
            else
            {
                a.VelocityX = 30;
            }
            a.TakeDamage(50);
        }
        public override void UpdatePosition()
        {
            if (left)
            {
                Location.X = Owner.Location.X - ActiveSprite.Width;
                Location.Y = Owner.Location.Y + (int)((double)Owner.ActiveSprite.Height * 0.7);
            }
            else
            {
                Location.X = Owner.Location.X + Owner.ActiveSprite.Width;
                Location.Y = Owner.Location.Y + (int)((double)Owner.ActiveSprite.Height * 0.7);
            }
        }
    }

    public class Flamethrower : MoveHitbox
    {
        public Flamethrower(Survivor owner, bool lefty, int locX, int locY, Bitmap[] Bitmaps) : base(owner, lefty, locX, locY, Bitmaps)
        {
            activeticks = 20;

        }
        public override void UpdatePosition()
        {
            if (left)
            {
                Location.X -= 10;
            }
            else
            {
                Location.X += 10;
            }
        }
        public override void Collision(Survivor a)
        {
            a.TakeDamage(Owner.calcDamage);
            activeticks = 1;
        }


    }
    public class EarthPower : MoveHitbox
    {
        public EarthPower(Survivor owner, bool lefty, int locX, int locY, Bitmap[] Bitmaps) : base(owner, lefty, locX, locY, Bitmaps)
        {
            activeticks = 40;

        }
        public override void UpdatePosition()
        {

        }
        public override void Collision(Survivor a)
        {
            a.TakeDamage(Owner.calcDamage * 3);
            a.VelocityY -= 10;
            a.Location.Y -= 5;
            a.Grounded = false;
        }
    }
    public class DragonClaw : MoveHitbox
    {
        public DragonClaw(Survivor owner, bool lefty, int locX, int locY, Bitmap[] Bitmaps) : base(owner, lefty, locX, locY, Bitmaps)
        {
            activeticks = 20;
        }
        public override void UpdatePosition()
        {
            if (left)
            {
                Location.X = Owner.Location.X - ActiveSprite.Width;
                Location.Y = Owner.Location.Y;
            }
            else
            {
                Location.X = Owner.Location.X + Owner.ActiveSprite.Width;
                Location.Y = Owner.Location.Y;
            }
        }
        public override void Collision(Survivor a)
        {
            a.TakeDamage(Owner.calcDamage * 3);
            if (left)
            {
                a.VelocityX = -10;

            }
            else
            {
                a.VelocityX = 10;
            }

        }
    }
}
