
namespace LibrarySystem;

public class Magazine : Item
{
    private const int _loanPeriod = 3;
    public Magazine(string catalogNaumber, string Title, decimal baseLateFee) :
        base(catalogNaumber, Title, _loanPeriod, baseLateFee, 0.5m * baseLateFee)
    {

    }
}
