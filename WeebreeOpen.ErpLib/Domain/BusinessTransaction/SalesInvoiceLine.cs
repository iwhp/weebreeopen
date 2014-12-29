namespace WeebreeOpen.ErpLib.Domain.BusinessTransaction
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Base;

    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/InvoiceLine
    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/InvoiceLineV2

    public class SalesInvoiceLine : BaseEntity
    {
        public int InvoiceLinePkId { get; set; }

        #region References

        /// <summary>
        /// The Invoice ID uniquely identifies an Invoice Document.
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Indicates the sales representative responsible for the transaction that the document specifies . A valid sales representative is a business partner marked as an employee and a sales representative.
        /// </summary>
        public int SalesRepresentativeId { get; set; }

        /// <summary>
        /// A unique identifier and a reference to a sales order line (product).
        /// </summary>
        public int SalesOrderId { get; set; }
        public int SalesOrderLineId { get; set; }

        /// <summary>
        /// The Goods Shipment Line indicates a unique line in a Shipment document.
        /// </summary>
        public int GoodsShipmentId { get; set; }
        public int GoodsShipmentLineId { get; set; }

        #endregion

        #region General Properties

        /// <summary>
        /// The Invoice Line uniquely identifies a single line of an Invoice.
        /// </summary>
        public int InvoiceLineNo { get; set; }

        /// <summary>
        /// The Project Line indicates a unique project line.
        /// </summary>
        public string ProjectLine { get; set; }

        public string Description { get; set; }

        #endregion

        #region Product

        /// <summary>
        /// Identifies an item which is either purchased or sold in this organization.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Identifies the category which this product belongs to. Product categories are used for pricing.
        /// </summary>
        public int ProductCategoryId { get; set; }

        #endregion

        #region Quantity

        /// <summary>
        /// The Invoiced Quantity indicates the quantity of a product that have been invoiced.
        /// </summary>
        public float QuantityInvoiced { get; set; }

        /// <summary>
        /// Product quantity in the order uom.
        /// </summary>
        public float QuantityOrdered { get; set; }

        /// <summary>
        /// The UOM defines a unique non monetary unit of measure.
        /// </summary>
        public int UnitOfMeasureId { get; set; }

        #endregion

        #region Price

        /// <summary>
        /// The Net List Price is the official price stated by the selected pricelist and the currency of the document.
        /// </summary>
        public float ListPrice { get; set; }

        /// <summary>
        /// The actual price indicates the price for a product in source currency.
        /// </summary>
        public float UnitPrice { get; set; }

        /// <summary>
        /// The Net Price Limit indicates the lowest price for a product stated in the Price List Currency.
        /// </summary>
        public float PriceLimit { get; set; }

        /// <summary>
        /// Indicates the line net amount based on the quantity and the actual price. Any additional charges or freight are not included.
        /// </summary>
        public float LineNetAmount { get; set; }

        #endregion

        #region Discount

        /// <summary>
        /// The Discount indicates the discount applied or taken as a percentage.
        /// </summary>
        public float DiscountPercentage { get; set; }

        /// <summary>
        /// Indicates the discount for this line as an amount.
        /// </summary>
        public float DiscountAmount { get; set; }

        #endregion

        #region Financial Margin

        /// <summary>
        /// The Margin indicates the margin for this product as a percentage of the limit price and selling price.
        /// </summary>
        public float MarginPercentage { get; set; }

        #endregion

        #region Tax

        /// <summary>
        /// The Tax indicates the type of tax for this document line.
        /// </summary>
        public int TaxId { get; set; }

        /// <summary>
        /// The Tax Amount displays the total tax amount for a document.
        /// </summary>
        public float TaxAmount { get; set; }

        #endregion

        #region Charge

        /// <summary>
        /// The Charge indicates a type of Charge (Handling, Shipping, Restocking).
        /// </summary>
        public int ChargeId { get; set; }

        /// <summary>
        /// The Charge Amount indicates the amount for an additional charge.
        /// </summary>
        public float ChargeAmount { get; set; }

        #endregion
    }
}
