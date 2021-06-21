<!--
author:   `BerndSchmecka`

email:    business@dunkelmann.eu

version:  0.0.1

language: en

narrator: US English Female

import: https://github.com/liascript/CodeRunner

comment:  My presentation about a particular
          code snippet.
-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://raw.githubusercontent.com/BerndSchmecka/lia-presentation/main/presentation.md)

# Presentation

## Example code

```csharp Write/WriteLine
using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;

public class Program {
  public static void Main(string[] args){
  try {
    const string test_id = "test@myteamspeak.com";
    Console.WriteLine($"Matrix ID for {test_id}: {getMatrixIDfromUsertag(test_id)}");
    } catch (Exception ex) {
      Console.WriteLine($"An error occured: {ex.Message}");
    }
  }
  
  public static string getMatrixIDfromUsertag(string usertag)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://35.195.56.213:8008/lookup");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"tsChatId\":\"" + usertag + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                dynamic de_ser_obj = JsonConvert.DeserializeObject(result);

                return de_ser_obj.matrixId;
            }
        }
}
```
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" Version="13.0.1" />
  </ItemGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)
