namespace LibrarySystem;

public class PremiumMember : Member
{
    private const int _maxLoanItems = 10;
    private int _totalPoints;
    public int ReadingPoints
    {
        get
        {

            foreach(var loan in base.LoanHistory)
            {
                if (loan.Status == LoanStatus.Returned)
                {
                    _totalPoints += 5;
                }
            }
            return _totalPoints;
        }
    } 
    public PremiumMember(string fullName, string phoneNumber, decimal feeDiscount)
        :base(fullName, phoneNumber, _maxLoanItems, feeDiscount)
    {
        
    }

}
