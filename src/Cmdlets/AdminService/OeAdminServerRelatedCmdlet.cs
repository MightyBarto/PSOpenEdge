using System.Management.Automation;
using System.Security;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    public class OeAdminServerRelatedCmdletBase : PSOpenEdgeCmdletBase
    {
        [Parameter(ParameterSetName = ParamSetsCustom.Default)]
        [Parameter(ParameterSetName = ParamSetsCustom.WithAuthentication)]
        public string HostName { get; set; }

        [Parameter(ParameterSetName = ParamSetsCustom.Default)]
        [Parameter(ParameterSetName = ParamSetsCustom.WithAuthentication)]
        [Alias("Port")]
        public int AdminServicePort { get; set; } = 20931;

        //[Parameter(ParameterSetName = ParamSetsCustom.Default)]
        //[Parameter(ParameterSetName = ParamSetsCustom.WithAuthentication)]
        //public SwitchParameter All { get; set; }

        //[Parameter(ParameterSetName = ParamSetsCustom.Default)]
        [Parameter(ParameterSetName = ParamSetsCustom.WithAuthentication, Mandatory = true)]
        [Alias("User", "Usr")]
        public string UserName { get; set; }

        //[Parameter(ParameterSetName = ParamSetsCustom.Default)]
        [Parameter(ParameterSetName = ParamSetsCustom.WithAuthentication, Mandatory = true)]
        [Alias("Pw")]
        public SecureString Password { get; set; }
    }
}
