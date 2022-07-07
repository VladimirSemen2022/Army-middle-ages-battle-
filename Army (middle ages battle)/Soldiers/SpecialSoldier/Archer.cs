using System;
using ConsoleApp2.Weapone.TwoHande;

namespace ConsoleApp2.Soldiers.SpecialSoldier
{
    class Archer : SolderBase
    {
        public Archer(string name, int dist) : base(name, 18, 4, dist, new Bow())
        { }
        public override void GetDamage(SolderBase solder)
        {
            if (this.Distance <= solder.Weapone.Range)
            {
                base.GetDamage(solder);
                Random rand = new Random();
                this.HP -= rand.Next(solder.Weapone.Damage) + 1;
                if (this.HP <= 0 && solder.HP > 0)
                {
                    solder.GetDamage(this);
                    Dead();
                }
            }
        }

        public override void Move()
        {
            if (this.Distance > 2 && this.HP > 0)
            {
                Console.WriteLine($"Archer {Name} stands at the point and waiting for enemies!");
                this.Distance -= 2;
            }
            else if (this.HP > 0)
            {
                this.Distance = 0;
            }
        }
        public override string ToString()
        {
            return "archer";
        }
    }
}
