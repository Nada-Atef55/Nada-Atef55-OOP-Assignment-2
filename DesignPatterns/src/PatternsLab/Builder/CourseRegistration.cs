
namespace PatternsLab.Builder;

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    public CourseRegistration(
        string studentEmail,
        string courseCode,
        string accessMode,
        string? groupCode,
        string? discountCode,
        bool sendWhatsApp,
        bool sendEmailWelcome,
        string? mentorNote,
        DateOnly? preferredStart)
    {

        StudentEmail = studentEmail;
        CourseCode = courseCode;
        AccessMode = accessMode;
        GroupCode = groupCode;
        DiscountCode = discountCode;
        SendWhatsApp = sendWhatsApp;
        SendEmailWelcome = sendEmailWelcome;
        MentorNote = mentorNote;
        PreferredStart = preferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";
}




public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudent()
    {
        var student = new RegistrationBuilder("sara@mail.com", "SEF-101", "LiveGroup")
            .SetGroupCode("G1")
            .SetDiscountCode("EARLY10")
            .SetSendWhatsApp(true)
            .SetSendEmailWelcome(true)
            .SetMentorNote("Needs evening slot")
            .SetPreferredStart(new DateOnly(2026, 10, 1))
            .Build();

        return student;
    }

    public static CourseRegistration CreateVideosOnly()
    {
        var student = new RegistrationBuilder("ali@mail.com", "SEF-101", "VideosOnly")
            .SetGroupCode(null)
            .SetDiscountCode(null)
            .SetSendWhatsApp(false)
            .SetSendEmailWelcome(true)
            .SetMentorNote(null)
            .SetPreferredStart(null)
            .Build();

        return student;
    }
}
