using System.Drawing;
using System.Numerics;
using System.Security.Cryptography.Pkcs;

namespace ITEC225FinalProject
{
    public partial class Form1 : Form
    {
        Renderer Test;
        List<Entity> entities = new List<Entity>();
        SurvivorFlygon STest = new SurvivorFlygon();
        SurvivorFlygon STest2 = new SurvivorFlygon();
        MonsterHakamo MTest1 = new MonsterHakamo();
        MonsterKommo MTest2 = new MonsterKommo();
        List<Monster> monsters = new List<Monster>();
        Bitmap ActiveCollision = Properties.Resources.TestCollision5;
        Color Ground = Properties.Resources.CollisionKey.GetPixel(2, 0);
        Movement movement = new Movement();
        List<Entity> deletionlist = new List<Entity>();
        Bitmap Game = new Bitmap(Properties.Resources.TestCollision5);
        public Form1()
        {
            InitializeComponent();
            Test = new Renderer();
            Test.Background = Properties.Resources.TestVisual5;
            entities.Add(STest);
            entities.Add(STest2);
            entities.Add(MTest1);
            monsters.Add(MTest1);
            entities.Add(MTest2);
            monsters.Add(MTest2);
            STest.Location.X = 1100;
            STest2.Location.X = 1000;
            MTest1.Location.X = 100;
            MTest2.Location.X = 150;
            movement.ActiveCollision = ActiveCollision;
            STest2.Sprites[0] = Properties.Resources.TestSprite2;
            STest.SecondaryFireWent += AddMoveHitbox;
            STest.PrimaryFireWent += AddMoveHitbox;
            STest.SpecialWent += AddMoveHitbox;
            MTest1.PrimaryFireWent += AddMoveHitbox;
            MTest2.PrimaryFireWent += AddMoveHitbox;
        }

        private void AddMoveHitbox(object sender, AttackEventArg e)
        {
            foreach (MoveHitbox hitbox in e.MoveHitbox)
            {
                entities.Add(hitbox);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            CollisionTest();

            foreach (Entity entity in entities)
            {
                if (entity is Monster)
                {
                    if ((entity as Monster).Target == null || (entity as Monster).Target.CurrentHealth < 1)
                    {
                        (entity as Monster).FindTarget(entities);
                    }
                    movement.MonsterMovement((entity as Monster));

                }
                if (entity is Survivor)
                {
                    if ((entity as Survivor).CurrentHealth < 1)
                    {
                        deletionlist.Add(entity);
                    }
                    (entity as Survivor).UpdateCooldowns();
                    (entity as Survivor).anim();
                    movement.PlayerVelocity((entity as Survivor));
                }
                if (entity is MoveHitbox)
                {
                    (entity as MoveHitbox).activeticks--;
                    if ((entity as MoveHitbox).activeticks == 0)
                    {
                        deletionlist.Add(entity as MoveHitbox);
                    }
                }


            }
            foreach(Monster a in monsters)
            {
                movement.MonsterAttack(a);
            }
            
            entities.RemoveAll(entity => deletionlist.Contains(entity));
            deletionlist.Clear();
            movement.ApplyVelocity(entities);
            movement.Gravity(entities);
            foreach (Entity entity in entities)
            {

                if (entity is MoveHitbox)
                {
                    (entity as MoveHitbox).UpdatePosition();
                }

            }


            Game = Test.RenderFrame(entities);
            Bitmap P1 = Game.Clone(STest.CameraPos(ActiveCollision), Game.PixelFormat);



            pBoxMain.Image = P1;
            label1.Text = STest.VelocityX.ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:

                    STest.MoveLeft = true;
                    break;
                case Keys.Right:

                    STest.MoveRight = true;
                    break;
                case Keys.Up:
                    STest.MoveUp = true;
                    break;
                case Keys.Down:
                    STest.SecondaryFire();
                    break;
                case Keys.NumPad1:
                    STest.PrimaryFire();
                    break;
                case Keys.NumPad2:
                    STest.SecondaryFire();
                    break;
                case Keys.NumPad3:
                    STest.Utility();
                    break;
                case Keys.NumPad4:
                    STest.Special();
                    break;
                case Keys.A:
                    STest2.MoveLeft = true;
                    break;
                case Keys.D:
                    STest2.MoveRight = true;
                    break;
                case Keys.W:
                    STest2.MoveUp = true;

                    break;
                case Keys.S:
                    STest2.MoveDown = true;
                    break;

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    STest.MoveLeft = false;
                    break;
                case Keys.Right:
                    STest.MoveRight = false;
                    break;
                case Keys.Up:
                    if (STest.Grounded == false && STest.VelocityY < -3)
                    {
                        STest.VelocityY = -3;
                    }
                    STest.MoveUp = false;
                    STest.JumpedThisUpPress = false;
                    break;
                case Keys.A:
                    STest2.MoveLeft = false;
                    break;
                case Keys.D:
                    STest2.MoveRight = false;
                    break;
                case Keys.W:
                    if (STest2.Grounded == false && STest2.VelocityY < -3)
                    {
                        STest2.VelocityY = -3;
                    }
                    STest2.MoveUp = false;
                    STest2.JumpedThisUpPress = false;
                    break;
                case Keys.S:
                    STest2.MoveDown = false;
                    break;
            }
        }

        private void CollisionTest()
        {
            foreach (Entity a in entities)
            {
                if (a is MoveHitbox)
                {
                    foreach (Entity b in entities)
                    {
                        if (a.Location.X < b.Location.X + b.ActiveSprite.Width &&
                            a.Location.X + a.ActiveSprite.Width > b.Location.X &&
                            a.Location.Y < b.Location.Y + b.ActiveSprite.Height &&
                            a.Location.Y + a.ActiveSprite.Height > b.Location.Y
                            && b is not MoveHitbox
                            && (b as Survivor).TeamID != (a as MoveHitbox).Owner.TeamID
                            && !(b as Survivor).ImmunityList.Contains(a))                            
                        {
                            (a as MoveHitbox).Collision(b as Survivor);
                            (b as Survivor).ImmunityList.Add(a as MoveHitbox);
                        }
                    }
                }
            }
        }

    }
}