// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Model.Competition competition = new Model.Competition();
Controller.Data.Initialize(competition);
Controller.Data.NextRace();
Console.WriteLine(Controller.Data.CurrentRace.Track.Name);
for (; ; )
{
    Thread.Sleep(100);
}