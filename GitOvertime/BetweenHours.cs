using GitOvertime.Models;
using LibGit2Sharp;
using Newtonsoft.Json;

namespace GitOvertime;

/// <summary>   A between hours. </summary>
///
/// <remarks>   Brand, 4/12/2022. </remarks>

public class BetweenHours
{
    /// <summary>   Gets commits between hours. </summary>
    ///
    /// <remarks>   Brand, 4/12/2022. </remarks>
    ///
    /// <param name="repoPath">     Full pathname of the repo file. </param>
    /// <param name="oldestDate">   The oldest date. </param>
    /// <param name="h1">           The first int. </param>
    /// <param name="h2">           The second int. </param>

    public void GetCommitsBetweenHours(string repoPath, DateTime oldestDate, int h1, int h2)
    {
        List<HoursWorkedModel> worked = new List<HoursWorkedModel>();
        HoursWorkedReport report = new HoursWorkedReport();
        using (var repo = new Repository(repoPath))
        {
            var afterHoursOnly =
                repo.Commits.Where(commit =>
                    commit.Committer.When.Date >= oldestDate &&
                    (commit.Committer.When.Hour < h1 || commit.Committer.When.Hour > h2));

            foreach (var c in afterHoursOnly)
            {
                var model = new HoursWorkedModel()
                {
                    Branch = repo.Branches.FirstOrDefault(b => b.Commits.Contains(c)).FriendlyName,
                    Notes = c.Message,
                    AuthorEmail = c.Author.Email,
                    AuthorName = c.Author.Name,
                    CommitId = c.Id.Sha,
                    RepositoryName = repoPath,
                    DateOfCommit = c.Committer.When
                };
                worked.Add(model);
            }

            Console.WriteLine(JsonConvert.SerializeObject(worked));
        }
    }
}