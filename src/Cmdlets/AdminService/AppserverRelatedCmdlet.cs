
using System.Management.Automation;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    public abstract class AppserverRelatedCmdlet : AdminServerRelatedCmdletBase
    {
        #region --- Properties ---

        [Parameter(ParameterSetName = ParamSetsCustom.Default)]
        [Parameter(ParameterSetName = ParamSetsCustom.WithAuthentication)]
        public string Name { get; set; }

        #endregion --- Properties ---

        #region --- Methods ---

        protected string GetAppServerNameArgs()
        {
            return string.IsNullOrWhiteSpace(this.Name) ||
                   this.Name == "*"
                ? "-all"
                : $"-name {this.Name}";
        }

        #endregion --- Methods ---

    }
}
