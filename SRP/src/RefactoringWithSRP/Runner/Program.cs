
using RefactoringAppointmentDesk;
using RefactoringCheckoutBasket;
using RefactoringCourseEnrollmentDesk;
using RefactoringGradeBook;
using RefactoringKitchenTicket;
using RefactoringLoanDesk;
using RefactoringSubscriptionBilling;
using RefactoringWardBoard;
using RefactoringSupportTicket;
namespace Runner;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("SrpLab — 10 intentional SRP violations (refactor me)");
        Console.WriteLine("=================================================");


        NurseHandoffNoteGenerator nurseHandoffNoteGenerator = new();
        var ward = new WardBoard();
        ward.AssignBed(1, "p-88", heartRate: 130, spo2: 89);
        Console.WriteLine(nurseHandoffNoteGenerator.BuildHandoffNote(1,ward));
        Console.WriteLine(string.Join(" | ", ward.DrainPagerLog()));



        DiscountAmountCalculator discountAmountCalculator = new();
        GiftMaker giftMaker = new();
        PaymentAuthorizationStub paymentAuthorizationStub = new();
        var basket = new CheckoutBasket();
        basket.AddLine("SKU-1", 40m, 2);
        discountAmountCalculator.ApplyCouponText("SAVE10");
        giftMaker.EnableGiftWrap();
        Console.WriteLine($"basket total={basket.GrandTotal()} auth={paymentAuthorizationStub.AuthorizePaymentStub("4242", basket)}");



        PublicReplyGenerator publicReplyGenerator = new();
        var ticket = new SupportTicket("T-1", "cannot login", "prod is down for me", DateTimeOffset.UtcNow);
        Console.WriteLine(publicReplyGenerator.DraftPublicReply("Nora", ticket));


        DecisionLetterGenerator decisionLetterGenerator = new();
        var loan = new LoanDesk(60_000m, 640, 4, hasCollateral: false);
        Console.WriteLine(decisionLetterGenerator.DecisionLetter("Omar", loan));


        WelcomePacketGenerator welcomePacketGenerator = new();
        var course = new CourseEnrollmentDesk("SEF-101", capacity: 1, tuition: 3000m);
        Console.WriteLine(course.Register("a@mail.com"));
        Console.WriteLine(course.Register("b@mail.com"));
        Console.WriteLine(welcomePacketGenerator.WelcomePacketMarkdown("b@mail.com", "Bea", course));


        ThermalTicketGenerator thermalTicketGenerator = new();
        var kitchen = new KitchenTicket();
        kitchen.AddItem("Pasta", new[] { "wheat", "milk" }, 12);
        Console.WriteLine(thermalTicketGenerator.RenderThermalTicket(42, kitchen));



        DunningEmailGenerator dunningEmailGenerator = new();
        var sub = new SubscriptionBilling("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));
        sub.RegisterFailedPayment();
        Console.WriteLine(dunningEmailGenerator.DunningEmail("Sara", new DateOnly(2026, 9, 20), sub));


        PickerScriptGenerator pickerScriptGenerator = new();
        var pick = new WarehousePickList();
        pick.AddNeed("BOLT", "A", 3, 10, 7);
        pick.AddNeed("NUT", "B", 1, 5, 5);
        Console.WriteLine(pickerScriptGenerator.PickerScript(pick));



        var grades = new GradeBook();
        grades.Record("s1", 92);
        grades.Record("s1", 88);
        Console.WriteLine(grades.TranscriptPlain("s1", "Ali"));


        SmsReminder smsReminder = new();
        var appt = new AppointmentDesk(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
        var slot = appt.FindNextSlot(DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48);
        if (slot is null) throw new InvalidOperationException("no slot");
        appt.TryBook(slot.Value);
        Console.WriteLine(smsReminder.Reminder(slot.Value, "0100"));

        Console.WriteLine("Done. Now split responsibilities — without breaking behavior.");
    }
}
