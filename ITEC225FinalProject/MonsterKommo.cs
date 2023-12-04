using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    internal class MonsterKommo : Monster
    {
        public MonsterKommo() : base(200, 0, 1, 0, 20)
        {

            SpritesArray = new Bitmap[]
            {

                Properties.Resources.KommoWalk1,
                Properties.Resources.KommoWalk1,
                Properties.Resources.KommoWalk2,
                Properties.Resources.KommoWalk3,
                Properties.Resources.KommoWalk4,

            };
            MaxDoubleJumps = 0;
            AttackRange = 25;
            teamID = 1;
        }
        public override void PrimaryFire()
        {
            if (AttackCooldowns[((int)AttacksCs.PrimaryCooldown)] == 0)
            {
                DragonClaw a;
                if (FacingLeft)
                {
                    a = new DragonClaw(this, FacingLeft, Location.X, Location.Y,
                        new Bitmap[] { Properties.Resources.Flamethrower });
                    a.Location.X -= a.ActiveSprite.Width;
                }
                else
                {
                    a = new DragonClaw(this, FacingLeft, Location.X, Location.Y,
                        new Bitmap[] { Properties.Resources.Flamethrower });
                    a.Location.X += ActiveSprite.Width;
                }
                AttackCooldowns[((int)AttacksCs.PrimaryCooldown)] = 50;
                AttackCooldowns[((int)AttacksCs.PrimaryRecovery)] = 20;
                DirectionLocked = true;
                MovementLocked = true;
                PrimaryFireWent.Invoke(this, new AttackEventArg(new List<MoveHitbox> { a }));
            }
        }
        public delegate void PrimaryFireEvent(object sender, AttackEventArg e);
        public event PrimaryFireEvent PrimaryFireWent;
    }
}
