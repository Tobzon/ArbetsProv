namespace ArbetsProv.Models
{
    public class priceDetail
    {
        public Int32 Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public String CatalogEntryCode { get; set; }

        public String MarketId { get; set; }

        public String CurrencyCode { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime? ValidUntil { get; set; }

        public Decimal UnitPrice { get; set; }
    }
}
