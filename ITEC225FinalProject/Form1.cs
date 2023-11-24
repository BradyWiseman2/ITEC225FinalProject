namespace ITEC225FinalProject
{
    public partial class Form1 : Form
    {
        Renderer Test;
        List<Entity> entities = new List<Entity>();
        Survivor STest = new Survivor(100, 0, 8, 0, 100);
        bool MoveLeft;
        bool MoveRight;
        Bitmap ActiveCollision = Properties.Resources.TestCollision3;
        Color Ground = Properties.Resources.CollisionKey.GetPixel(2, 0);
        Movement movement = new Movement();
        public Form1()
        {
            InitializeComponent();
            Test = new Renderer();
            Test.Background = Properties.Resources.TestCollision3;
            entities.Add(STest);
            STest.Location.X = 1100;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            if (MoveLeft && ActiveCollision.GetPixel(STest.Location.X - 1, STest.Location.Y) != Ground
                && ActiveCollision.GetPixel(STest.Location.X - 1, STest.Location.Y + STest.ActiveSprite.Height) != Ground)
            {
                int a = STest.Location.X - STest.MoveSpeed;
                while (ActiveCollision.GetPixel(a, STest.Location.Y) == Ground ||
                    ActiveCollision.GetPixel(a, STest.Location.Y + STest.ActiveSprite.Height) == Ground)
                {
                    a++;
                }
                STest.Location.X = a;
            }
            if (MoveRight && ActiveCollision.GetPixel(STest.Location.X + STest.ActiveSprite.Width + 1, STest.Location.Y) != Ground
                && ActiveCollision.GetPixel(STest.Location.X + STest.ActiveSprite.Width + 1, STest.Location.Y + STest.ActiveSprite.Height) != Ground)
            {

                int a = STest.Location.X + STest.MoveSpeed;
                while (ActiveCollision.GetPixel(a + STest.ActiveSprite.Width, STest.Location.Y) == Ground ||
                    ActiveCollision.GetPixel(a + STest.ActiveSprite.Width, STest.Location.Y + STest.ActiveSprite.Height) == Ground)
                {
                    a--;
                }
                STest.Location.X = a;
               
            }

            Gravity(entities);

           
            pBoxMain.Image = Test.RenderFrame(entities);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    MoveLeft = true;
                    break;
                case Keys.Right:
                    MoveRight = true;
                    break;
                case Keys.Up:
                    if (STest.Grounded)
                    {
                        STest.Jump();
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    MoveLeft = false;
                    break;
                case Keys.Right:
                    MoveRight = false;
                    break;
                case Keys.Up:
                    if (STest.Grounded == false && STest.Acceleration < -3)
                    {
                        STest.Acceleration = -3;
                    }
                    break;
            }
        }

        private void Gravity(List<Entity> a)
        {
            foreach (Entity e in a)
            {
                if (ActiveCollision.GetPixel(e.Location.X, e.Location.Y + e.ActiveSprite.Height + 1) == Ground ||
                   ActiveCollision.GetPixel(e.Location.X + e.ActiveSprite.Width, e.Location.Y + e.ActiveSprite.Height + 1) == Ground)
                {
                    e.Acceleration = 0;
                    e.Grounded = true;
                }
                else
                {
                    if (e.Acceleration < 30)
                    {
                        e.Acceleration++;
                    }
                    e.Grounded = false;
                }


                 int b = e.Location.Y += e.Acceleration;

                while (ActiveCollision.GetPixel(e.Location.X, b + e.ActiveSprite.Height) == Ground ||
                    ActiveCollision.GetPixel(e.Location.X + e.ActiveSprite.Width, b + e.ActiveSprite.Height) == Ground)
                {
                    b--;
                }
                e.Location.Y = b;
            }
        }
    }
}