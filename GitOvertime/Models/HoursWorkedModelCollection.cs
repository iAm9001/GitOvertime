using Newtonsoft.Json;
using System.Collections;
using System.Text;

namespace GitOvertime.Models;

/// <summary>   A data Model for the hours worked. </summary>
///
/// <remarks>   Brand, 4/12/2022. </remarks>
///
/// <typeparam name="TValue">   Type of the value. </typeparam>

public class HoursWorkedModelCollection<TValue> : IGrouping<DateTime, TValue> where TValue : HoursWorkedModel
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HoursWorkedModelCollection{TValue}"/>
    ///     class.
    /// </summary>
    ///
    /// <remarks>   Brand, 4/13/2022. </remarks>
    ///
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    ///
    /// <param name="inner">    The inner. </param>

    public HoursWorkedModelCollection(IGrouping<DateTime, TValue> inner)
    {
        Inner = inner ?? throw new ArgumentNullException(nameof(inner));
        this.Key = inner.Key;
    }

    /// <summary>   Gets the inner. </summary>
    ///
    /// <value> The inner. </value>

    [JsonIgnore]
    public IGrouping<DateTime, HoursWorkedModel> Inner { get; }

    /// <summary>   Gets the hours tagged before shift. </summary>
    ///
    /// <value> The hours tagged before shift. </value>

    [JsonProperty]
    public int[] HoursTaggedBeforeShift
    {
        get
        {
            return this.Where(h => h.DateOfCommit.Hour < this.ShiftStartHour).Select(s => s.DateOfCommit.Hour)
                .ToArray();
        }
    }

    /// <summary>   Gets the hours tagged after shift. </summary>
    ///
    /// <value> The hours tagged after shift. </value>

    [JsonProperty]
    public int[] HoursTaggedAfterShift
    {
        get
        {
            return this.Where(h => h.DateOfCommit.Hour > this.ShiftEndHour).Select(s => s.DateOfCommit.Hour)
                .ToArray();
        }
    }

    /// <summary>   Gets or sets the shift start hour. </summary>
    ///
    /// <value> The shift start hour. </value>

    public int ShiftStartHour { get; set; } = 8;

    /// <summary>   Gets or sets the shift end hour. </summary>
    ///
    /// <value> The shift end hour. </value>

    public int ShiftEndHour { get; set; } = 16;

    /// <summary>   Returns an enumerator that iterates through the collection. </summary>
    ///
    /// <remarks>   Brand, 4/13/2022. </remarks>
    ///
    /// <returns>   An enumerator that can be used to iterate through the collection. </returns>

    public IEnumerator<TValue> GetEnumerator()
    {
        return (IEnumerator<TValue>)this.Inner.GetEnumerator();
    }

    /// <summary>   Returns an enumerator that iterates through a collection. </summary>
    ///
    /// <remarks>   Brand, 4/13/2022. </remarks>
    ///
    /// <returns>
    ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate
    ///     through the collection.
    /// </returns>

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>   Gets the key of the <see cref="T:System.Linq.IGrouping`2" />. </summary>
    ///
    /// <value> The key of the <see cref="T:System.Linq.IGrouping`2" />. </value>

    [JsonProperty]
    public DateTime Key { get; }

    /// <summary>   Calculates the final report. </summary>
    ///
    /// <remarks>   Brand, 4/13/2022. </remarks>
    ///
    /// <returns>   The calculated final report. </returns>

    public object CalculateFinalReport()
    {
        StringBuilder repoNameSb = new StringBuilder();
        StringBuilder branchNameSb = new StringBuilder();

        repoNameSb.AppendJoin(';', this.Select(s => s.RepositoryName).Distinct());
        branchNameSb.AppendJoin(';', this.Inner.Select(b => b.Branch).Distinct());
        return new
        {
            WorkDate = Key,
            ShiftStartHour,
            ShiftEndHour,
            HoursTaggedBeforeShift,
            HoursTaggedAfterShift,
            ReposTouched = repoNameSb.ToString(),
            BranchesTouched = branchNameSb.ToString()
        };
    }
}