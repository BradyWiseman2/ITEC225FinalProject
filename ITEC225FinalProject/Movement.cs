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
     
        public void PlayerVelocity(Survivor a)
        {
            if(a.MoveLeft && Math.Abs(a.VelocityX) < a.MaxMoveSpeed & !a.IsMovementLocked)
            {              
                a.VelocityX -= 2;
                a.ChangeDir(true);
            }
            else if (a.MoveRight && Math.Abs(a.VelocityX) < a.MaxMoveSpeed && !a.IsMovementLocked)
            {
                a.ChangeDir(false);
                a.VelocityX += 2;
            }
            else if(a.IsMovementLocked || Math.Abs(a.VelocityX) > a.MaxMoveSpeed || a.MoveLeft != true && a.VelocityX < 0 || a.MoveRight != true && a.VelocityX > 0)
            {
                int sub = Math.Abs(a.VelocityX / 10);
                if(sub <= 0)
                {
                    sub = 1;
                }

                if(a.VelocityX > 0)
                {
                    a.VelocityX-=sub;
                }
                if(a.VelocityX < 0)
                {
                    a.VelocityX+=sub;
                }
            }
            if (a.MoveUp)
            {
                a.Jump();

            }
        }

        public void MonsterMovement(Monster a)
        {
            if (a.Target.Location.X - a.Location.X > 0 && a.TargetDistance > a.AttackRange -3)
            {
                a.MoveRight = true;
                a.MoveLeft = false;
           
            }
            else if (a.TargetDistance > a.AttackRange -3)
            {
                a.MoveRight = false;
                a.MoveLeft = true;
               
            }
            else
            {
                a.MoveRight = false;
                a.MoveLeft = false;
            }
            
                if (ActiveCollision.GetPixel(a.Location.X + a.ActiveSprite.Width + 10, a.Location.Y) == Ground
                || ActiveCollision.GetPixel(a.Location.X + a.ActiveSprite.Width + 10, a.Location.Y + a.ActiveSprite.Height) == Ground)
                {
                  a.MoveUp = true;
                }
            
           
               else if (ActiveCollision.GetPixel(a.Location.X - 10, a.Location.Y) == Ground
               || ActiveCollision.GetPixel(a.Location.X - 10, a.Location.Y + a.ActiveSprite.Height) == Ground)
                {
                a.MoveUp = true;
                }
            else
            {
                a.MoveUp = false;
                a.JumpedThisUpPress = false;
            }
                
        }

        public void MonsterAttack(Monster a)
        {
            if(a.TargetDistance < a.AttackRange)               
            {
                a.PrimaryFire();
            }
        }

        public void Gravity(List<Entity> a)
        {
            foreach (Entity e in a)
            {
                if (e is not MoveHitbox)
                {
                    if (ActiveCollision.GetPixel(e.Location.X, e.Location.Y + e.ActiveSprite.Height + 1) == Ground ||
                       ActiveCollision.GetPixel(e.Location.X + e.ActiveSprite.Width, e.Location.Y + e.ActiveSprite.Height + 1) == Ground)
                    {
                        e.VelocityY = 0;
                        e.Grounded = true;
                        if(e is Survivor)
                        {
                            (e as Survivor).RefreshDoubleJumps();
                        }
                        
                    }
                    else
                    {
                        if (e.VelocityY < 35)
                        {
                            e.VelocityY++;
                        }
                        e.Grounded = false;
                    }


                    int b = e.Location.Y += e.VelocityY;

                    while (ActiveCollision.GetPixel(e.Location.X, b + e.ActiveSprite.Height) == Ground ||
                        ActiveCollision.GetPixel(e.Location.X + e.ActiveSprite.Width, b + e.ActiveSprite.Height) == Ground)
                    {
                        b--;
                    }
                    e.Location.Y = b;
                }
            }
        }

        public void ApplyVelocity(List<Entity> e)
        {
            foreach (Entity a in e)
            {

                if (a.VelocityX > 0) //Moving Right
                {
                    if (ActiveCollision.GetPixel(a.Location.X + a.ActiveSprite.Width + 1, a.Location.Y) != Ground
                    && ActiveCollision.GetPixel(a.Location.X + a.ActiveSprite.Width + 1, a.Location.Y + a.ActiveSprite.Height) != Ground)
                    {

                        int i = a.Location.X + a.VelocityX;
                        while (ActiveCollision.GetPixel(i + a.ActiveSprite.Width, a.Location.Y) == Ground ||
                            ActiveCollision.GetPixel(i + a.ActiveSprite.Width, a.Location.Y + a.ActiveSprite.Height) == Ground)
                        {
                            i--;
                        }
                        a.Location.X = i;
                    }
                }
                else if (a.VelocityX < 0) //Moving Left
                {
                    if (ActiveCollision.GetPixel(a.Location.X - 1, a.Location.Y) != Ground
                   && ActiveCollision.GetPixel(a.Location.X - 1, a.Location.Y + a.ActiveSprite.Height) != Ground)
                    {
                        int i = a.Location.X + a.VelocityX;
                        while (ActiveCollision.GetPixel(i, a.Location.Y) == Ground ||
                            ActiveCollision.GetPixel(i, a.Location.Y + a.ActiveSprite.Height) == Ground)
                        {
                            i++;
                        }
                        a.Location.X = i;
                    }
                }
            }
        }
        
    }
}
