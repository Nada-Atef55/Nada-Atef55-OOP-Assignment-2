
namespace LibrarySystem;

public class Book : Item
{
    private const int _loanPeriod = 21;
    
    public Book(string catalogNaumber, string Title, decimal baseLateFee)
        : base(catalogNaumber, Title, _loanPeriod, baseLateFee, baseLateFee)
    {
        
    }

}
