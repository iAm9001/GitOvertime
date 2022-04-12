namespace GitOvertime.Models;

/// <summary>   The hours worked report. </summary>
///
/// <remarks>   Brand, 4/12/2022. </remarks>

public class HoursWorkedReport
{
    /// <summary>   Gets or sets the commits. </summary>
    ///
    /// <value> The commits. </value>

    public IEnumerable<HoursWorkedModel> Commits { get; set; }
    

    public IEnumerable<IGrouping<DateTime,HoursWorkedModel>> GetAsGroupedDates()
    {
        var results = this.Commits.GroupBy(g => g.DateOfCommitCalendarDay);
        return results;
    }
}