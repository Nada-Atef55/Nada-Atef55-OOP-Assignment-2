
namespace LibrarySystem;

public class DVD : Item
{
    private const int _loanPeriod = 7;
    public DVD(string catalogNaumber, string Title, decimal baseLateFee) : 
        base(catalogNaumber, Title, _loanPeriod, baseLateFee, 2*baseLateFee)
    {
        
    }
}
