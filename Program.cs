namespace FileWatcher;

internal static class Program
{
    private static readonly List<Observer> Observers = [];

    private static void Main(string[] args)
    {
        string[] paths = args.Length != 0 ? args : DriveInfo.GetDrives().Select(x => x.Name).ToArray();
        paths.ToList().ForEach(x => Console.WriteLine(x));
        foreach (var path in paths)
            Observers.Add(new Observer(path));

        Console.ReadLine();
    }
}