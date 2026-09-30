
namespace RefactoringSupportTicket;

public sealed class SupportTicket
{
    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }


    public string Priority { get; private set; } = "P3";
    public readonly TicketPriorityCalculator _priority = new();
    public readonly SlaDeadlineCalculator _sla = new();
    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;
        Priority = _priority.RecalculatePriorityFromText(this);
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        Priority = _priority.RecalculatePriorityFromText(this);
    }


    public bool IsBreached(DateTimeOffset now) => now > _sla.SlaDeadline(this);

}
