using MetaFinance.Domain.SharedKernel.Base;
using MetaFinance.Domain.SharedKernel.Exceptions;

namespace MetaFinance.Domain.SharedKernel.ValueObjects;

public class Money : BaseValueObject
{
    private const string DefaultCurrency = "BRL";

    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency = DefaultCurrency)
    {
        if (amount < 0) throw new DomainException("Amount cannot be negative");
        if (string.IsNullOrWhiteSpace(currency)) throw new DomainException("Currency is required");

        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new DomainException("Cannot add different currencies");
        
        return new Money(a.Amount + b.Amount, a.Currency);
    }
}