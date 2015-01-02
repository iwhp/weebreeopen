namespace WeebreeOpen.ErpLib.Infractructure.MsSql
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    public partial class ErpDbContext : BaseDbContext
    {
        public ErpDbContext(string nameOrConnectionString = "Name=ErpDbContext", string currentUsername = "")
            : base(nameOrConnectionString, currentUsername)
        {
        }
    }
}