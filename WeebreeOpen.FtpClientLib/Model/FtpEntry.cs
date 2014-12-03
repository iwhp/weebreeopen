namespace WeebreeOpen.FtpClientLib.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FtpEntry
    {
        public override string ToString()
        {
            return "FtpEntry: " + this.FtpEntryType + " " + this.DirectoryPath + " " + this.Name + " " + this.DateTime + " " + this.Size;
        }

        public FtpEntryType FtpEntryType { get; set; }

        public string Name { get; set; }

        public string DirectoryPath { get; set; }


        public DateTime DateTime { get; set; }

        public int Size { get; set; }
    }
}
