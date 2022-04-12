using System.Collections;

namespace GitOvertime.Models;

/// <summary>   A data Model for the hours worked. </summary>
///
/// <remarks>   Brand, 4/12/2022. </remarks>

public class HoursWorkedModelCollection : IGrouping<DateTime, HoursWorkedModel>
{
    public IEnumerator<HoursWorkedModel> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public DateTime Key { get; }

    public int[] HoursTaggedBeforeShift
    {
        get
        {
            return this.Where(h => h.DateOfCommit.Hour < this.ShiftStartHour).Select(s => s.DateOfCommit.Hour)
                .ToArray();
        }
    }

    public int[] HoursTaggedAfterShift
    {
        get
        {
            return this.Where(h => h.DateOfCommit.Hour > this.ShiftEndHour).Select(s => s.DateOfCommit.Hour)
                .ToArray();
        }
    }

    public int ShiftStartHour { get; set; }
    public int ShiftEndHour { get; set; }
}