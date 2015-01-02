namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Party
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/BusinessPartner
    /// </summary>
    public partial class BusinessParty : BaseEntityTenant
    {
        #region Constructors

        protected BusinessParty()
        {
            this.Create();
        }

        public BusinessParty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw (new ArgumentNullException("name"));
            }

            this.Name = name;

            this.Create();
        }

        public BusinessParty(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw (new ArgumentNullException("firstName"));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw (new ArgumentNullException("lastName"));
            }

            this.FirstName = firstName;
            this.LastName = lastName;

            this.Create();
        }

        private void Create()
        {
            this.LocationBusinessParties = new List<LocationBusinessParty>();
        }

        #endregion

        #region Key

        [Key]
        public int BusinessPartyPkId { get; set; }

        /// <summary>
        /// External Number.
        /// </summary>
        public string BusinessPartyNo { get; set; }

        #endregion

        #region References

        public int PreferredInvoicingLocationId { get; set; }

        public virtual ICollection<LocationBusinessParty> LocationBusinessParties { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// If it is an organisation, it contains the legal name.
        /// If it is a person, it contains the firstname and lastname (calculated form FirstName and LastName).
        /// </summary>
        [Required]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    if (string.IsNullOrWhiteSpace(this.FirstName) && string.IsNullOrWhiteSpace(this.LastName))
                    {
                        this.name = value;
                        this.IsPerson = false;
                        this.IsOrganisation = true;
                    }
                    else
                    {
                        this.name = string.Format("{0} {1}", this.FirstName, this.LastName);
                        this.IsPerson = true;
                        this.IsOrganisation = false;
                    }
                }
            }
        }

        private string name;

        public string Name2 { get; set; }

        #endregion

        #region Properties for Organisations

        public bool IsOrganisation { get; protected set; }

        public string TaxId { get; set; }

        #endregion

        #region Properties for Persons

        public bool IsPerson { get; protected set; }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.Name = "SetName"; // Dummy value, value will be calculated in the setter
                }
            }
        }

        private string firstName;

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    this.Name = "SetName"; // Dummy value, value will be calculated in the setter
                }
            }
        }

        private string lastName;




        #endregion

        #region Methods

        public void LocationAdd(Location location)
        {
            this.LocationBusinessParties.Add(new LocationBusinessParty(location, this));
        }

        #endregion
    }
}