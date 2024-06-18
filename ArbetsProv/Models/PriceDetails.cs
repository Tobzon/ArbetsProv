namespace ArbetsProv.Models
{
    public class PriceDetails
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

        public PriceDetails Copy()
        {
            return new PriceDetails
            {
                Id = Id,
                Created = Created,
                Modified = Modified,
                CatalogEntryCode = CatalogEntryCode,
                MarketId = MarketId,
                CurrencyCode = CurrencyCode,
                ValidFrom = ValidFrom,
                ValidUntil = ValidUntil,
                UnitPrice = UnitPrice
            };
        }
    }

}
