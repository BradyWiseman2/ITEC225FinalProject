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
        Survivor STest2 = new Survivor(100, 10, 8, 0, 100);
        Bitmap ActiveCollision = Properties.Resources.TestCollision3;
        Color Ground = Properties.Resources.CollisionKey.GetPixel(2, 0);
        Movement movement = new Movement();
        List<Entity> deletionlist = new List<Entity>();
        Bitmap Game = new Bitmap(Properties.Resources.TestCollision3);
        public Form1()
        {
            InitializeComponent();
            Test = new Renderer();
            Test.Background = Properties.Resources.TestCollision3;
            entities.Add(STest);
            entities.Add(STest2);
            STest.Location.X = 1100;
            movement.ActiveCollision = ActiveCollision;
            STest2.Sprites[0] = Properties.Resources.TestSprite2;
            STest.SecondaryFireWent += STest_SecondaryFireWent;
            STest.PrimaryFireWent += STest_PrimaryFireWent;
        }

        private void STest_PrimaryFireWent(object sender, AttackEventArg e)
        {
            entities.Add(e.MoveHitbox);
        }

        private void STest_SecondaryFireWent(object sender, AttackEventArg e)
        {

            entities.Add(e.MoveHitbox);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            CollisionTest();

            foreach (Entity entity in entities)
            {
                if (entity is Survivor)
                {
                    if ((entity as Survivor).CurrentHealth < 1)
                    {
                        deletionlist.Add(entity);
                    }
                    (entity as Survivor).UpdateCooldowns();
                    
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

            STest.anim();
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
            Bitmap P1 = Game.Clone(STest.CameraPos(ActiveCollision),Game.PixelFormat);
           
            
            pBoxMain.Image = P1;

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
                    if (STest.Grounded)
                    {
                        STest.Jump();
                    }
                    break;
                case Keys.Down:
                    STest.PrimaryFire();
                    break;
                case Keys.A:
                    STest2.MoveLeft = true;
                    break;
                case Keys.D:
                    STest2.MoveRight = true;
                    break;
                case Keys.W:
                    if (STest2.Grounded)
                    {
                        STest2.Jump();
                    }
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
                            && b != (a as MoveHitbox).Owner
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