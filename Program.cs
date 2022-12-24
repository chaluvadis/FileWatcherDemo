using System;
using System.IO;

namespace FileWatcherDemo
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // If a directory is not specified, exit program.
            if (args.Length != 2)
            {
                // Display the proper way to call the program.
                Console.WriteLine("Usage: FileWatcherDemo.exe <source_directory> <destination_directory>");
                return;
            }
            try
            {
                // Create a new FileSystemWatcher and set its properties.
                var sourceDirectory = args[0];
                var destinationDirectory = args[1];
                var watcher = new FileSystemWatcher
                {
                    Path = sourceDirectory,
                    // Watch both files and subdirectories.
                    IncludeSubdirectories = true,
                    // Watch for all changes specified in the NotifyFilters
                    //enumeration.
                    NotifyFilter = NotifyFilters.Attributes |
                                NotifyFilters.CreationTime |
                                NotifyFilters.DirectoryName |
                                NotifyFilters.FileName |
                                NotifyFilters.LastAccess |
                                NotifyFilters.LastWrite |
                                NotifyFilters.Security |
                                NotifyFilters.Size,
                    // Watch all files.
                    Filter = "*.*"
                };
                // Add event handlers.
                watcher.Created += (s, e) => OnChanged(e.Name, e.FullPath, destinationDirectory);
                //Start monitoring.
                watcher.EnableRaisingEvents = true;
                Console.WriteLine("Press \'q\' to quit the sample.");
                Console.WriteLine();
                //Make an infinite loop till 'q' is pressed.
                while (Console.Read() != 'q') ;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"An IO Exception Occurred : {ioEx}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Exception Occurred {ex}");
            }
        }
        // Define the event handlers.
        public static void OnChanged(string fileName, string fullPath, string destinationDirectory)
        {
            try
            {
                // Specify what is done when a file is changed.
                var destinationFilePath = Path.Combine(destinationDirectory, fileName);
                Console.WriteLine($"{fileName}, with path {fullPath} has been created");
                File.Move(fullPath, destinationFilePath);
                Console.WriteLine($"File moved to {destinationFilePath}");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error while processing the file {fileName}, {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while processing the file {fileName}, {ex.Message}");
            }
        }
    }
}