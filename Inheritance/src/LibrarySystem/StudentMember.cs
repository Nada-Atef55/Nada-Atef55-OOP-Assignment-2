
namespace LibrarySystem;

public class StudentMember : Member
{
    private const int _maxLoanItems = 3;
    public StudentMember(string fullName, string phoneNumber)
        : base(fullName, phoneNumber, _maxLoanItems, 0)
    {
        
    }
}
