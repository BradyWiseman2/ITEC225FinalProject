using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    internal class SurvivorFlygon : Survivor
    {

        public SurvivorFlygon() : base(100, 0, 5, 1, 10)
        {
            
            SpritesArray = new Bitmap[]
            {
                
                Properties.Resources.FlygonIdle1,
                Properties.Resources.FlygonIdle1,
                Properties.Resources.FlygonIdle2,
                Properties.Resources.FlygonIdle3,
                Properties.Resources.FlygonIdle4,
                Properties.Resources.FlygonIdle5,
                Properties.Resources.FlygonIdle6,
                Properties.Resources.FlygonIdle7,
            };
            MaxDoubleJumps = 1;
            teamID = 0;
        }

        public override void PrimaryFire()
        {
            if (AttackCooldowns[((int)AttacksCs.PrimaryCooldown)] == 0 && TestRecovery())
            {
                Flamethrower a;
                if (FacingLeft)
                {
                    a = new Flamethrower(this, FacingLeft, Location.X, Location.Y,
                        new Bitmap[] { Properties.Resources.Flamethrower });
                    a.Location.X -= a.ActiveSprite.Width;
                }
                else
                {
                    a = new Flamethrower(this, FacingLeft, Location.X, Location.Y,
                        new Bitmap[] { Properties.Resources.Flamethrower });
                    a.Location.X += ActiveSprite.Width;
                }
                AttackCooldowns[((int)AttacksCs.PrimaryCooldown)] = 0;
                AttackCooldowns[((int)AttacksCs.PrimaryRecovery)] = (int)((double)20 * GetAttackSpeedReduction());
                DirectionLocked = true;
                PrimaryFireWent.Invoke(this, new AttackEventArg(new List<MoveHitbox> {a}));
            }
        }

        public override void SecondaryFire()
        {
            if (AttackCooldowns[((int)AttacksCs.SecondaryCooldown)] == 0 && TestRecovery()) {
                DragonTail a;
                if (FacingLeft)
                {
                    a = new DragonTail(this, FacingLeft, Location.X, Location.Y + (int)((double)ActiveSprite.Height * 0.7),
                        new Bitmap[] { Properties.Resources.DragonTailTest });
                    a.Location.X -= a.ActiveSprite.Width;
                }
                else
                {
                    a = new DragonTail(this, FacingLeft, Location.X, Location.Y + (int)((double)ActiveSprite.Height * 0.7),
                        new Bitmap[] { Properties.Resources.DragonTailTest });
                    a.Location.X += ActiveSprite.Width;
                }
                AttackCooldowns[((int)AttacksCs.SecondaryCooldown)] = 100;
                AttackCooldowns[((int)AttacksCs.SecondaryRecovery)] = 20;
                DirectionLocked = true;
                SecondaryFireWent.Invoke(this, new AttackEventArg(new List<MoveHitbox> { a }));
            }
        }
        public override void Utility()
        {
            if (AttackCooldowns[((int)AttacksCs.UtilityCooldown)] == 0 && TestRecovery())
            {
                switch (UtilityHelper())
                {
                    case 0:                       
                        break;
                    case 1:
                        VelocityY = -(int)(MaxMoveSpeed * 3.5);
                        AttackCooldowns[((int)AttacksCs.UtilityCooldown)] = 100;
                        break;
                    case 2:
                        VelocityY = -(int)(MaxMoveSpeed * 3);
                        VelocityX = MaxMoveSpeed;
                        AttackCooldowns[((int)AttacksCs.UtilityCooldown)] = 100;
                        break;
                    case 3:
                        VelocityX = MaxMoveSpeed * 3;
                        AttackCooldowns[((int)AttacksCs.UtilityCooldown)] = 100;
                        break;
                    case 7:
                        VelocityX = -MaxMoveSpeed * 3;
                        AttackCooldowns[((int)AttacksCs.UtilityCooldown)] = 100;
                        break;
                    case 8:
                        VelocityY = -(int)(MaxMoveSpeed * 3);
                        VelocityX = -MaxMoveSpeed * 3;
                        AttackCooldowns[((int)AttacksCs.UtilityCooldown)] = 100;
                        break;
                }

            }
                
        }
        public override void Special()
        {
            if (AttackCooldowns[((int)AttacksCs.SpecialCooldown)] == 0 && Grounded && TestRecovery())
            {
                MovementLocked = true;

                EarthPower a = new EarthPower(this, FacingLeft, Location.X, Location.Y + (int)((double)ActiveSprite.Height * 0.8),
                    new Bitmap[] { Properties.Resources.EarthPower });
                a.Location.X -= a.ActiveSprite.Width;
                EarthPower b = new EarthPower(this, FacingLeft, Location.X + ActiveSprite.Width, Location.Y + (int)((double)ActiveSprite.Height * 0.8),
                   new Bitmap[] { Properties.Resources.EarthPower });

                AttackCooldowns[((int)AttacksCs.SpecialCooldown)] = 100;
                AttackCooldowns[((int)AttacksCs.SpecialRecovery)] = 20;
                DirectionLocked = true;
                SpecialWent.Invoke(this, new AttackEventArg(new List<MoveHitbox> { a, b }));
            }
        }
        private int UtilityHelper()
        {
            int a = 0;
            if (MoveUp)
            {
                if (MoveRight)
                {
                    a = 2;
                }
                else if (MoveLeft)
                {
                    a = 8;
                }
                else
                {
                    a = 1;
                }
            }
            else if (MoveRight && !Grounded)
            {
                a = 3;
            }
            else if (MoveLeft && !Grounded)
            {
                a = 7;
            }
            return a;
        }

        public delegate void SecondaryFireEvent(object sender, AttackEventArg e);
        public event SecondaryFireEvent SecondaryFireWent;

        public delegate void PrimaryFireEvent(object sender, AttackEventArg e);
        public event PrimaryFireEvent PrimaryFireWent;

        public delegate void SpecialEvent(object sender, AttackEventArg e);
        public event SpecialEvent SpecialWent;

    }
}
