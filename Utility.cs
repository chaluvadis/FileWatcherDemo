namespace FileWatcherDemo;
public static class Utility
{
    static int processedFilesCount;
    static DateTime processInitiatedTime;
    public static void MoveFile(string sourceFilePath, string destinationFilePath)
            => File.Move(sourceFilePath, destinationFilePath);
    public static int GetProcessedFilesCount() => processedFilesCount;
    public static void IncrementProcessedFilesCount() => processedFilesCount++;
    public static void ClearProcessedFilesCount() => processedFilesCount = 0;
    public static DateTime GetProcessInitiatedTime() => processInitiatedTime;
    public static void SetProcessInitiatedTime() => processInitiatedTime = DateTime.Now;
    public static void ClearProcessInitiatedTime() => processInitiatedTime = DateTime.MinValue;
    public static void ClearAll()
    {
        ClearProcessedFilesCount();
        ClearProcessInitiatedTime();
    }
}