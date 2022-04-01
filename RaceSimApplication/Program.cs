// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using RaceSimApplication;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Hello, World!");
Model.Competition competition = new Model.Competition();
Controller.Data.Initialize(competition);
Controller.Data.NextRaceEvent += VisualizeR.OnNextRaceEvent!;
Controller.Data.StartNextRace(competition);
Console.WriteLine(Controller.Data.CurrentRace?.Track.Name);
Debug.Assert(Controller.Data.CurrentRace != null, "Controller.Data.CurrentRace != null");
VisualizeR.Initialize(Controller.Data.CurrentRace);
for (; ; )
{
    Thread.Sleep(100);
}