using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    public class Renderer
    {
        public Bitmap Background;
       
        public Bitmap RenderFrame(List<Entity> entities)
        {
            Bitmap a = new Bitmap(Background);
            using (Graphics g = Graphics.FromImage(a))
            {
                if (entities != null)
                {
                    foreach (Entity entity in entities)
                    {
                        g.DrawImage(entity.ActiveSprite, entity.Location);
                    }
                }
            }
            GC.Collect();
            return a;
        }

    }
    public class Entity
    {
        public Point Location;

        public Bitmap[] SpritesArray;
        public bool Grounded;
        public int Acceleration { get; set; }

        public Bitmap ActiveSprite { get { return SpritesArray[0]; } }
        public Bitmap[] Sprites
        {
            get { return SpritesArray; }
        }


        public Entity(double locationX, double locationY, Bitmap[] sprites)
        {
            Location = new Point((int)locationX, (int)locationY);
            SpritesArray = sprites;           
        }
       
    }
}
