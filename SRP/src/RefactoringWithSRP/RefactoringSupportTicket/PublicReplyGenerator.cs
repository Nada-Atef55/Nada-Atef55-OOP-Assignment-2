
namespace RefactoringSupportTicket;

public class PublicReplyGenerator
{
    public string DraftPublicReply(string agentName, SupportTicket ticket)
    {
        SlaDeadlineCalculator sla = new();
        var apology = ticket.Priority == "P1" ? "We are treating this as a critical incident." : "Thanks for reaching out.";
        return $"Hi,\n{apology}\nTicket {ticket.Id} is with {agentName}. Next update before {sla.SlaDeadline(ticket):u}.\n";
    }
}
