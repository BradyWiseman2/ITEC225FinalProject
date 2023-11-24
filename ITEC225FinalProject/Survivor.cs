using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    public class Survivor : Entity
    {
        
        int Health { get; set; }
        int Armor { get; set; }
        public int MoveSpeed { get; set; }
        int DoubleJumps { get; set; }
        int Damage { get; set; } //Subject to change when the damage formula is complete
        public bool MoveLeft { get; set; }
        public bool MoveRight { get; set; }

        public bool MoveDown { get; set; }
        public Survivor(int health, int armor, int moveSpeed, int doubleJumps, int damage) 
            : base(0,0, new Bitmap[] {Properties.Resources.TestSprite})
        {
            Health = health;
            Armor = armor;
            MoveSpeed = moveSpeed;
            DoubleJumps = doubleJumps;
            Damage = damage;
        }

        public void Jump()
        {
            Acceleration = -15;
            Location.Y += Acceleration;
            Grounded = false;
        }

        public virtual void PrimaryFire()
        {

        }

        public virtual void SecondaryFire()
        {

        }

        public virtual void Utility()
        {

        }

        public virtual void Special()
        {

        }
    }
}
