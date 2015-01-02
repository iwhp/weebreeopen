namespace WeebreeOpen.ErpLib.Application.Service
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;

    public class CustomerService
    {
        public BusinessPartyCustomer CustomerNew(BusinessPartyCustomer businessPartyCustomer)
        {
            businessPartyCustomer.BusinessPartyPkId = 1;
            return businessPartyCustomer;
        }
    }
}