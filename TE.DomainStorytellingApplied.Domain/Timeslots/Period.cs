namespace TE.DomainStorytellingApplied.Domain.Timeslots;

public sealed record Period
{
    public Period(DateTime startUtc, DateTime endUtc)
    {
        if (startUtc.Kind != DateTimeKind.Utc || endUtc.Kind != DateTimeKind.Utc)
            throw new ArgumentException("Period must be expressed in UTC.");
        if (endUtc <= startUtc)
            throw new ArgumentException("End must be after start.");

        StartUtc = startUtc;
        EndUtc = endUtc;
    }

    public DateTime StartUtc { get; }

    public DateTime EndUtc { get; }
}
