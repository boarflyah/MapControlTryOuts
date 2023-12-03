// See https://aka.ms/new-console-template for more information
using CoreClassesLib.Models;
using CoreClassesLib.Contexts;
using MapControlConsole;


using(var context = new PointsContext())
{
    Console.WriteLine(string.Format($"InitializeResources - dodano: {Updater.InitializeResources(context)} wierszy"));
    Console.WriteLine(string.Format($"InitializeAmmoContainers - dodano: {Updater.InitializeAmmoContainers(context)} wierszy"));
}

Console.WriteLine("Zrobione");
