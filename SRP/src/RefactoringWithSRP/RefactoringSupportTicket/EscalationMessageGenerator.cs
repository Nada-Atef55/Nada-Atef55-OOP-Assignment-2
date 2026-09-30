
namespace RefactoringSupportTicket;

public class EscalationMessageGenerator
{
    public string InternalEscalationBlurb(SupportTicket ticket)
    {
        SlaDeadlineCalculator sla = new();
        return $"ESCALATE {ticket.Id} priority={ticket.Priority} breachAt={sla.SlaDeadline(ticket):u} keywords-scanned=yes";
    }
}
