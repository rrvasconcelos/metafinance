using MetaFinance.Domain.SharedKernel.Base;
using MetaFinance.Domain.SharedKernel.Exceptions;

namespace MetaFinance.Domain.SharedKernel.ValueObjects;

public class DateRange : BaseValueObject
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public DateRange(DateTime start, DateTime end)
    {
        if (end < start) throw new DomainException("End date must be after start date");

        Start = start;
        End = end;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }

    public bool Contains(DateTime date)
    {
        return date >= Start && date <= End;
    }
}