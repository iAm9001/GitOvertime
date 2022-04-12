using LibGit2Sharp;

namespace GitOvertime;

public class BetweenHours
{
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