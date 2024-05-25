using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using DotNetEnv;
using HelloWorld.Data;


namespace HelloWorld
{
    public class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();
            DataContextEF entityFramework = new DataContextEF();


            Env.Load();

            var url = Environment.GetEnvironmentVariable("TEST");

            Console.WriteLine("its working" + url);


            String sqlCommand = "SELECT GETDATE()";

            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

            Console.WriteLine(rightNow.ToString());



            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                Haswifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "TRX 2060"
            };

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                                Motherboard,
                                CPUcores,
                                Haswifi,
                                HasLTE,
                                ReleaseDate,
                                Price,
                                VideoCard
                            ) VALUES (
                                @Motherboard,
                                @CPUcores,
                                @Haswifi,
                                @HasLTE,
                                @ReleaseDate,
                                @Price,
                                @VideoCard
                            )";

            var parameters = new
            {
                myComputer.Motherboard,
                myComputer.CPUcores,
                myComputer.Haswifi,
                myComputer.HasLTE,
                myComputer.ReleaseDate,
                myComputer.Price,
                myComputer.VideoCard
            };
            // Console.WriteLine(sql);

            // int result = dapper.ExecuteSqlWithRowCount(sql);
            bool result = dapper.ExecuteSql(sql, parameters);


            Console.WriteLine(result);

            string sqlSelect = @"SELECT * FROM TutorialAppSchema.computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);


            foreach (Computer singleComputer in computers){
                Console.WriteLine("ComputerId: " + singleComputer.ComputerId);
                Console.WriteLine("Motherboard: " + singleComputer.Motherboard);
                Console.WriteLine("CPU Cores: " + singleComputer.CPUcores);
                Console.WriteLine("Has WiFi: " + singleComputer.Haswifi);
                Console.WriteLine("Has LTE: " + singleComputer.HasLTE);
                Console.WriteLine("Release Date: " + singleComputer.ReleaseDate);
                Console.WriteLine("Price: " + singleComputer.Price);
                Console.WriteLine("Video Card: " + singleComputer.VideoCard);
                Console.WriteLine("---------------");
            }

            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
            if (computersEF != null){

                foreach (Computer singleComputer in computersEF)
                {
                    Console.WriteLine("ComputerId: " + singleComputer.ComputerId);
                    Console.WriteLine("Motherboard: " + singleComputer.Motherboard);
                    Console.WriteLine("CPU Cores: " + singleComputer.CPUcores);
                    Console.WriteLine("Has WiFi: " + singleComputer.Haswifi);
                    Console.WriteLine("Has LTE: " + singleComputer.HasLTE);
                    Console.WriteLine("Release Date: " + singleComputer.ReleaseDate);
                    Console.WriteLine("Price: " + singleComputer.Price);
                    Console.WriteLine("Video Card: " + singleComputer.VideoCard);
                    Console.WriteLine("---------------");
                }
            }
        }
    }
}
