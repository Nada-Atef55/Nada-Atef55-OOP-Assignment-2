namespace RefactoringKitchenTicket;

public class KitchenTicket
{
    public readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();
    public readonly AllergensDetector _allergensDetector = new();
    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add((item, ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(), prepMinutes));
    }

  

    public int EstimatedReadyMinutes(int openStations)
    {
        if (openStations <= 0) openStations = 1;
        var sequential = _items.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (_allergensDetector.DetectAllergens(this).Count > 0) parallel += 3; 
        var longest = _items.Count == 0 ? 0 : _items.Max(i => i.PrepMinutes);
        return Math.Max(parallel, longest);
    }



    public string ExpoLaneHint()
        => _allergensDetector.DetectAllergens(this).Count > 0 ? "LANE-ALLERGY" : EstimatedReadyMinutes(2) > 20 ? "LANE-SLOW" : "LANE-FAST";
    
}




public class AllergensDetector
{
    public IReadOnlyList<string> DetectAllergens(KitchenTicket kitchen)
    {
        var hits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var (_, ingredients, _) in kitchen._items)
        {
            foreach (var ing in ingredients)
            {
                if (ing.Contains("milk") || ing.Contains("cheese") || ing.Contains("butter")) hits.Add("dairy");
                if (ing.Contains("wheat") || ing.Contains("flour") || ing.Contains("bread")) hits.Add("gluten");
                if (ing.Contains("peanut") || ing.Contains("almond") || ing.Contains("cashew")) hits.Add("nuts");
                if (ing.Contains("shrimp") || ing.Contains("prawn") || ing.Contains("crab")) hits.Add("shellfish");
            }
        }
        return hits.OrderBy(x => x).ToList();
    }
}




public class ThermalTicketGenerator
{
    public readonly AllergensDetector _allergensDetector = new();

    public string RenderThermalTicket(int orderNumber, KitchenTicket kitchen)
    {
        var width = 32;
        var line = new string('=', width);
        var body = string.Join('\n', kitchen._items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergens = _allergensDetector.DetectAllergens(kitchen);
        var allergyLine = allergens.Count == 0 ? "ALLERGENS: none" : "ALLERGENS: " + string.Join(",", allergens);
        return $"{line}\nORDER #{orderNumber}\nETA {kitchen.EstimatedReadyMinutes(2)} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}