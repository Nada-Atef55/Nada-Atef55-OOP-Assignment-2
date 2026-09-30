
namespace LibrarySystem;

public class Shelver : Staff
{
    public Shelver(string fullName, string phoneNumber,DateTime hireDate, decimal salary, string section) 
        : base(fullName, phoneNumber, hireDate, salary)
    {
        Section = section;
    }
    public string Section { get; private set; }

    public void ReassignSection(string section)
    {
        if(section == null)
        {
            throw new ArgumentNullException("section is needed");
        }
        Section = section;
    }



}
