using System.Text.Json.Serialization;
using LibGit2Sharp;

namespace GitOvertime.Models;


public class HoursWorkedReport
{
    private readonly int _startTime;
    private readonly int _endTime;
    private readonly IEnumerable<HoursWorkedModel> _commits;
    private IEnumerable<HoursWorkedModelCollection>? _commitsByDate;

    public HoursWorkedReport(int startTime, int endTime, IEnumerable<HoursWorkedModel> commits)
    {
        _startTime = startTime;
        _endTime = endTime;
        _commits = commits ?? throw new ArgumentNullException(nameof(commits));
    }

    public int StartTime => _startTime;

    public int EndTime => _endTime;


    /// <summary>   Gets or sets the commits. </summary>
    ///
    /// <value> The commits. </value>
    [JsonIgnore]
    public IEnumerable<HoursWorkedModel> Commits => _commits;

    [JsonIgnore]
    private IEnumerable<IGrouping<DateTime, HoursWorkedModel>>? CommitsByDate
    {
        get
        {
            if (this._commitsByDate == null)
            {
                this._commitsByDate = this.GetAsGroupedDates();
            }

            return this._commitsByDate;
        }
    }
    
    // public IGrouping<DateTime, HoursWorkedModel> Get

    public IEnumerable<HoursWorkedModelCollection>? GetAsGroupedDates()
    {

        var results = this.Commits?.GroupBy(g => g.DateOfCommitCalendarDay);

        var returnObj = new List<HoursWorkedModelCollection>();
        foreach (var date in results)
        {
            var grouping = new HoursWorkedModelCollection()
            {
                
            };
        }
        
        return results;
    }
}