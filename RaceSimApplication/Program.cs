// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using Controller;
using Model;
using RaceSimApplication;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Hello, World!");
var competition = new Competition();
Data.Initialize(competition);
Data.NextRaceEvent += Visualize.OnNextRaceEvent!;
Data.StartNextRace(competition);
Console.WriteLine(Data.CurrentRace?.Track?.Name);
Debug.Assert(Data.CurrentRace != null, "Controller.Data.CurrentRace != null");
Visualize.Initialize(Data.CurrentRace);
for (;;) Thread.Sleep(100);