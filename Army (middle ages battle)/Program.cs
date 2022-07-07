using System;
using System.Data;
using System.Data.SqlClient;
using ConsoleApp2.Soldiers;
using ConsoleApp2.Soldiers.Swordsman;
using ConsoleApp2.Soldiers.SpecialSoldier;


namespace Army__middle_ages_battle_
{
    class Program
    {
        static void Main(string[] args)
        {
            bool SqlLog(SolderBase[] solder, int sold, string action)
            {
                DateTime dateLogo = DateTime.Now;
                string connStr = "Server=DESKTOP-MV43C0T;Database=LogFile;Trusted_Connection=True;";
                string sqlText = String.Format($@"INSERT INTO [dbo].[Log]([Name],[SoldierType],[WeaponType],[HP],[Action],[DateTime]) VALUES ('{solder[sold].Name}','{solder[sold].ToString()}', '{solder[sold].Weapone.ToString()}', {solder[sold].HP}, '{action}', '{dateLogo}');");
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(sqlText, conn))
                    {
                        if (command.ExecuteNonQuery() > 0)
                            return true;
                        else
                            return false;
                    }
                }
            }
            int distance = 10;
            Viking vikingWarrior = new Viking("Hrodger", distance);
            Berserk berserk = new Berserk("Haapernun", distance);
            RomeLegionnaire romeWarrior = new RomeLegionnaire("Ruf", distance);
            Archer archer = new Archer("Lundt", distance);
            RomeLegionnaire romeLancer = new RomeLegionnaire("Tuy", distance);

            SolderBase[] romWarriors = { romeWarrior, archer,  };
            SolderBase[] vikingWarriors = { vikingWarrior, berserk };
            int rome = 0, viking = 0;
                Random romChange = new Random();
                Random VikingChange = new Random();
                rome = romChange.Next(romWarriors.Length);
                viking = VikingChange.Next(vikingWarriors.Length);
            Console.WriteLine($"The warriors came out and move towards each other.");
            SqlLog(vikingWarriors, viking, "begin");
            SqlLog(romWarriors, rome, "begin");
            do
            {
                Console.WriteLine($"{vikingWarriors[viking].Name} hp is {vikingWarriors[viking].HP}\t{romWarriors[rome].Name} hp is {romWarriors[rome].HP}");
                vikingWarriors[viking].Move();
                SqlLog(vikingWarriors, viking, "moves");
                romWarriors[rome].Move();
                SqlLog(romWarriors, rome, "moves");
                if (vikingWarriors[viking].Distance <= vikingWarriors[viking].Weapone.Range) {
                    romWarriors[rome].GetDamage(vikingWarriors[viking]);
                    SqlLog(vikingWarriors, viking, "attacks");
                    Console.WriteLine($"{vikingWarriors[viking].ToString()} {vikingWarriors[viking].Name} with HP {vikingWarriors[viking].HP} attacks at the {romWarriors[rome].ToString()} {romWarriors[rome].Name} with HP {romWarriors[rome].HP}");
                }
                if (romWarriors[rome].Distance <= romWarriors[rome].Weapone.Range)
                {
                    vikingWarriors[viking].GetDamage(romWarriors[rome]);
                    SqlLog(romWarriors, rome, "attacks");
                    Console.WriteLine($"{romWarriors[rome].ToString()} {romWarriors[rome].Name} with HP {romWarriors[rome].HP} attacks at the {vikingWarriors[viking].ToString()} {vikingWarriors[viking].Name} with HP {vikingWarriors[viking].HP}");
                }
            } while (vikingWarriors[viking].HP > 0 && romWarriors[rome].HP > 0);
            if (vikingWarriors[viking].HP > 0)
            {
                SqlLog(vikingWarriors, viking, "wins");
                Console.WriteLine($"{vikingWarriors[viking].ToString()} {vikingWarriors[viking].Name} won!");
            }
            if (romWarriors[rome].HP > 0)
            {
                SqlLog(romWarriors, rome, "wins");
                Console.WriteLine($"{romWarriors[rome].ToString()} {romWarriors[rome].Name} won!");
            }
        }
    }
}
