// See https://aka.ms/new-console-template for more information

using GitOvertime;
using Newtonsoft.Json;

/// <summary>   Initializes a new instance of the <see cref="Program"/> class. </summary>
///
/// <remarks>   Brand, 4/12/2022. </remarks>
///
/// <param name="parameter1">   The first parameter. </param>
/// <param name="parameter2">   The second parameter. </param>

if (args is null || !args.Any() || args.Any(b => string.IsNullOrWhiteSpace(b)))
{
    Console.WriteLine("Pass in the full path to a Git repository to generate an overtime report.");
    Console.WriteLine("Parameters (all mandatory) in order are: ");
    Console.WriteLine("GitOvertime.exe <jsonReportOutputPath> <Oldest Calendar Date> <ShiftStartHour> <ShiftEndHour> <Repository1FulLPath> <Repository2FullPath> <Repository3FullPath> <RepositoryNFullPath (etc.)>");
    Console.WriteLine(
        "Example usage: GitOvertime.exe \"overtimeReport.json\" \"January 1, 2022\" \"8\" \"16\" \"c:\\users\\your_user_name\\source\\repos\\YourProject1\" " +
        "\"c:\\users\\your_user_name\\source\\repos\\YourProject1\"");
    return;
}

/// <summary>   Full pathname of the report file. </summary>
string reportPath = args[0];
/// <summary>   The oldest Date/Time. </summary>
DateTime oldest = Convert.ToDateTime(args[1]);
/// <summary>   The work start hour. </summary>
int workStartHour = Convert.ToInt32(args[2]);
/// <summary>   The work end hour. </summary>
int workEndHour = Convert.ToInt32(args[3]);
/// <summary>   The repository paths. </summary>
string[] repositoryPaths = args.Skip(4).ToArray();

/// <summary>   The gitter. </summary>
var gitter = new BetweenHours();

/// <summary>   Initializes a new instance of the <see cref="Program"/> class. </summary>
///
/// ### <remarks>   Brand, 4/12/2022. </remarks>
///
/// ### <param name="Generator">    The generator". </param>
/// ### <param name="parameter2">   The second parameter. </param>
/// ### <param name="parameter3">   The third parameter. </param>

var report = gitter.GetCommitsBetweenHours(oldest, workStartHour, workEndHour, repositoryPaths);

/// <summary>   As grouped. </summary>
var asGrouped = report.GetAsGroupedDates();

/// <summary>   Initializes a new instance of the <see cref="Program"/> class. </summary>
///
/// <remarks>   Brand, 4/13/2022. </remarks>
///
/// <param name="commits..."">  The commits...". </param>

Console.WriteLine("Evaluating each collection of commits...");
/// <summary>   The report final. </summary>
List<object> reportFinal = new List<object>();

foreach (var g in asGrouped)
{
    reportFinal.Add(g.CalculateFinalReport());
}

/// <summary>   Initializes a new instance of the <see cref="Program"/> class. </summary>
///
/// <remarks>   Brand, 4/13/2022. </remarks>
///
/// <param name="parameter1">   The first parameter. </param>
/// <param name="parameter2">   The second parameter. </param>

File.WriteAllText(reportPath, JsonConvert.SerializeObject(reportFinal, Formatting.Indented));

/// <summary>   Initializes a new instance of the <see cref="Program"/> class. </summary>
///
/// <remarks>   Brand, 4/13/2022. </remarks>
///
/// <param name="completed."">  The completed.". </param>

Console.WriteLine("Execution completed.");