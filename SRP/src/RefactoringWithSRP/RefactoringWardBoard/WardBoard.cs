
namespace RefactoringWardBoard;

public class WardBoard
{
    public readonly Dictionary<int, string> _bedPatient = new();
    public readonly Dictionary<int, int> _vitalsScore = new();
    public readonly List<string> _pagerLog = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = ScoreAcuity(heartRate, spo2);

        if (_vitalsScore[bed] >= 8)
            _pagerLog.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        var score = 0;
        if (heartRate > 120 || heartRate < 45) score += 4;
        else if (heartRate > 100) score += 2;
        if (spo2 < 90) score += 5;
        else if (spo2 < 94) score += 2;
        return Math.Min(score, 10);
    }



    public IReadOnlyList<string> DrainPagerLog()
    {
        var copy = _pagerLog.ToList();
        _pagerLog.Clear();
        return copy;
    }


}


public class NurseHandoffNoteGenerator
{
    public string BuildHandoffNote(int bed, WardBoard ward)
    {
        if (!ward._bedPatient.TryGetValue(bed, out var patient))
            return $"Bed {bed}: empty";

        var acuity = ward._vitalsScore[bed];
        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}


public class CensusExporter
{
    public string ExportCensusCsv(WardBoard ward)
    {
        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var bed in ward._bedPatient.Keys.OrderBy(x => x))
            lines.Add($"{bed},{ward._bedPatient[bed]},{ward._vitalsScore[bed]}");
        return string.Join('\n', lines);
    }
}

