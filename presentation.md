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

Hello!

My presentation is about a small program I wrote.
It uses an API to get Random Quotes.

Have fun!

## The API

I'm using an API called `Quotable`. It can be found [here](https://github.com/lukePeavey/quotable).

It is reachable on `api.quotable.io` and needs to be called as following:

```HTTP Get random Quote
    GET /random
```

The response is in JSON format:

```JSON The Response (JSON)
{
  _id: string
  // The quotation text
  content: string
  // The full name of the author
  author: string
  // The `slug` of the quote author
  authorSlug: string
  // The length of quote (number of characters)
  length: number
  // An array of tag names for this quote
  tags: string[]
}
```

You can also filter for specific tags:

```HTTP
    GET /random?tags=technology,famous-quotes
```

or

```HTTP
    GET /random?tags=history|civil-rights
```

*The examples are from the [GitHub-Repo](https://github.com/lukePeavey/quotable) of the API*

### List of Tags
You can get a list of all tags:

```HTTP
    GET /tags
```

```javascript Small JavaScript Fragment
(() => { 
	var req = new XMLHttpRequest();
  req.open("GET", "https://api.quotable.io/tags");
  req.onreadystatechange = function() {
  	if(req.readyState === 4){
      if(req.status === 200){
        var tags = JSON.parse(req.responseText);
        for(var i = 0; i<tags.length; i++){
          console.log(tags[i].name);
        }
      }
    }
  };
  req.send();
})();
```
<script>@input</script>

## Example code

```csharp Program.cs
using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace sample_code
{
    class Program
    {
        static string API_URL = "https://api.quotable.io/random";

        static void Main(string[] args)
        {
            Console.Write("Enter tag(s), leave empty for none: ");
            string buffer = Console.ReadLine();

            buffer = buffer.Replace(" ", "");

            if(buffer.Length > 0) API_URL = String.Concat(API_URL, $"?tags={buffer}");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(API_URL);
            request.Method = "GET";

            try{
                using(HttpWebResponse response = (HttpWebResponse)request.GetResponse()){
                    using(StreamReader sr = new StreamReader(response.GetResponseStream())){
                        string jsonResponse = sr.ReadToEnd();
                        dynamic quote = JsonConvert.DeserializeObject(jsonResponse);
                        Console.WriteLine($"Received Quote: \"{quote.content}\" - {quote.author}");
                        Console.WriteLine($"Tags: {String.Join(", ",quote.tags)}");
                  }
                }
            } catch (WebException e) {
                var response = (HttpWebResponse)e.Response;
                using(StreamReader sr = new StreamReader(response.GetResponseStream())){
                    if((int)response.StatusCode == 404){
                        string errorResponse = sr.ReadToEnd();
                        dynamic error = JsonConvert.DeserializeObject(errorResponse);
                        Console.WriteLine($"Error getting quote: {error.statusMessage}");
                    } else {
                        string errorResponse = sr.ReadToEnd();
                        Console.WriteLine($"Error {response.StatusCode} returned: {errorResponse}");
                    }
                }
            }
        }
    }
}
```
```xml project.csproj
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
    <RootNamespace>sample_code</RootNamespace>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" Version="13.0.1" />
  </ItemGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)
