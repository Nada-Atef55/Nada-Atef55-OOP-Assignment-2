namespace RefactoringGradeBook;

public class GradeBook
{
    public readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);
    public readonly GradeAverageCalculator _averageCalculator = new();
    public readonly LetterGradeCalculator _letterCalculator = new();
    public readonly HonorRollChecker _honorChecker = new();

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(score));
        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }
        list.Add(score);
    }


    public string TranscriptPlain(string studentId, string fullName)
        => $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {_averageCalculator.Average(studentId, this)}\n" +
        $"Letter: {_letterCalculator.Letter(studentId, this)}\nHonor: {_honorChecker.MeetsHonorRoll(studentId, this)}\n";
    
}




public class GradeAverageCalculator
{
    public decimal Average(string studentId, GradeBook gradeBook)
    {
        if (!gradeBook._scores.TryGetValue(studentId, out var list) || list.Count == 0) return 0m;
        return Math.Round(list.Average(), 2);
    }
}



public class LetterGradeCalculator
{
    public readonly GradeAverageCalculator _averageCalculator = new();
    public string Letter(string studentId, GradeBook gradeBook)
    {
        var avg = _averageCalculator.Average(studentId, gradeBook);
        if (avg >= 90) return "A";
        if (avg >= 80) return "B";
        if (avg >= 70) return "C";
        if (avg >= 60) return "D";
        return "F";
    }
}



public class HonorRollChecker
{
    public readonly GradeAverageCalculator _averageCalculator = new();
    public readonly LetterGradeCalculator _letterCalculator = new();

    public bool MeetsHonorRoll(string studentId, GradeBook gradeBook)
        => _averageCalculator.Average(studentId, gradeBook) >= 85 && _letterCalculator.Letter(studentId, gradeBook) is "A" or "B";
    
}




public class CsvExporter
{
    public readonly GradeAverageCalculator _averageCalculator = new();
    public readonly LetterGradeCalculator _letterCalculator = new();
    public readonly HonorRollChecker _honorChecker = new();
    public string ExportCsv(GradeBook gradeBook)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };
        foreach (var id in gradeBook._scores.Keys.OrderBy(x => x))
            rows.Add($"{id},{_averageCalculator.Average(id, gradeBook)}," +
                $"{_letterCalculator.Letter(id, gradeBook)}," +
                $"{(_honorChecker.MeetsHonorRoll(id, gradeBook) ? 1 : 0)}");
        return string.Join('\n', rows);
    }
}