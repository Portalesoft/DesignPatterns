 
 Visual studio Code CLI

    dotnet new - Create Project
    dotnet run - Run the project
    dotnet test - Run unit tests
    dotnet add package <Package> - Nuget package install

 Unit Test Packages Required

    dotnet add package NUnit
    dotnet add package NUnit.Console
    dotnet add package NUnit3TestAdapter
    dotnet add package Microsoft.NET.Test.Sdk

 Add Following To csproj file:

    <GenerateProgramFile>false</GenerateProgramFile>
    https://andrewlock.net/fixing-the-error-program-has-more-than-one-entry-point-defined-for-console-apps-containing-xunit-tests/

