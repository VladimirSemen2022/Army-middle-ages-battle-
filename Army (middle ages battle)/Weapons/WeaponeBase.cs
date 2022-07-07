namespace ConsoleApp2.Weapone
{
    abstract class WeaponeBase
    {
        public int Range { get; set; }
        public int Damage { get; set; }
        private WeaponeBase()
        { }

        protected WeaponeBase(int range, int damage)
        {
            Damage = damage;
            Range = range;
        }

        public virtual int SendDamage() { return Damage; }
    }
}
