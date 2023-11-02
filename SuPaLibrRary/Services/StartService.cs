using Newtonsoft.Json.Linq;
using SuPaLibrRary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuPaLibrRary.Services
{
    public class StartService : IStartService
    {
        public void ShowAlternatives()
        {
            Console.WriteLine("1.Sväng vänster");
            Console.WriteLine("2.Sväng höger");
            Console.WriteLine("3.Kör framåt");
            Console.WriteLine("4.Backa");
            Console.WriteLine("5.Rasta");
            Console.WriteLine("6.Tanka bilen");
            Console.WriteLine("7.Avsluta");
        }
        public int ReturnInt()
        {
            int val =0;
            while(true)
            {
                if (int.TryParse(Console.ReadLine(), out val) && val >= 1 && val <= 7)
                {
                    return val;
                }
                else
                {
                    Console.WriteLine("Ange ett nummer från 1 - 7 tack");
                }
            }
        }
        public void DeduktFuel(Status stat)
        {
            stat.Fuel--;
        }
        public void FillFuel(Status stat)
        {
            Console.WriteLine($"{stat.Name} väljer att tanka");
            stat.Fuel = 10;
            AddFatigue(stat);
        }
        public void AddFatigue(Status stat)
        {
            stat.Fatigue++;
        }
        public void Rest(Status stat)
        {
            Console.WriteLine($"{stat.Name} väljer at vila");
            stat.Fatigue = 0;
        }
        public void Reverse(Status stat)
        {
            Console.WriteLine($"{stat.Name} väljer att backa");
            stat.Direction = Direction.Suoth;
            AddFatigue(stat);
            DeduktFuel (stat);
        }
        public void DriveRight(Status stat)
        {
            Console.WriteLine($"{stat.Name} väljer att svänga åt höger");
            stat.Direction = Direction.East;
            AddFatigue(stat);
            DeduktFuel(stat);
        }
        public void DriveLeft(Status stat)
        {
            Console.WriteLine($"{stat.Name} väljer att svänga åt vänster");
            stat.Direction = Direction.West;
            AddFatigue(stat);
            DeduktFuel(stat);
        }
        public void DriveForeWard(Status stat)
        {
            Console.WriteLine($"{stat.Name} är trött, vila gärna");
            stat.Direction = Direction.North;
            AddFatigue(stat);
            DeduktFuel(stat);
        }
        public void ShowStatus(Status stat)
        {
            Console.WriteLine();
            Console.WriteLine($"Bilens riktning {stat.Direction}");
            if (stat.FuelStatus == FuelStatus.LittleFuel)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Bilen har lite bensin, tanka gärna");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            else if(stat.FuelStatus == FuelStatus.NoFuel)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Bilen har ingen bensin och beöver tankas");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();   
            }
            Console.WriteLine("Bensin: " + stat.Fuel);
            if (stat.FatigueStatus == FatigueStatus.Tired)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{stat.Name} är trött, vila gärna");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            else if(stat.FatigueStatus == FatigueStatus.Done)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{stat.Name} behöver vila");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            Console.WriteLine($"{stat.Name}s trötthet: "+ stat.Fatigue);
            Console.WriteLine();
        }
        public void Start()
        {
            bool started = false;
            Status status = new();
            GenerateName(status);
            Console.WriteLine("Tryck Enter för att starta.");

            while (true)
            {
                Console.ReadLine();
                Console.Clear();
                if (started)
                {
                    ShowStatus(status);
                }
                started = true;
                ShowAlternatives();
                int val = ReturnInt();

                switch (val) { 
                    case 1:
                        DriveLeft(status);
                        break;
                    case 2:
                        DriveRight(status);
                        break;
                    case 3:
                        DriveForeWard(status);
                        break;
                    case 4:
                        Reverse(status);
                        break;
                    case 5:
                        Rest(status);
                        break;
                    case 6:
                        FillFuel(status);
                        break;
                    case 7:
                        throw new StackOverflowException();
                }
            }
        }

        public async void GenerateName(Status stat)
        {
            string apiUrl = "https://randomuser.me/api/";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();

                        // Parse the JSON response
                        JObject userData = JObject.Parse(jsonResult);
                        JToken nameToken = userData["results"][0]["name"];

                        string firstName = nameToken["first"].ToString();
                        string lastName = nameToken["last"].ToString();

                        stat.Name = firstName;
                    }
                    else
                    {
                        Console.WriteLine("Failed to retrieve data from the API");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
