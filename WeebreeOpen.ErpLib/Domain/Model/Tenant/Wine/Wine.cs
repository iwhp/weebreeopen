namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Wine
{
    using System;
    using System.Linq;

    public partial class Wine
    {
        public string BestToDrinkBeweenEndDate { get; set; }

        public string BestToDrinkBeweenStartDate { get; set; }

        public string BootleSize { get; set; }

        public string Class { get; set; }// Red

        public string CostBottle { get; set; }

        public string Country { get; set; }// USA

        public string Label { get; set; }// Baron Longuvielle

        public string Note { get; set; }

        public string Producer { get; set; }// Abreu Vineyards

        public string PurchasedFrom { get; set; }

        public string QuantityOnHand { get; set; }

        public string Rating { get; set; }// 92

        public string Region { get; set; }// California

        public string ValueBottle { get; set; }

        public string Variety { get; set; }// Cabernet Sauvignon

        public string Vintage { get; set; }// 2002
    }
}