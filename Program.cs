using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;


namespace HelloWorld
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=SQLConnect1";


            IDbConnection dbConnection = new SqlConnection(connectionString);

            String sqlCommand = "SELECT GETDATE()";

            DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);

            Console.WriteLine(rightNow.ToString());


            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                CPUcores = 8,
                Haswifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "TRX 2060"
            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                CPUcores,
                Haswifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) Values ('" + myComputer.Motherboard + "', "
                        + myComputer.CPUcores + ", '"
                        + myComputer.Haswifi.ToString() + "', '"
                        + myComputer.HasLTE.ToString() + "', '"
                        + myComputer.ReleaseDate.ToString("yyyy-MM-dd HH:mm:ss") + "', "
                        + myComputer.Price + ", '"
                        + myComputer.VideoCard + "')";

            Console.WriteLine(sql);

            int result = dbConnection.Execute(sql);

            Console.WriteLine(result);

            string sqlSelect = @"SELECT * FROM TutorialAppSchema.computer";

            IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);

            foreach (Computer computer in computers){
                Console.WriteLine("Motherboard: " + computer.Motherboard);
                Console.WriteLine("CPU Cores: " + computer.CPUcores);
                Console.WriteLine("Has WiFi: " + computer.Haswifi);
                Console.WriteLine("Has LTE: " + computer.HasLTE);
                Console.WriteLine("Release Date: " + computer.ReleaseDate);
                Console.WriteLine("Price: " + computer.Price);
                Console.WriteLine("Video Card: " + computer.VideoCard);
                Console.WriteLine("---------------");
            }
        }
    }
}
