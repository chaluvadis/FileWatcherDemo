namespace FileWatcherDemo;
public static class Utility
{
    static int processedFilesCount;
    static DateTime processInitiatedTime;
    public static void MoveFile(string sourceFilePath, string destinationFilePath)
        => File.Move(sourceFilePath, destinationFilePath, true);
    public static int GetProcessedFilesCount() => processedFilesCount;
    public static void IncrementProcessedFilesCount() => processedFilesCount++;
    public static void ClearProcessedFilesCount() => processedFilesCount = 0;
    public static DateTime GetProcessInitiatedTime() => processInitiatedTime;
    public static void SetProcessInitiatedTime() => processInitiatedTime = DateTime.Now;
    public static void ClearProcessInitiatedTime() => processInitiatedTime = DateTime.MinValue;
    public static void StartProcess()
    {
        ClearAll();
        SetProcessInitiatedTime();
        IncrementProcessedFilesCount();
    }
    public static void ClearAll()
    {
        ClearProcessedFilesCount();
        ClearProcessInitiatedTime();
    }

    public static string GetTopFilePath(string sourceDirectory)
    {
        if (!Directory.Exists(sourceDirectory))
        {
            throw new DirectoryNotFoundException($"Directory {sourceDirectory} not found");
        }
        var files = Directory.GetFiles(sourceDirectory);
        if (files.Length == 0)
        {
            return string.Empty;
        }
        return Path.GetFileName(files[0]);
    }
}