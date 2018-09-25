using System;
using System.Collections.Generic;
using System.Text;

namespace RXCO.AzureDevOps.REST.Base
{
    public class APISemanticVersionWrapper : IComparer<APISemanticVersionWrapper>
    {
        public APISemanticVersionWrapper(int verMajor, int verMinor)
        {
            VersionMajor = verMajor;
            VersionMinor = verMinor;
        }

        public int VersionMajor { get; private set; }
        public int VersionMinor { get; private set; }
        public String VersionPatch { get; set; }
        public String CustomVersionString { get; set; }

        public override string ToString()
        {
            var result = "";
            StringBuilder sb = new StringBuilder().Append(VersionMajor).Append(".").Append(VersionMinor);
            if (!String.IsNullOrEmpty(CustomVersionString))
            {
                sb.Append(CustomVersionString);
            }
            result = sb.ToString();
            return result;
        }

        int IComparer<APISemanticVersionWrapper>.Compare(APISemanticVersionWrapper x, APISemanticVersionWrapper y)
        {
            int comparisonResult = 0;
            bool IsSameVersion = String.Compare(x.ToString(), y.ToString(), StringComparison.InvariantCulture) == 0;
            if (IsSameVersion)
            {
                comparisonResult = 0;
            }
            else
            {
                // check for less than
                bool isLessThan = (x.VersionMajor < y.VersionMinor) || ((x.VersionMajor == y.VersionMinor) && (x.VersionMinor < y.VersionMinor));
            }
            return comparisonResult;
        }
    }
}
