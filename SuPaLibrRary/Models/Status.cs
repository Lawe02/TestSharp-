using System;

namespace SuPaLibrRary.Models
{
    public class Status
    {
        private int fuel = 10;
        private int fatigue = 0;
        public int Fuel
        {
            get { return fuel; }
            set
            {
                if (value < 0)
                    fuel = 0;
                else
                    fuel = value;
                SetFuelStatus();
            } 
        } 
        public int Fatigue 
        {
            get { return fatigue; }
            set
            {
                if(value > 10)
                    fatigue = 10;
                else
                    fatigue = value;
                SetFatigueStatus(); 
            }
        } 
        public string Name { get; set; }
        public Direction Direction { get; set; }
        public FuelStatus FuelStatus { get; set; }
        public FatigueStatus FatigueStatus { get; set; }

        public void SetFatigueStatus()
        {
            if (fatigue >= 7 && fatigue < 10)
                FatigueStatus = FatigueStatus.Tired;
            else if(fatigue == 10)
                FatigueStatus = FatigueStatus.Done;
            else
                FatigueStatus= FatigueStatus.Ok;
        }
        public void SetFuelStatus()
        {
            if (fuel <= 3 && fuel > 0)
                FuelStatus = FuelStatus.LittleFuel;
            else if(fuel == 0)
                FuelStatus = FuelStatus.NoFuel;
            else
                FuelStatus = FuelStatus.Ok;
        }
    }
}
