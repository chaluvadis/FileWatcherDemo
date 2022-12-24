### Steps to run the code

1) Open the solution in Visual Studio or Visual Studio Code.
2) This code written in dotnet 7, if u dont have dotnet 7 install it.
3) You can change the target framework in csproj file.

```
    dotnet restore
    dotnet build
```

After the build, check the bin/Debug folder for FileWatcherDemo.exe file.

you can run the console like this.

FileWatcherDemo.exe <source_directory> <destination_directory>
FileWatcherDemo.exe "/Users/hola/downloads/source" "/Users/hola/downloads/resumes" (on Mac)

on windows the filepath will be different.
