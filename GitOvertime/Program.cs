// See https://aka.ms/new-console-template for more information

using GitOvertime;

Console.WriteLine("Hello, World!");

var gitter = new BetweenHours();
gitter.GetCommitsBetweenHours(@"C:\Users\brand\source\repos\Sequence Diagram Generator", 8, 16);