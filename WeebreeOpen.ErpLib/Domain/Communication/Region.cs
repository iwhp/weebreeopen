namespace WeebreeOpen.ErpLib.Domain.Communication
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Base;

    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Region

    /// <summary>
    /// The Region Tab defines a Region within a Country. This tab is enabled only if the Has Region checkbox is selected for the Country.
    /// </summary>
    public class Region : BaseEntity
    {
        public int RegionPkId { get; set; }

        public int CountryId { get; set; }

        /// <summary>
        ///  A more descriptive identifier (that does need to be unique) of a record/document that is used as a default search option along with the search key (that is unique and mostly shorter). It is up to 60 characters in length.
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
