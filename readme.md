### Steps to run the code

1) Open the solution in Visual Studio or Visual Studio Code.
2) This code written in dotnet 7, if u dont have dotnet 7 install it.
3) You can change the target framework in csproj file.

Update the source and destination directory information in appsettings.json.

```
"DirectoryConifg": {
    "SourceDirectory": "/Users/hola/downloads/source",
    "DestinationDirectory": "/Users/hola/downloads/resumes",
    "Filter": "*.*",
    "DelayTime": 120000, // number of microseconds. 1sec = 1000
    "ThresholdCount": 5,// thresh hold count
    "ThresholdTime": 2 // minutes
  }
```

Configurations:
```
  1. DelayTime - 120000 (2 Minutes) service runs every 2 minutes
  2. ThresholdTime - 2 Minutes
  3. ThresholdCount - 5 (after processing 5 files in 2 minutes the service will not move the files.)
```

Run the following commands.

```
    dotnet restore
    dotnet build
    dotnet publish ./FileWatcherDemo.csproj -o ./publish
```

After the build, check the publish folder for FileWatcherDemo.exe file.

Run the following commands to create the windows background service.

```
sc create <service_name> binPath=<exepath in double quotes> start="demand" displayname=<sevicename in double quotes>
```

after successful run of the above command, open window services (win+r -> service.msc)

check the service in the list, and start the service.