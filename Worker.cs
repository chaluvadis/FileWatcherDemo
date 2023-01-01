namespace FileWatcherDemo;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly DirectoryConifg directoryConifg;
    public Worker(ILogger<Worker> logger, DirectoryConifg directoryConifg)
        => (this.logger, this.directoryConifg) = (logger, directoryConifg);
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            int delayTime = directoryConifg.DelayTime;
            ProcessFiles(directoryConifg.SourceDirectory, directoryConifg.DestinationDirectory);
            await Task.Delay(delayTime, stoppingToken);
        }
    }

    private bool IsOkToProcess()
    {
        var timeElapsed = Math.Round((DateTime.Now - Utility.GetProcessInitiatedTime()).TotalMinutes, 0);
        var processedFilesCount = Utility.GetProcessedFilesCount();
        var isTimeElapsed = timeElapsed <= directoryConifg.ThreasholdTime;
        var isThresholdReached = processedFilesCount <= directoryConifg.ThresholdCount;
        return isTimeElapsed && isThresholdReached;
    }
    public void ProcessFiles(string sourceDirectory, string destinationDirectory)
    {
        Utility.StartProcess();
        while (IsOkToProcess())
        {
            var filePath = Utility.GetTopFilePath(sourceDirectory);
            logger.LogInformation("File Path: {path}", filePath);
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var sourceFilePath = Path.Combine(sourceDirectory, filePath);
                var destinationFilePath = Path.Combine(destinationDirectory, filePath);
                Utility.MoveFile(sourceFilePath, destinationFilePath);
                Utility.IncrementProcessedFilesCount();
            }
        }
    }
}
