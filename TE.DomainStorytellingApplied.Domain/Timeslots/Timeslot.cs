using TE.DomainStorytellingApplied.Domain.Reservations;
using TE.DomainStorytellingApplied.Domain.Shared;

namespace TE.DomainStorytellingApplied.Domain.Timeslots;

public sealed class Timeslot
{
    private Timeslot(TimeslotId id, PremisesId premisesId, Period period)
    {
        Id = id;
        PremisesId = premisesId;
        Period = period;
        Status = TimeslotStatus.Available;
    }

    public TimeslotId Id { get; }

    public PremisesId PremisesId { get; }

    public Period Period { get; }

    public TimeslotStatus Status { get; private set; }

    public ReservationId? ReservationId { get; private set; }

    public static Timeslot CreateAvailable(PremisesId premisesId, Period period) =>
        new(TimeslotId.New(), premisesId, period);

    public void Reserve(ReservationId reservationId)
    {
        if (Status != TimeslotStatus.Available)
            throw new DomainValidationException("Timeslot is not available.");

        Status = TimeslotStatus.Reserved;
        ReservationId = reservationId;
    }

    public void Release(ReservationId reservationId)
    {
        if (Status != TimeslotStatus.Reserved || ReservationId != reservationId)
            throw new DomainValidationException("Timeslot is not reserved by this reservation.");

        Status = TimeslotStatus.Available;
        ReservationId = null;
    }

    public void Block()
    {
        if (Status == TimeslotStatus.Reserved)
            throw new DomainValidationException("Reserved timeslot must be released before it is blocked.");

        Status = TimeslotStatus.Blocked;
    }
}
