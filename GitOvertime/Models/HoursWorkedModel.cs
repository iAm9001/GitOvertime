using LibGit2Sharp;
using Newtonsoft.Json;

namespace GitOvertime.Models;

public class HoursWorkedModel
{
    [JsonIgnore]
    public Commit InnerCommit { get; set; }
    
    /// <summary>   Gets or sets the name of the repository. </summary>
    ///
    /// <value> The name of the repository. </value>

    public string RepositoryName { get; set; }

    /// <summary>   Gets or sets the name of the author. </summary>
    ///
    /// <value> The name of the author. </value>

    public string AuthorName { get; set; }

    /// <summary>   Gets or sets the author email. </summary>
    ///
    /// <value> The author email. </value>

    public string AuthorEmail { get; set; }

    /// <summary>   Gets or sets the date of commit. </summary>
    ///
    /// <value> The date of commit. </value>

    public DateTimeOffset DateOfCommit { get; set; }

    /// <summary>   Gets the date of commit calendar day. </summary>
    ///
    /// <value> The date of commit calendar day. </value>

    public DateTime DateOfCommitCalendarDay
    {
        get { return this.DateOfCommit.Date; }
    }

    /// <summary>   Gets or sets the notes. </summary>
    ///
    /// <value> The notes. </value>

    public string Notes { get; set; }

    /// <summary>   Gets or sets the branch. </summary>
    ///
    /// <value> The branch. </value>

    public string Branch { get; set; }

    /// <summary>   Gets or sets the identifier of the commit. </summary>
    ///
    /// <value> The identifier of the commit. </value>

    public string CommitId { get; set; }
}