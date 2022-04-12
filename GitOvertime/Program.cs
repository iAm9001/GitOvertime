// See https://aka.ms/new-console-template for more information

using GitOvertime;

/// <summary>   Initializes a new instance of the <see cref="Program"/> class. </summary>
///
/// <remarks>   Brand, 4/12/2022. </remarks>
///
/// <param name="parameter1">   The first parameter. </param>
/// <param name="parameter2">   The second parameter. </param>

Console.WriteLine("Hello, World!");

/// <summary>   The gitter. </summary>
var gitter = new BetweenHours();

/// <summary>   Initializes a new instance of the <see cref="Program"/> class. </summary>
///
/// <remarks>   Brand, 4/12/2022. </remarks>
///
/// <param name="Generator"">   The generator". </param>
/// <param name="parameter2">   The second parameter. </param>
/// <param name="parameter3">   The third parameter. </param>

gitter.GetCommitsBetweenHours(@"C:\Users\brand\source\repos\Sequence Diagram Generator", DateTime.Today.AddYears(-1), 8, 16);