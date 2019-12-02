using System;
using System.Management.Automation;
using System.IO;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, NounsCustom.OeVersion)]
    [Alias("oeversion")]
    public class GetOeVersionCmdlet : PSOpenEdgeCmdletBase
    {
        protected override void ProcessRecord()
        {
            var versionFile = Path.Combine(OeEnvironment.DLC, "version");

            if (!File.Exists(versionFile))
                throw new InvalidOperationException("OpenEdge not properly installed");

            Console.WriteLine(File.ReadAllText(versionFile));
        }
    }
}
