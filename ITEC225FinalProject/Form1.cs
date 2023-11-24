namespace ITEC225FinalProject
{
    public partial class Form1 : Form
    {
        Renderer Test;
        List<Entity> entities = new List<Entity>();
        Survivor STest = new Survivor(100, 0, 8, 0, 100);
        Survivor STest2 = new Survivor(100, 0, 8, 0, 100);
        Bitmap ActiveCollision = Properties.Resources.TestCollision3;
        Color Ground = Properties.Resources.CollisionKey.GetPixel(2, 0);
        Movement movement = new Movement();
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            foreach(Entity entity in entities)
            {
                if(entity is Survivor)
                {
                    movement.MovementPlayer((entity as Survivor));
                }
            }         
            movement.Gravity(entities);

           
            pBoxMain.Image = Test.RenderFrame(entities);

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
                    if (STest.Grounded == false && STest.Acceleration < -3)
                    {
                        STest.Acceleration = -3;
                    }
                    break;
                case Keys.A:
                    STest2.MoveLeft = false;
                    break;
                case Keys.D:
                    STest2.MoveRight = false;
                    break;
                case Keys.W:
                    if (STest2.Grounded == false && STest2.Acceleration < -3)
                    {
                        STest2.Acceleration = -3;
                    }
                    break;
                case Keys.S:
                    STest2.MoveDown = false;
                    break;
            }
        }
             
    }
}