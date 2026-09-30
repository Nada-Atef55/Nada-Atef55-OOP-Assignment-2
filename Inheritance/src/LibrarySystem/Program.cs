namespace LibrarySystem;

internal class Program
{
    static void Main(string[] args)
    {
        //var prson = new Person();        must not compile

        Member member1 = new PremiumMember("Ahmed Ali", "012145477", 0.2m);

        //member1.FullName = "Saif";         must not compile



        Item item1 = new Book("#C05", "Advanced C#", 5);
        Item item2 = new Book("#B01", "OOP", 5);
        Item item3 = new DVD("#D03", "Some Name", 3);
        Item item4 = new DVD("#D07", "Some Name", 3);
        Item item5 = new Magazine("#A44", "Micky", 7);
        Item item6 = new Magazine("#A08", "Magazine Name", 7);

        Loan member1Loan1 = new(member1, item1);

        //member1.LoanHistory.Add(member1Loan1);            must not compile


        //itemAhmedAli.IsOnLoan = true;           must not compile


        var head = new HeadLibrarian("Hossam Mohamed", "0110225688", new DateTime(2024, 2, 6, 7, 40, 0), 1500m);
        head.WithdrawItem(item2);
        try
        {
            var member1Loan2 = new Loan(member1, item2);        //item2 is withdrawn, so throwed an exception
        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        Member member2 = new PremiumMember("Ahmed Ali", "012145477", 0.5m);
        try
        {
            Loan member2Loan1 = new(member2, item1);          //item1 is on loan, so throwed an exception
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }



        Member member3 = new StudentMember("Omar", "0105688744");
        Loan member3Loan1 = new(member3, item3);
        Loan member3Loan2 = new(member3, item4);
        Loan member3Loan3 = new(member3, item5);
        try
        {
            Loan member3Loan4 = new(member3, item6);          //member3 exceeded his Max Loan which is 3
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("============================================");

        var librarian = new Librarian("Assem Mohamed", "011579288", new DateTime(2026, 5, 19, 7, 40, 0), 1000m);
        var shelver = new Shelver("Mohamed Ali", "0128744563", new DateTime(2022, 8, 1, 7, 40, 0), 1200m, "Fiction");

        List<Staff> staff = [head, librarian, shelver];

        foreach (var staffItem in staff)
        {
            Console.WriteLine($"{staffItem.GetType()} salary => {staffItem.MonthlySalary}");
        }

        Console.WriteLine("============================================");
        List<Item> items = [item1, item3, item5];
        foreach (var libraryItem in items)
        {
            Console.WriteLine($"{libraryItem.GetType()} => {libraryItem.LoanPeriod} days, {libraryItem.ItemLateFee}$");
        }


        
        Loan member2Loan2 = new(member2, item6);

        librarian.ProcessReturn(member2Loan2, member2Loan2.DueDate.AddDays(5));
        Console.WriteLine("============================================");

        Console.WriteLine(member2Loan2.DueDate);
        Console.WriteLine($"{member2Loan2.CalculateLateFee(member2)} $");
        Console.WriteLine(((PremiumMember)member2).ReadingPoints);

        try
        {
            librarian.ProcessReturn(member2Loan2, member2Loan2.DueDate.AddDays(5));

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        try
        {
            librarian.MarkLost(member2Loan2);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}
