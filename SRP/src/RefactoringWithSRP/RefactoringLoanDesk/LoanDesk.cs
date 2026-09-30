namespace RefactoringLoanDesk;

public class LoanDesk
{
    public readonly RiskScoreCalculator riskScoreCalculator = new();
    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    public LoanDesk(decimal requestedAmount, int creditScore, int employmentMonths, bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }



    public bool IsEligible() => riskScoreCalculator.RiskScore(this) >= 55m && CreditScore >= 580;



 
}




public class RiskScoreCalculator
{
    public decimal RiskScore(LoanDesk loan)
    {
        // Risk model will change with risk committee — not with letter templates.
        decimal score = 100m;
        score -= Math.Max(0, 700 - loan.CreditScore) * 0.15m;
        if (loan.EmploymentMonths < 6) score -= 20m;
        if (loan.RequestedAmount > 50_000m && !loan.HasCollateral) score -= 25m;
        if (loan.RequestedAmount > 150_000m) score -= 10m;
        return Math.Clamp(score, 0m, 100m);
    }
}



public class RequiredDocumentsSetter
{
    public IReadOnlyList<string> RequiredDocuments(LoanDesk loan)
    {
        // Compliance checklist changes with regulation, independently of risk formula.
        var docs = new List<string> { "National ID", "Proof of income (3 months)" };
        if (loan.RequestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (loan.HasCollateral) docs.Add("Collateral ownership deed");
        if (loan.EmploymentMonths < 12) docs.Add("Employer letter");
        if (!loan.IsEligible()) docs.Add("Manual underwriter referral form");
        return docs;
    }
}



public class DecisionLetterGenerator
{
    public readonly RiskScoreCalculator riskScoreCalculator = new();
    public readonly RequiredDocumentsSetter requiredDocumentsSetter = new();

    public string DecisionLetter(string applicantName, LoanDesk loan)
    {
        // Legal/comms wording ≠ underwriting math.
        if (loan.IsEligible())
        {
            return $"Dear {applicantName},\nYour request for {loan.RequestedAmount:C} is pre-approved (risk {riskScoreCalculator.RiskScore(loan):0}).\n" +
                   $"Please upload: {string.Join("; ", requiredDocumentsSetter.RequiredDocuments(loan))}.\n";
        }

        return $"Dear {applicantName},\nWe are unable to approve {loan.RequestedAmount:C} at this time.\n" +
               $"Reference risk={riskScoreCalculator.RiskScore(loan):0}. You may reapply after improving documentation.\n";
    }
}




public class CsvRowGenerator
{
    public readonly RiskScoreCalculator riskScoreCalculator = new();

    public string UnderwriterCsvRow(string applicationId, LoanDesk loan)
    {
        // Analytics export schema is yet another reason to change.
        return $"{applicationId},{loan.CreditScore},{loan.EmploymentMonths},{(loan.HasCollateral ? 1 : 0)},{riskScoreCalculator.RiskScore(loan):0.00},{(loan.IsEligible() ? "Y" : "N")}";
    }
}