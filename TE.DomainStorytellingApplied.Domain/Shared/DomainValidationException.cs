namespace TE.DomainStorytellingApplied.Domain.Shared;

public sealed class DomainValidationException(string message) : Exception(message);
