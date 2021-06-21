<!--
author:   `BerndSchmecka`

email:    business@dunkelmann.eu

version:  0.0.1

language: en

narrator: US English Female

comment:  My presentation about a particular
          code snippet.
-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://raw.githubusercontent.com/BerndSchmecka/lia-presentation/main/presentation.md)

# Presentation

## Example code

```csharp Write/WriteLine
using System;

public class Program {
  public static void Main(string[] args){
    Console.WriteLine("Test");
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)
