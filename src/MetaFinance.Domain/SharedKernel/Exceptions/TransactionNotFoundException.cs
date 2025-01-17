namespace MetaFinance.Domain.SharedKernel.Exceptions;

public class TransactionPersistenceException(string message, Exception innerException)
    : Exception(message, innerException);