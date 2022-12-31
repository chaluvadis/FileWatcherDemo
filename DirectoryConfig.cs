namespace FileWatcherDemo;
public class DirectoryConifg
{
    public string SourceDirectory { get; set; }
    public string DestinationDirectory { get; set; }
    public string Filter { get; set; }
    public int DelayTime { get; set; }
    public int ThresholdCount { get; set; }
    public int ThreasholdTime { get; set; }
}