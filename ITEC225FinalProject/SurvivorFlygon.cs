using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    internal class SurvivorFlygon : Survivor
    {

        public SurvivorFlygon() : base(100, 0, 2, 1, 10)
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
        }

        public override void PrimaryFire()
        {
            if (AttackCooldowns[((int)AttacksCs.PrimaryCooldown)] == 0 && AttackCooldowns[((int)AttacksCs.PrimaryRecovery)] == 0)
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
                PrimaryFireWent.Invoke(this, new AttackEventArg(a));
            }
        }

        public override void SecondaryFire()
        {
            if (AttackCooldowns[((int)AttacksCs.SecondaryCooldown)] == 0) {
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
                SecondaryFireWent.Invoke(this, new AttackEventArg(a));
            }
        }
        public delegate void SecondaryFireEvent(object sender, AttackEventArg e);
        public event SecondaryFireEvent SecondaryFireWent;

        public delegate void PrimaryFireEvent(object sender, AttackEventArg e);
        public event PrimaryFireEvent PrimaryFireWent;
        
    }
}
