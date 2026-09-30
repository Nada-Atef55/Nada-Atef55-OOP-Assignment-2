namespace RefactoringCourseEnrollmentDesk;

public class CourseEnrollmentDesk
{
    public readonly HashSet<string> _seated = new(StringComparer.OrdinalIgnoreCase);
    public readonly List<string> _waitlist = new();
    
    public int Capacity { get; }
    public decimal Tuition { get; }
    public string CourseCode { get; }
     

    public CourseEnrollmentDesk(string courseCode, int capacity, decimal tuition)
    {
        CourseCode = courseCode;
        Capacity = capacity;
        Tuition = tuition;
    }

    public string Register(string studentEmail)
    {
        if (string.IsNullOrWhiteSpace(studentEmail)) throw new ArgumentException("email");
        var email = studentEmail.Trim();

        if (_seated.Contains(email) || _waitlist.Contains(email))
            return "ALREADY_REGISTERED";

        if (_seated.Count < Capacity)
        {
            _seated.Add(email);
            return "SEATED";
        }

        _waitlist.Add(email);
        return $"WAITLIST:{_waitlist.Count}";
    }

    public int WaitlistPosition(string studentEmail)
    {
        var idx = _waitlist.FindIndex(x => x.Equals(studentEmail, StringComparison.OrdinalIgnoreCase));
        return idx < 0 ? -1 : idx + 1;
    }



}



public class WelcomePacketGenerator
{
    public string WelcomePacketMarkdown(string studentEmail, string studentName, CourseEnrollmentDesk courseManager)
    {
        var status = courseManager._seated.Contains(studentEmail) ? "confirmed seat" : $"waitlist #{courseManager.WaitlistPosition(studentEmail)}";
        return $"# Welcome to {courseManager.CourseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{courseManager.CourseCode.ToLowerInvariant()}\n";
    }
}




public class WaitinglistPromoter
{

    public void PromoteFromWaitlist(int seats, CourseEnrollmentDesk courseManager)
    {
        while (seats > 0 && courseManager._waitlist.Count > 0 && courseManager._seated.Count < courseManager.Capacity)
        {
            var next = courseManager._waitlist[0];
            courseManager._waitlist.RemoveAt(0);
            courseManager._seated.Add(next);
            seats--;
        }
    }
}



public class TaxCalculator
{
    public decimal CalculateTax(CourseEnrollmentDesk course) => Math.Round(course.Tuition * 0.14m, 2);
}



public class TuitionInvoiceGenerator
{
    public readonly TaxCalculator _taxCalculator = new();
    public string TuitionInvoiceLine(string studentEmail, CourseEnrollmentDesk courseManager)
    {
        if (!courseManager._seated.Contains(studentEmail)) return $"{courseManager.CourseCode},WAITLIST,0.00";
        var vat = _taxCalculator.CalculateTax(courseManager);
        return $"{courseManager.CourseCode},TUITION,{courseManager.Tuition:0.00},VAT,{vat:0.00},TOTAL,{(courseManager.Tuition + vat):0.00}";
    }
}




