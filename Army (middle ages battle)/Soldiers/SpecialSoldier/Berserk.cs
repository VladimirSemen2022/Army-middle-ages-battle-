﻿using System;
using ConsoleApp2.Weapone.OneHande;

namespace ConsoleApp2.Soldiers.SpecialSoldier
{
    class Berserk : SolderBase
    {
        public Berserk(string name, int dist) : base(name, 10, 7, dist, new OneHandeBlade())
        { }

        public override void GetDamage(SolderBase solder)
        {
            if (this.Distance <= solder.Weapone.Range)
            {
                base.GetDamage(solder);
                Random rand = new Random();
                this.HP -= rand.Next(solder.Weapone.Damage)+1;
                if (this.HP <= 0 && solder.HP > 0)
                {
                    solder.GetDamage(this);
                    Dead();
                }
            }
        }

        public override void Move()
        {
            if (this.Distance > 2)
            {
                Console.WriteLine($"Berserk {this.Name} walks to the enemy warriors!");
                this.Distance -= 2;
            }
            else
            {
                this.Distance = 0;
                Console.WriteLine($"Berserk {this.Name} is near the enemy warriors and attack !");
            }
        }
        public override string ToString()
        {
            return "berserk";
        }
    }
}
