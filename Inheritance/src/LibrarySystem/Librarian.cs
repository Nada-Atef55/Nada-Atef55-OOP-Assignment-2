
namespace LibrarySystem;

public class Librarian : Staff
{
    public Librarian(string fullName, string phoneNumber, DateTime hireDate, decimal salary)
        : base(fullName, phoneNumber, hireDate, salary)
    {

    }

    public void ProcessReturn(Loan loan,DateTime returnDate)
    {
        if(loan is null)
        {
            throw new ArgumentNullException("not nul Loan parameter is needed");
        }
        loan.Return(returnDate);
    }

    public void MarkLost(Loan loan)
    {
        if (loan is null)
        {
            throw new ArgumentNullException("not nul Loan parameter is needed");
        }
        loan.Lost();
    }

}
