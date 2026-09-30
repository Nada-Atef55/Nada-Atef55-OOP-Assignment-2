
namespace LibrarySystem;

public class Item
{
    protected Item(string catalogNumber, string title, int loanPeriod, decimal baseLateFee, decimal itemLateFee)
    {
        CatalogNumber = catalogNumber;
        Title = title;
        LoanPeriod = loanPeriod;
        ItemLateFee = itemLateFee;
        BaseLateFee = baseLateFee;
    }
    public bool IsWithdrawn { get; private set; } = false;
    public bool IsOnLoan { get; private set; } = false;

    public string CatalogNumber { get; }
    public string Title { get; }
    public int LoanPeriod { get; }
    public decimal BaseLateFee { get; private set; }
    public decimal ItemLateFee { get; }
    public void ChangeBaseLateFee(decimal fee)
    {
        BaseLateFee = fee;
    }

    public void MarkWithdrawn() => IsWithdrawn = true;

    public void MarkRestored() => IsWithdrawn = false;

    public void MarkOnLoan() => IsOnLoan = true;


}
