using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using PSOpenEdge.OpenEdge.Database;

namespace PSOpenEdge.Cmdlets
{
    public class PSOpenEdgeCmdletBase : PSCmdlet
    {
        protected const string ParamSetDefault = "Default";
        protected const string ParamSetPipeline = "Pipeline";
        protected const string ParamSetCustom1 = "custom01";


        protected string CurrentDirectory => this.SessionState.Path.CurrentFileSystemLocation.Path;
    }
}
