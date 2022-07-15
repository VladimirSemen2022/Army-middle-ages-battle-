using System;
using System.Data.SqlClient;
using ConsoleApp2.Soldiers;


namespace Army__middle_ages_battle_
{
    public sealed class SQLConnection //Класс Singleton (одиночка) для создания единичного подключения к базе SQL
    { 

        static string connect;
        public SqlConnection connection { get; private set;  }

        private SQLConnection()     //Создание подключения к базе SQL
        {
            connection = new SqlConnection(connect);
            connection.Open();      //Открытие соединения с базой SQL
        }

        private static SQLConnection _instance;     //Внутренняя переменная класса, хранящая соединение с базой 
        public static SQLConnection GetInstance(string connStr)     //Метод создания подключения к базе SQL
        {
            if (_instance == null)
            {
                connect = connStr;
                _instance = new SQLConnection();
            }
            return _instance;
        }

        public void SaveLog (SolderBase[] solder, int sold, string action)      //Метод записи данных в открытую базу SQL
        {
            DateTime datetime = DateTime.Now;
            var dateLogo = datetime.ToString("yyyy-MM-dd HH:mm:ss");
            string sqlText = String.Format($@"INSERT INTO [dbo].[Log]([Name],[SoldierType],[WeaponType],[HP],[Action],[DateTime]) VALUES ('{solder[sold].Name}','{solder[sold].ToString()}', '{solder[sold].Weapone.ToString()}', {solder[sold].HP}, '{action}', '{dateLogo}');");
            SqlCommand command = new SqlCommand(sqlText, connection);
            command.ExecuteNonQuery();
        }
    }
}


