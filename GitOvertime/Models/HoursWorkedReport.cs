using LibGit2Sharp;

namespace GitOvertime.Models;


public class HoursWorkedReport
{
    private readonly int _startTime;
    private readonly int _endTime;
    private readonly IEnumerable<HoursWorkedModel> _commits;
    private IEnumerable<IGrouping<DateTime, HoursWorkedModel>> _commitsByDate;

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
    public IEnumerable<HoursWorkedModel> Commits => _commits;

    private IEnumerable<IGrouping<DateTime, HoursWorkedModel>> CommitsByDate
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

    public IEnumerable<IGrouping<DateTime,HoursWorkedModel>> GetAsGroupedDates()
    {
        var results = this.Commits?.GroupBy(g => g.DateOfCommitCalendarDay);
        return results;
    }
}