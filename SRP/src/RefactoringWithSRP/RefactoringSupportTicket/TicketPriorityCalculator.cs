
namespace RefactoringSupportTicket;

public sealed class TicketPriorityCalculator
{
    public string RecalculatePriorityFromText(SupportTicket ticket)
    {
        string priority;
        var blob = (ticket.Subject + " " + ticket.Body).ToLowerInvariant();
        if (blob.Contains("down") || blob.Contains("outage") || blob.Contains("cannot login"))
            priority = "P1";
        else if (blob.Contains("urgent") || blob.Contains("asap") || blob.Contains("blocked"))
            priority = "P2";
        else
            priority = "P3";
        return priority;
    }
}
