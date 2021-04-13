# DevelopmentStatusUpdater
>Console app to automatically generate random weekly development report

## Requirement
[.NET CORE 2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1)

## How to use it
>1). Download doku file into a local txt location
>
>2). Create a new empty txt file for output
>
>3). Modify file: `./DevelopmentStatusUpdater/Program.cs` for the following parameters:

    string pathOrig = @"C:\Users\zzhang\Downloads\Misc\orig.txt";
    string pathNew = @"C:\Users\zzhang\Downloads\Misc\new.txt";
    string currentUser = "ZZ";
    string[] appNames = new[] { "VeriFi", "AiCR", "MIAC Security Tool" };
    double[] appRatios = new[] { 0.55, 0.44, 0.01};
    

List your applications from most important to least important

>4). *(optional)* The current entries can be 1) development 2) bug fixes 3) client support
If you would like to create more entries, please expand `EntryTemplateType` enum and implement more `IEntryTemplate` in `EntryTemplates.cs`
Also change function `public bool RandomGenerateComplete(EntryTemplateType type)` in `EntryGenerator.cs`
