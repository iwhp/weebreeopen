namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.Linq;

    public partial class StreetAddress
    {
        #region Reference City

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string CityName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.cityName))
                {
                    return this.City.Name;
                }
                return this.cityName;
            }
            set
            {
                this.cityName = value;
            }
        }

        private string cityName;

        #endregion

        #region Reference Region

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public string RegionName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.regionName))
                {
                    return this.Region.Name;
                }
                return this.regionName;
            }
            set
            {
                this.regionName = value;
            }
        }

        private string regionName;

        #endregion

        #region Reference Country

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string CountryName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.countryName))
                {
                    return this.Country.Name;
                }
                return this.countryName;
            }
            set
            {
                this.countryName = value;
            }
        }

        private string countryName;

        public string CountryShort2Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.countryShort2Name))
                {
                    return this.Country.IsoCountryCode2;
                }
                return this.countryShort2Name;
            }
            set
            {
                this.countryShort2Name = value;
            }
        }

        private string countryShort2Name;

        public string CountryShort3Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.countryShort3Name))
                {
                    return this.Country.IsoCountryCode3;
                }
                return this.countryShort3Name;
            }
            set
            {
                this.countryShort3Name = value;
            }
        }

        private string countryShort3Name;

        #endregion

        #region Properties

        /// <summary>
        /// Prefix may contain address prefix information, eg. Mr. Mrs, etc.
        /// </summary>
        public string Prefix { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string StreetAddress3 { get; set; }

        public string PostalCode { get; set; }

        /// <summary>
        /// The Additional Postal Code identifies, if appropriate, any additional Postal Code information.
        /// </summary>
        public string PostalCodeAdditional { get; set; }

        #endregion
    }
}