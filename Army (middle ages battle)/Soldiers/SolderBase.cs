using System;
using ConsoleApp2.Weapone;

namespace ConsoleApp2.Soldiers
{
    abstract public class SolderBase
    {
        public string Name { get; }
        public int HP { get; set; }
        public int DamageLevel { get; set; }

        public int Distance { get; set; }

        public WeaponeBase Weapone { get; private set; }

        private SolderBase()
        {}

        protected SolderBase(string name, int hp, int damage, int distance, WeaponeBase weapone)
        {
            DamageLevel = damage;
            HP = hp;
            Weapone = weapone;
            Name = name;
            Distance = distance;
        }

        public abstract void Move();
        public virtual void GetDamage(SolderBase solder)
        {
            Console.WriteLine($"{solder.Name} send damage to {Name}!");
        }
        public virtual void Dead()
        {
            Console.WriteLine($"{Name} dead");
        }
    }
}
