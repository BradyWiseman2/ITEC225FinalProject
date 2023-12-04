using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ITEC225FinalProject.SurvivorFlygon;

namespace ITEC225FinalProject
{
    public class Monster : Survivor
    {
        public int TargetDistance { get { return Math.Abs(Target.Location.X - Location.X ); } }
        protected static Random random = new Random();
        public int AttackRange { get; set; }
        public Survivor Target { get; set; } //The monster will try and move toward this target
        public Monster(int health, int armor, int moveSpeed, int doubleJumps, int damage) 
            : base(health, armor , moveSpeed, doubleJumps, damage)
    
        {

            SpritesArray = new Bitmap[]
            {

                Properties.Resources.HakamoWalk1,
                Properties.Resources.HakamoWalk1,
                Properties.Resources.HakamoWalk2,
                Properties.Resources.HakamoWalk3,
                Properties.Resources.HakamoWalk4,

            };
            MaxDoubleJumps = 0;
            
        }
        public void FindTarget(List<Entity> list)
        {
            List<Survivor> targets = new List<Survivor>();
            foreach (Entity e in list) 
            { 
                if(e is Survivor && e is not Monster) targets.Add((e as Survivor));
            }
            Target = targets[random.Next(targets.Count)];
        }
    }
    public class MonsterHakamo : Monster
    {
        public MonsterHakamo() : base(100, 0, 2, 0, 10)
        {

            SpritesArray = new Bitmap[]
            {

                Properties.Resources.HakamoWalk1,
                Properties.Resources.HakamoWalk1,
                Properties.Resources.HakamoWalk2,
                Properties.Resources.HakamoWalk3,
                Properties.Resources.HakamoWalk4,

            };
            MaxDoubleJumps = 0;
            AttackRange = 30;
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
