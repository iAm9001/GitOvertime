using System.Text.Json.Serialization;

namespace GitOvertime.Models;

/// <summary>   The hours worked report. </summary>
///
/// <remarks>   Brand, 4/13/2022. </remarks>

public class HoursWorkedReport
{
    private readonly int _startTime;
    private readonly int _endTime;
    private readonly IEnumerable<HoursWorkedModel>? _commits;
    private IEnumerable<HoursWorkedModelCollection<HoursWorkedModel>>? _commitsByDate;

    /// <summary>
    ///     Initializes a new instance of the <see cref="HoursWorkedReport"/> class.
    /// </summary>
    ///
    /// <remarks>   Brand, 4/13/2022. </remarks>
    ///
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    ///
    /// <param name="startTime">    The start time. </param>
    /// <param name="endTime">      The end time. </param>
    /// <param name="commits">      The commits. </param>

    public HoursWorkedReport(int startTime, int endTime, IEnumerable<HoursWorkedModel>? commits)
    {
        _startTime = startTime;
        _endTime = endTime;
        _commits = commits ?? throw new ArgumentNullException(nameof(commits));
    }

    /// <summary>   Gets the start time. </summary>
    ///
    /// <value> The start time. </value>

    public int StartTime => _startTime;

    /// <summary>   Gets the end time. </summary>
    ///
    /// <value> The end time. </value>

    public int EndTime => _endTime;

    /// <summary>   Gets or sets the commits. </summary>
    ///
    /// <value> The commits. </value>
    [JsonIgnore]
    public IEnumerable<HoursWorkedModel>? Commits => _commits;

    /// <summary>   Gets the commits by date. </summary>
    ///
    /// <value> The commits by date. </value>

    [JsonIgnore]
    private IEnumerable<IGrouping<DateTime, HoursWorkedModel>>? CommitsByDate
    {
        get
        {
            if (this._commitsByDate == null)
            {
                this._commitsByDate = (IEnumerable<HoursWorkedModelCollection<HoursWorkedModel>>?)this.GetAsGroupedDates();
            }

            return this._commitsByDate;
        }
    }

    /// <summary>   public IGrouping<DateTime, HoursWorkedModel> Get. </summary>
    ///
    /// <remarks>   Brand, 4/13/2022. </remarks>
    ///
    /// <returns>
    ///     An enumerator that allows foreach to be used to process as grouped dates in this
    ///     collection.
    /// </returns>

    public IEnumerable<HoursWorkedModelCollection<HoursWorkedModel>>? GetAsGroupedDates()
    {
        //var results = this.Commits?.GroupBy(g => g.DateOfCommitCalendarDay);

        var results = this.Commits.GroupBy(a => a.DateOfCommitCalendarDay);

        List<HoursWorkedModelCollection<HoursWorkedModel>> collections = new List<HoursWorkedModelCollection<HoursWorkedModel>>();

        foreach (var r in results)
        {
            var collection = new HoursWorkedModelCollection<HoursWorkedModel>(r);
            collections.Add(collection);
        }

        return collections;
    }
}