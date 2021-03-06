using System;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using ConsoleApp2.Soldiers;
using ConsoleApp2;
using ConsoleApp2.Soldiers.Swordsman;
using ConsoleApp2.Soldiers.SpecialSoldier;


namespace Army__middle_ages_battle_
{
    class Program
    {
        delegate void LogFile(SolderBase[] solder, int sold, string action);    //Создание делегата под требуемый метод
        static void Main(string[] args)
        {   
            //Создание класса Singleton (одиночка) для реализации подключения к базе данных SQL
            SQLConnection singleton = SQLConnection.GetInstance("Server=DESKTOP-MV43C0T;Database=LogFile;Trusted_Connection=True;");
            LogFile log = singleton.SaveLog;   //Передача делегату метода логирования записей в базу SQL


            int distance = 10; //Первоначальная дистация между воинами
            //Создание разных типов воинов-викингов
            Viking vikingWarrior = new Viking("Hrodger", distance);
            Berserk berserk = new Berserk("Haapernun", distance);
            
            //Создание разных типов римских воинов
            RomeLegionnaire romeWarrior = new RomeLegionnaire("Ruf", distance);
            Archer archer = new Archer("Lundt", distance);
            LanceKnight knight = new LanceKnight("Romul", distance);

            //Случайный выбор типов воинов для каждой из сторон
            SolderBase[] romWarriors = { romeWarrior, archer,  knight};
            SolderBase[] vikingWarriors = { vikingWarrior, berserk };
            Random romChange = new Random();
            Random VikingChange = new Random();
            int rome = romChange.Next(romWarriors.Length);
            int viking = VikingChange.Next(vikingWarriors.Length);

            //Начало битвы между воинами
            Console.WriteLine($"The warriors came out and move towards each other.");
            log(romWarriors, rome, "begin");
            do
            {
                Console.WriteLine($"{vikingWarriors[viking].Name} hp is {vikingWarriors[viking].HP}\t{romWarriors[rome].Name} hp is {romWarriors[rome].HP}");
                if (vikingWarriors[viking].Distance <= vikingWarriors[viking].Weapone.Range) //Если воин-викинг приблизился на расстояние удара оружием он начинает атаковать противника
                {
                    romWarriors[rome].GetDamage(vikingWarriors[viking]);
                    log(vikingWarriors, viking, "attacks");
                    Console.WriteLine($"{vikingWarriors[viking].ToString()} {vikingWarriors[viking].Name} with HP {vikingWarriors[viking].HP} attacks at the {romWarriors[rome].ToString()} {romWarriors[rome].Name} with HP {romWarriors[rome].HP}");
                }
                else   //Воин-викинг двигается к своему противнику пока не сможет нанести удар
                {
                    vikingWarriors[viking].Move();
                    log(vikingWarriors, viking, "moves");
                }

                if (romWarriors[rome].Distance <= romWarriors[rome].Weapone.Range) //Если римский воин приблизился на расстояние удара оружием он начинает атаковать противника
                {
                    vikingWarriors[viking].GetDamage(romWarriors[rome]);
                    log(romWarriors, rome, "attacks");
                    Console.WriteLine($"{romWarriors[rome].ToString()} {romWarriors[rome].Name} with HP {romWarriors[rome].HP} attacks at the {vikingWarriors[viking].ToString()} {vikingWarriors[viking].Name} with HP {vikingWarriors[viking].HP}");
                }
                else   //Римский воин двигается к своему противнику пока не сможет нанести удар
                {
                    romWarriors[rome].Move();
                    log(romWarriors, rome, "moves");
                }
                Thread.Sleep(100);
            } while (vikingWarriors[viking].HP > 0 && romWarriors[rome].HP > 0);  //Цикл повторяется до тех пор пока кто-то из воинов не погибнет
            
            //Определение победителя и проигравшего
            if (vikingWarriors[viking].HP > 0)
            {
                log(vikingWarriors, viking, "wins");
                Console.WriteLine($"{vikingWarriors[viking].ToString()} {vikingWarriors[viking].Name} won!");
            }
            else
                log(vikingWarriors, viking, "dead");
            if (romWarriors[rome].HP > 0)
            {
                log(romWarriors, rome, "wins");
                Console.WriteLine($"{romWarriors[rome].ToString()} {romWarriors[rome].Name} won!");
            }
            else
                log(romWarriors, rome, "dead");
            singleton.connection.Close();
        }
    }
}
