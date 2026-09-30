
namespace LibrarySystem;

public class Staff : Person
{
    protected Staff(string fullName, string phoneNumber, DateTime hireDate, decimal monthlySalary) : base(fullName, phoneNumber)
    {
        HireDate = hireDate;
        MonthlySalary = monthlySalary;
    }

    public DateTime HireDate { get; }
    public decimal MonthlySalary { get; private set; }
    public void ChangeSalaryBy(double percentage)
    {
        if (percentage <= 0)
        {
            throw new ArgumentException("percentage should be positive");
        }
        MonthlySalary += (decimal)percentage * MonthlySalary;
    }
}
