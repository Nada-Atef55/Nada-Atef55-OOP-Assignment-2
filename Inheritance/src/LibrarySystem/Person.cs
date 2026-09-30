
namespace LibrarySystem;

public class Person
{
    private static int _id = 1;
    public int Id { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }
    protected Person(string fullName, string phoneNumber)
    {
        Id = _id++;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

}
