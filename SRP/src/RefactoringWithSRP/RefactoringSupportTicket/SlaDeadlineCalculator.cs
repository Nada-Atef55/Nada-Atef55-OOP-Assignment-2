
namespace RefactoringSupportTicket;

public class SlaDeadlineCalculator
{
    public DateTimeOffset SlaDeadline(SupportTicket ticket)
    {
        var hours = ticket.Priority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };
        return ticket.OpenedAt.AddHours(hours);
    }
}
