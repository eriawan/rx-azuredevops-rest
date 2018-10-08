using System;
using System.Collections.Generic;
using System.Text;

namespace RXCO.AzureDevOps.REST.Base
{
    /// <summary>A simple class wrapper for Azure DevOps REST API and to compare equality and also check for lesser and greater version.</summary>
    /// <remarks><para>Normally, Azure DevOps REST API has only "major.minor" version model for non-preview API and "major.minor-preview" for API preview.</para>
    /// <para>The Semantic Versioning 2.0 (semver 2.0) dictates that usually API version is "major.minor.patch" model. Therefore </para></remarks>
    /// <example><para>Comparing an exact version should be done as directly comparing the resulting toString.</para>
    /// <para>Example of same versions version "2.1" with "2.1", "2.1preview" with "2.1preview".</para>
    /// </example>
    public sealed class APISemanticVersionWrapper : IComparer<APISemanticVersionWrapper>, IComparable<APISemanticVersionWrapper>
    {
        public APISemanticVersionWrapper(int verMajor, int verMinor)
        {
            VersionMajor = verMajor;
            VersionMinor = verMinor;
        }

        public APISemanticVersionWrapper(int verMajor, int verMinor, int verPatch)
        {
            VersionMajor = verMajor;
            VersionMinor = verMinor;
            VersionPatch = verPatch;
        }

        /// <summary>Major version part.</summary>
        public int VersionMajor { get; private set; }

        /// <summary>Minor verion part.</summary>
        public int VersionMinor { get; private set; }

        /// <summaryPatch version part.</summary>
        public int VersionPatch { get; private set; } = 0;

        public String CustomVersionString { get; set; }

        #region Comparison operator implementation
        public static bool operator >(APISemanticVersionWrapper versionA, APISemanticVersionWrapper versionB)
        {
            var result = false;
            result = APISemanticVersionWrapper.Compare(versionA, versionB) > 0;
            return result;
        }
        public static bool operator <(APISemanticVersionWrapper versionA, APISemanticVersionWrapper versionB)
        {
            var result = false;
            result = APISemanticVersionWrapper.Compare(versionA, versionB) < 0;
            return result;
        }

        public static bool operator ==(APISemanticVersionWrapper versionA, APISemanticVersionWrapper versionB)
        {
            var result = false;
            result = APISemanticVersionWrapper.Compare(versionA, versionB) == 0;
            return result;
        }

        public static bool operator !=(APISemanticVersionWrapper versionA, APISemanticVersionWrapper versionB)
        {
            var result = false;
            result = APISemanticVersionWrapper.Compare(versionA, versionB) != 0;
            return result;
        }
        #endregion

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

        public override bool Equals(object obj)
        {
            var semverIsEqual = false;
            var checkSemVerType = obj as APISemanticVersionWrapper;
            if (checkSemVerType != null)
            {
                semverIsEqual = (this.CompareTo(checkSemVerType) == 0);
            }
            return semverIsEqual;
        }

        #region Implicit IComparer<T> and IComparable<T> for convenience

        public int CompareTo(APISemanticVersionWrapper otherVersion)
        {
            var comparisonResult = 0;
            var tempCompare = (IComparer<APISemanticVersionWrapper>)this;
            comparisonResult = tempCompare.Compare(this, otherVersion);
            return comparisonResult;
        }

        public static int Compare(APISemanticVersionWrapper versionA, APISemanticVersionWrapper versionB)
        {
            var comparisonResult = 0;
            var tempCompare = (IComparer<APISemanticVersionWrapper>)versionA;
            comparisonResult = tempCompare.Compare(versionA, versionB);
            return comparisonResult;
        }

        #endregion

        #region Explicit IComparer<T> and IComparable<T> implementation

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
                bool isLessThan = false;
                isLessThan = (x.VersionMajor < y.VersionMajor) || ((x.VersionMajor == y.VersionMajor) && (x.VersionMinor < y.VersionMinor));
                isLessThan = isLessThan || ((x.VersionMajor == y.VersionMajor) && (x.VersionMinor == y.VersionMinor) && (x.VersionPatch < y.VersionPatch));
                if (isLessThan)
                {
                    comparisonResult = -1;
                }
                else
                {
                    // check for greater than
                    bool isGreaterThan = false;
                    isGreaterThan = (x.VersionMajor > y.VersionMajor) || ((x.VersionMajor == y.VersionMajor) && (x.VersionMinor > y.VersionMinor));
                    isGreaterThan = isGreaterThan || ((x.VersionMajor == y.VersionMajor) && (x.VersionMinor == y.VersionMinor) && (x.VersionPatch > y.VersionPatch));
                    if (isGreaterThan)
                    {
                        comparisonResult = 1;
                    }
                }
            }
            return comparisonResult;
        }

        int IComparable<APISemanticVersionWrapper>.CompareTo(APISemanticVersionWrapper other)
        {
            var comparisonResult = 0;
            var versionAComparer = (IComparer<APISemanticVersionWrapper>)this;
            comparisonResult = versionAComparer.Compare(this, other);
            return comparisonResult;
        }

        #endregion

    }
}
