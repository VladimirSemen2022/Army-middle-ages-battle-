using System;
using ConsoleApp2.Weapone.TwoHande;

namespace ConsoleApp2.Soldiers.SpecialSoldier
{
    class Archer : SolderBase
    {
        public Archer(string name, int dist) : base(name, 15, 4, dist, new Bow())
        { }
        public override void GetDamage(SolderBase solder)
        {
                Random rand = new Random();
                this.HP -= rand.Next(solder.Weapone.Damage) + 1;
                if (this.HP <= 0)
            {
                this.HP = 0;
                Dead();
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
