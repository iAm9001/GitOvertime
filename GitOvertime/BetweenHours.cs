using LibGit2Sharp;

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
    /// <param name="repoPath"> Full pathname of the repo file. </param>
    /// <param name="h1">       The first int. </param>
    /// <param name="h2">       The second int. </param>

    public void GetCommitsBetweenHours(string repoPath, int h1, int h2)
    {
        using (var repo = new Repository(repoPath))
        {
            var afterHoursOnly =
                repo.Commits.Where(commit => commit.Committer.When.Hour < h1 || commit.Committer.When.Hour > h2);

            foreach (var c in afterHoursOnly)
            {
                Console.WriteLine($"{repo.Branches.Where(b => b.Commits.Contains(c)).FirstOrDefault().FriendlyName}{c.Author.Email} - {c.Author.Name} - {c.Committer.When.ToString()}" +
                                  $"\r\nID: {c.Id.Sha}: {c.Message}");
            }
            
        }
    }
}