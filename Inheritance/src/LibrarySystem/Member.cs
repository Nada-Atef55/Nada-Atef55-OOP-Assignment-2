
namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = [];
    public int MaxLoan { get; }
    public decimal LateFeeDiscount { get; }
    protected Member(string fullName, string phoneNumber, int maxLoan, decimal feeDiscount) : base(fullName, phoneNumber)
    {
        MaxLoan = maxLoan;
        LateFeeDiscount = feeDiscount;
    }

    public IReadOnlyList<Loan> LoanHistory => _loans;
    public void AddLoan(Loan loan) => _loans.Add(loan);
}
