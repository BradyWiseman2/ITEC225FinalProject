using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    public enum AttacksCs
    {
        PrimaryCooldown = 0,
        PrimaryRecovery = 1,
        SecondaryCooldown = 2,
        SecondaryRecovery = 3,
        UtilityCooldown = 4,
        UtilityRecovery = 5,
        SpecialCooldown = 6,
        SpecialRecovery = 7
    }
    public class AttackEventArg
    {
        public MoveHitbox MoveHitbox { get; set; }

        public AttackEventArg(MoveHitbox moveHitbox)
        {
            MoveHitbox = moveHitbox;
        }
    }
    public class Survivor : Entity
    {

        protected int animdelay;
        protected int animframe = 1;
        protected bool DirectionLocked;
        public List<int> AttackCooldowns = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<MoveHitbox> ImmunityList { get; set; }
        public int CurrentHealth { get; set; }
        int MaxHealth { get; set; }
        int Armor { get; set; }
        public int MoveSpeed { get; set; }
        int DoubleJumps { get; set; }
        int Damage { get; set; } //Subject to change when the damage formula is complete
        public bool MoveLeft { get; set; }
        public bool MoveRight { get; set; }
        public int MaxMoveSpeed { get; set; }
        public bool FacingLeft { get; set; }
        public bool MoveDown { get; set; }
        public int calcDamage { get { return Damage; } }
        public int calcAttackSpeed { get { return 115; } }
       
        public Survivor(int health, int armor, int moveSpeed, int doubleJumps, int damage) 
            : base(0,0, new Bitmap[] {Properties.Resources.TestSprite})
        {
            animdelay = 5;
            MaxHealth = health;
            CurrentHealth = health;
            Armor = armor;
            MoveSpeed = moveSpeed;
            DoubleJumps = doubleJumps;
            Damage = damage;
            MaxMoveSpeed = 10;
            ImmunityList = new List<MoveHitbox>();
           
        }

        public void Move()
        {
            
        }
        
        public void Jump()
        {
            VelocityY = -15;
            Location.Y += VelocityY;
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

        public virtual void UpdateCooldowns()
        {
            for (int j = 0; j < AttackCooldowns.Count; j++)
            {
                if (AttackCooldowns[j] > 0)
                {
                    AttackCooldowns[j]--;
                }
            }
            if (AttackCooldowns[(int)AttacksCs.PrimaryRecovery] == 0
                && AttackCooldowns[(int)AttacksCs.SecondaryRecovery] == 0
                && AttackCooldowns[(int)AttacksCs.UtilityRecovery] == 0
                && AttackCooldowns[(int)AttacksCs.SpecialRecovery] == 0)
            {
                DirectionLocked = false;
            }
                        
        }

        public void ChangeDir(bool left)
        {
            if(!DirectionLocked)
            {
                FacingLeft = left;
               
            }
            
        }

        public void TakeDamage(int amount)
        {
            int damage = amount - Armor;
            if (damage <= 0)
            {
                damage = 1;
            }
            CurrentHealth -= damage;
        }
        protected double GetAttackSpeedReduction()
        {
            return (double)1 / (calcAttackSpeed / (double)100);
        }
        public void anim()
        {
            if(animdelay > 0)
            {
                animdelay--;
            }
            else
            {
                animframe++;
                if(animframe >= SpritesArray.Length)
                {
                    animframe = 1;
                }

                SpritesArray[0] = new Bitmap(SpritesArray[animframe]);
                if (FacingLeft)
                {
                    SpritesArray[0].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                animdelay = 3;
            }
        }

        public Rectangle CameraPos(Bitmap map)
        {
            Rectangle a = new Rectangle(new Point(Location.X - 200 + (ActiveSprite.Width/2),
                Location.Y - 200 + (ActiveSprite.Height/2)),new Size(400,400));
            if (a.X < 0)
            {
                a.X = 0;
            }
            if(a.Y < 0)
            {
                a.Y = 0;
            }
            if(a.X + a.Width > map.Width)
            {
                a.X = map.Width - a.Width;
            }
            if (a.Y + a.Height > map.Height)
            {
                a.Y = map.Height - a.Height;
            }
            return a;
        }

       
        
    }

}


