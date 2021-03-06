using System;
using ConsoleApp2.Weapone.OneHande;

namespace ConsoleApp2.Soldiers.SpecialSoldier
{
    class LanceKnight : SolderBase
    {
        public LanceKnight(string name, int dist) : base(name, 16, 4, dist, new Lance())
        { }

        public override void GetDamage(SolderBase solder)
        {
            if (this.Distance <= solder.Weapone.Range)
            {
                base.GetDamage(solder);
                Random rand = new Random();
                this.HP -= rand.Next(solder.Weapone.Damage) + 1;
                if (this.HP <= 0)
                {
                    this.HP = 0;
                    Dead();
                }
            }
        }

        public override void Move()
        {
            if (this.Distance > 2)
            {
                Console.WriteLine($"Lance knight {this.Name} walks to the enemy warriors!");
                this.Distance -= 2;
            }
            else
            {
                this.Distance = 0;
                Console.WriteLine($"Lance knight {this.Name} is near the enemy warriors and attack !");
            }
        }
        public override string ToString()
        {
            return "lancer knight";
        }
    }
}
