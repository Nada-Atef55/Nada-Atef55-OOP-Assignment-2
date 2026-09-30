
namespace PatternsLab.Builder;

public class RegistrationBuilder
{
    public RegistrationBuilder(string studentEmail, string courseCode, string accessMode)
    {
        _studentEmail = studentEmail;
        _courseCode = courseCode;
        _accessMode = accessMode;
    }

    private string _studentEmail;
    private string _courseCode;
    private string _accessMode;
    private string? _groupCode;
    private string? _discountCode;
    private bool _sendWhatsApp;
    private bool _sendEmailWelcome;
    private string? _mentorNote;
    private DateOnly? _preferredStart;

    public RegistrationBuilder SetStudentEmail(string studentEmail)
    {
        _studentEmail = studentEmail;
        return this;
    }

    public RegistrationBuilder SetCourseCode(string courseCode)
    {
        _courseCode = courseCode;
        return this;
    }

    public RegistrationBuilder SetGroupCode(string? groupCode)
    {
        _groupCode = groupCode;
        return this;
    }

    public RegistrationBuilder SetDiscountCode(string? discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public RegistrationBuilder SetSendWhatsApp(bool sendWhatsApp)
    {
        _sendWhatsApp = sendWhatsApp;
        return this;
    }

    public RegistrationBuilder SetSendEmailWelcome(bool sendEmailWelcome)
    {
        _sendEmailWelcome = sendEmailWelcome;
        return this;
    }

    public RegistrationBuilder SetMentorNote(string? mentorNote)
    {
        _mentorNote = mentorNote;
        return this;
    }

    public RegistrationBuilder SetPreferredStart(DateOnly? preferredStart)
    {
        _preferredStart = preferredStart;
        return this;
    }


    public CourseRegistration Build()
    {
        if (string.IsNullOrWhiteSpace(_studentEmail)) 
            throw new ArgumentException("email required");
        if (string.IsNullOrWhiteSpace(_courseCode)) 
            throw new ArgumentException("course required");

        if (_accessMode == "LiveGroup" && string.IsNullOrWhiteSpace(_groupCode))
            throw new InvalidOperationException("LiveGroup requires GroupCode");
        if (_accessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(_groupCode))
            throw new InvalidOperationException("VideosOnly cannot have GroupCode");

        return new CourseRegistration(_studentEmail,
                                    _courseCode,
                                    _accessMode,
                                    _groupCode,
                                    _discountCode,
                                    _sendWhatsApp,
                                    _sendEmailWelcome,
                                    _mentorNote,
                                    _preferredStart);
    }
        

}
