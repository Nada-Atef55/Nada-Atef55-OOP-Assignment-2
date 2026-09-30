
namespace LibrarySystem;

public class HeadLibrarian : Staff
{
    private const decimal _allowance = 400m;
    public HeadLibrarian(string fullName, string phoneNumber, DateTime dateTime, decimal salary)
        : base(fullName, phoneNumber, dateTime, salary + _allowance)
    {

    }

    public void ChangeLateFee(Item item,decimal newFee)
    {
        item.ChangeBaseLateFee(newFee);
    }

    public void WithdrawItem(Item item)
    {
        item.MarkWithdrawn();
    }

    public void RestoreItem(Item item)
    {
        item.MarkRestored();
    }
}
