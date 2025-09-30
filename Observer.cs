namespace FileWatcher;

public class Observer
{
    private readonly FileSystemWatcher _watcher;

    public Observer(string startPath)
    {
        _watcher = new FileSystemWatcher(startPath);

        _watcher.NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.CreationTime
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
                                | NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.Security
                                | NotifyFilters.Size;

        _watcher.IncludeSubdirectories = true;
        _watcher.EnableRaisingEvents = true;

        _watcher.Changed += OnChanged;
        _watcher.Created += OnCreated;
        _watcher.Deleted += OnDeleted;
        _watcher.Renamed += OnRenamed;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType != WatcherChangeTypes.Created) return;
        if (MonitorPath(e.FullPath)) return;
        Console.WriteLine($"Created: {e.FullPath}");
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        if (MonitorPath(e.FullPath)) return;
        Console.WriteLine($"Deleted: {e.FullPath}");
        Console.WriteLine($"Renamed:");
        Console.WriteLine($"    Old: {e.OldFullPath}");
        Console.WriteLine($"    New: {e.FullPath}");
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        if (MonitorPath(e.FullPath)) return;
        Console.WriteLine($"Deleted: {e.FullPath}");
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType != WatcherChangeTypes.Changed) return;
        if (MonitorPath(e.FullPath)) return;
        Console.WriteLine($"Changed: {e.FullPath}");
    }

    private bool MonitorPath(string path) => path.Split("\\")[1] == "$RECYCLE.BIN";
}