namespace RefactoringSubscriptionBilling;
public class SubscriptionBilling
{
    public static int _invoiceSeq = 1000;

    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }

    public SubscriptionBilling(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }

    public void RegisterFailedPayment() => FailedPayments++;

}



public class InvoiceNumberGenerator
{
    public string NextInvoiceNumber(SubscriptionBilling subscription)
    {
        // Numbering scheme / fiscal prefixes — ops concern, not pricing.
        var n = ++SubscriptionBilling._invoiceSeq;
        return $"INV-{subscription.PeriodStart:yyyyMM}-{n:D5}";
    }
}





public class ProrateCalculator
{
    public decimal Prorate(DateOnly activeFrom, SubscriptionBilling subscription)
    {
        // Finance calendar rules change independently of email copy.
        if (activeFrom <= subscription.PeriodStart) return subscription.MonthlyPrice;
        if (activeFrom >= subscription.PeriodEnd) return 0m;
        var totalDays = subscription.PeriodEnd.DayNumber - subscription.PeriodStart.DayNumber;
        if (totalDays <= 0) return subscription.MonthlyPrice;
        var used = subscription.PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(subscription.MonthlyPrice * used / totalDays, 2);
    }
}



public class DunningEmailGenerator
{
    public readonly ProrateCalculator prorateCalculator = new();
    public readonly InvoiceNumberGenerator invoiceNumberGenerator = new();
    public string DunningEmail(string customerName, DateOnly asOf, SubscriptionBilling subscription)
    {
        var amount = prorateCalculator.Prorate(subscription.PeriodStart, subscription);
        var invoice = invoiceNumberGenerator.NextInvoiceNumber(subscription); 
        var severity = subscription.FailedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };
        return $"Subject: {severity} {invoice}\nHi {customerName},\nBalance {amount:C} as of {asOf:o} ({subscription.FailedPayments} failures).\n";
    }
}




public class JournalLineGenerator
{
    public readonly ProrateCalculator prorateCalculator = new();
    public readonly InvoiceNumberGenerator invoiceNumberGenerator = new();
    public string LedgerJournalLine(DateOnly activeFrom, SubscriptionBilling subscription)
    {
        // Accounting export format is another axis of change.
        return $"{subscription.CustomerId},{invoiceNumberGenerator.NextInvoiceNumber(subscription)},{prorateCalculator.Prorate(activeFrom, subscription):0.00},AR-SUB";
    }
}