
namespace LibrarySystem;
public enum LoanStatus
{
    Borrowed,
    Returned,
    Lost
}
public class Loan
{
    private static int _id = 1;
    public int Id { get; }
    
    public DateTime BorrowDate { get; }
    public LoanStatus Status { get; private set; } = LoanStatus.Borrowed;
    public DateTime DueDate { get; }
    public Item LoanItem { get; }
    private DateTime _returnedDate;

    public Loan(Member member, Item item)
    {
        if (member == null) throw new ArgumentNullException("member can't be null");
        if (item == null) throw new ArgumentNullException("item can't be null");
        if (member.LoanHistory.Count == member.MaxLoan)
        {
            throw new ArgumentOutOfRangeException("Max Loan is exceeded");
        }
        if(item.IsWithdrawn || item.IsOnLoan)
        {
            throw new InvalidOperationException("can not borrow this item ");
        }
        Id = _id++;
        BorrowDate = DateTime.Today;
        DueDate = BorrowDate.AddDays(item.LoanPeriod);
        item.MarkOnLoan();
        member.AddLoan(this);
        LoanItem = item;
    }

    public DateTime ReturnedDate
    {
        get
        {
            if (Status != LoanStatus.Returned)
            {
                throw new InvalidOperationException("loan is not returned yet");
            }
            return _returnedDate;
        }
        private set
        {
            _returnedDate = value;
        }
    }
    

    public void Return(DateTime returnDate)
    {
        if (returnDate < BorrowDate)
            throw new InvalidDataException("invalid return date");
        StatusValidation("this loan is already returned", Status);
        Status = LoanStatus.Returned;
        _returnedDate = returnDate;
    }
    public void Lost()
    {
        StatusValidation("this loan is returned, not lost", Status);
        Status = LoanStatus.Lost;
    }
    public decimal CalculateLateFee(Member member)
    {
        return (_returnedDate - DueDate).Days * LoanItem.ItemLateFee * (1 - member.LateFeeDiscount);
    }

    private void StatusValidation(string message, LoanStatus status) 
    {
        if(Status == LoanStatus.Returned)
        {
            throw new InvalidOperationException(message);
        }
    }


}
