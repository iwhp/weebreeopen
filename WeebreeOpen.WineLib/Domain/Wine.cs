namespace WeebreeOpen.WineLib.Domain
{
    using System;
    using System.Linq;

    public class Wine
    {
        public string StroageLocationRow { get; set; }
        public string StroageLocationBin { get; set; }
        public string Producer { get; set; }    // Abreu Vineyards
        public string Label { get; set; }   // Baron Longuvielle
        public string Vintage { get; set; } // 2002
        public string Variety { get; set; } // Cabernet Sauvignon
        public string Class { get; set; } // Red
        public string Country { get; set; }  // USA
        public string Region { get; set; }   // California
        public string Rating { get; set; }  // 92
        public string BestToDrinkBeweenStartDate { get; set; }
        public string BestToDrinkBeweenEndDate { get; set; }
        public string PurchasedFrom { get; set; }
        public string QuantityOnHand { get; set; }
        public string CostBottle { get; set; }
        public string ValueBottle { get; set; }
        public string BootleSize { get; set; }
        public string Note { get; set; }
    }
}
