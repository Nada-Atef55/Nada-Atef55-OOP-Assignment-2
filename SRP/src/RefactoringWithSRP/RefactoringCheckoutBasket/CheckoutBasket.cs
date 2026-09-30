
namespace RefactoringCheckoutBasket;

public class CheckoutBasket
{
    public readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    
    public readonly DiscountAmountCalculator discountAmountCalculator = new();
    public readonly GiftMaker giftMaker = new(); 
    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }

   
    public decimal SubTotal() => _lines.Sum(l => l.Price * l.Qty);


    public decimal GrandTotal()
    {
        var total = SubTotal() - discountAmountCalculator.DiscountAmount(this);
        if (giftMaker.giftWrap) total += giftMaker.GiftWrappingPrice; 
        return Math.Max(0m, total);
    }



}




public class DiscountAmountCalculator
{
    private string? _couponRaw;
    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public decimal DiscountAmount(CheckoutBasket basket)
    {
        if (string.IsNullOrWhiteSpace(_couponRaw)) return 0m;
        var t = _couponRaw.Trim().ToUpperInvariant();
        if (t.StartsWith("SAVE") && int.TryParse(t[4..], out var pct) && pct is > 0 and <= 50)
            return Math.Round(basket.SubTotal() * pct / 100m, 2);
        if (t.Contains("FREESHIP")) return 0m;
        if (t == "WELCOME10") return Math.Min(10m, basket.SubTotal());
        return 0m;
    }
}




public class PaymentAuthorizationStub
{
    public string AuthorizePaymentStub(string cardLast4, CheckoutBasket basket)
    {
        var payload = $"{basket.GrandTotal():0.00}|{cardLast4}|{basket._lines.Count}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }
}




public class GiftMaker
{
    public decimal GiftWrappingPrice { get; } = 4.99m;
    public bool giftWrap { get; private set; }
    public void EnableGiftWrap() => giftWrap = true;

    public string GiftMessageCard(string fromName, CheckoutBasket basket)
    {
        var items = string.Join(", ", basket._lines.Select(l => l.Sku));
        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {basket.GrandTotal():C}\n";
    }
}




