### Steps to run the code

1) Open the solution in Visual Studio or Visual Studio Code.
2) This code written in dotnet 7, if u dont have dotnet 7 install it.
3) You can change the target framework in csproj file.

Update the source and destination directory information in appsettings.json.

```
"DirectoryConifg": {
    "SourceDirectory": "/Users/hola/downloads/source",
    "DestinationDirectory": "/Users/hola/downloads/resumes",
    "Filter": "*.*"
  }
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