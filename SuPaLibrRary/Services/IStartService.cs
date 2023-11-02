using SuPaLibrRary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuPaLibrRary.Services
{
    public interface IStartService
    {
        void Start();
        int ReturnInt();
        void ShowAlternatives();
        void DeduktFuel(Status stat);
        void FillFuel(Status stat);
        void DriveForeWard(Status stat);
        void DriveLeft(Status stat);
        void DriveRight(Status stat);
        void Reverse(Status stat);
        void AddFatigue(Status stat);
        void Rest(Status stat);
        void ShowStatus(Status stat);
        void GenerateName(Status stat);
    }
}
