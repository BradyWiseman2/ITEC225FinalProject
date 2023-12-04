using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC225FinalProject
{
    public interface Item
    {
        protected int Amount { get; set; } //When getting an item, the game will first check if the survivor already possesses one. If so,
                                  //this will be incremented instead of adding another. 
        public string Name { get; }
        public string Description { get;}
        public double HealthBoost { get { return 0; } }
        public double DamageBoost { get { return 0; } }
        public double SpeedBoost { get { return 0; } }
        public double ArmorBoost { get { return 0; } }
        //Yes, having every item have variables for every stat looks kinda ugly, however, it
        //allows far more freedom if I wanted to add more items later that hypothetically
        //could increase multiple stats.

        public void IncrementAmount()
        {
            Amount++;
        }

    }
    public class Protein : Item
    {
        public int Amount {get; set;}
        public string Name { get { return "Protein"; } }
        public string Description { get { return "Boosts damage"; } }
        public double DamageBoost { get { return (Amount * 0.1) + 1; } }

        public Protein()
        {        
            Amount = 1;
        }
    }
}
