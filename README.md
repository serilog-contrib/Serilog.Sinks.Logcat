### Serilog.Sinks.Logcat

Best way to integrate Your Serilog configuration with Xamarin.Android! 

## How to use?

1. Copy files ```LogcatConfigurationExtensions.cs``` & ```LogcatSink.cs``` to Your solution (There is small problem with nuget package for only Xamarin.Android).  
2. Use it: 

 
By class:
```C#
                .AddLogging(builder =>
                {
                    var loggerConfiguration = new LoggerConfiguration()
                        .WriteTo
                        .Logcat(tag: "MyAwsomeApp");
                
                    var logger = loggerConfiguration.CreateLogger();
                
                    builder.AddSerilog(logger);
                })
```  
By appsettings:
```Json
  "Serilog": {
    "Using":  [ "MyAwsomeAppProjectName" ],
    "MinimumLevel": "Debug",
    "WriteTo": [
      {
        "Name": "Logcat",
        "Args":  {
          "Tag": "MyAwsomeApp"
        }
      }
    ],
```
