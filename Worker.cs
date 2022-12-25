using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        Console.WriteLine(directoryConifg.SourceDirectory, directoryConifg.DestinationDirectory);
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
            string sourceDirectory = directoryConifg.SourceDirectory;
            string destinationDirectory = directoryConifg.DestinationDirectory;
            string filter = directoryConifg.Filter;

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

            fileSystemWatcher.Changed += (source, fileSystemEventArgs) => OnChanged(fileSystemEventArgs, destinationDirectory);
            fileSystemWatcher.Created += (source, fileSystemEventArgs) => OnChanged(fileSystemEventArgs, destinationDirectory);
        }
    }
    public void OnChanged(FileSystemEventArgs fileSystemEventArgs, string destinationDirectory)
    {
        try
        {
            // Specify what is done when a file is changed.
            var fileName = fileSystemEventArgs.Name;
            var fullPath = fileSystemEventArgs.FullPath;
            var destinationFilePath = Path.Combine(destinationDirectory, fileName);
            logger.LogInformation("{fileName}, with path {fullPath} has been created", fileName, fullPath);
            File.Move(fullPath, destinationFilePath);
            Console.WriteLine($"File moved to {destinationFilePath}");
            logger.LogInformation("File moved to {destinationFilePath}", destinationFilePath);
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"Error while processing the file {fileSystemEventArgs.Name}, {ioEx.Message}");
            logger.LogError("{Message} Error occured while processing the file {Name}", ioEx.Message, fileSystemEventArgs.Name);
        }
        catch (Exception ex)
        {
            logger.LogError("{Message} Error occured while processing the file {Name}", ex.Message, fileSystemEventArgs.Name);
        }
    }
}
