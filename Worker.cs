namespace FileWatcherDemo;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly DirectoryConifg directoryConifg;
    public Worker(ILogger<Worker> logger, DirectoryConifg directoryConifg)
    {
        this.logger = logger;
        this.directoryConifg = directoryConifg;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            string sourceDirectory = directoryConifg.SourceDirectory;
            string destinationDirectory = directoryConifg.DestinationDirectory;
            string filter = directoryConifg.Filter;
            int delayTime = directoryConifg.DelayTime;
            int threshold = directoryConifg.ThresholdCount;
            int thresholdTime = directoryConifg.ThreasholdTime;
            await Task.Delay(delayTime, stoppingToken);
            var fileSystemWatcher = new FileSystemWatcher
            {
                Path = directoryConifg.SourceDirectory,
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.Attributes |
                                NotifyFilters.CreationTime |
                                NotifyFilters.DirectoryName |
                                NotifyFilters.FileName |
                                NotifyFilters.LastAccess |
                                NotifyFilters.LastWrite |
                                NotifyFilters.Security |
                                NotifyFilters.Size,
                Filter = directoryConifg.Filter,
                EnableRaisingEvents = true
            };
            fileSystemWatcher.Changed +=
                        (source, fileSystemEventArgs) => OnChanged(fileSystemEventArgs);
            fileSystemWatcher.Created +=
                        (source, fileSystemEventArgs) => OnChanged(fileSystemEventArgs);
        }
    }
    public void OnChanged(FileSystemEventArgs fileSystemEventArgs)
    {
        try
        {
            var fileName = fileSystemEventArgs.Name;
            var sourceFilePath = fileSystemEventArgs.FullPath;
            var destinationFilePath = Path.Combine(directoryConifg.DestinationDirectory, fileName);
            logger.LogInformation("{fileName}, with path {sourceFilePath} has been created", fileName, sourceFilePath);
            Utility.MoveFile(sourceFilePath, destinationFilePath);
            logger.LogInformation("File moved to {destinationFilePath} at {Now}", destinationFilePath, DateTime.Now);
        }
        catch (IOException ioEx)
        {
            logger.LogError(ioEx, "{Message} Error occured while processing the file {Name}", ioEx.Message, fileSystemEventArgs.Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message} Error occured while processing the file {Name}", ex.Message, fileSystemEventArgs.Name);
        }
    }
}
