using System;
using System.Management.Automation;
using PSOpenEdge.Powershell;
using OeEnvironment = PSOpenEdge.OpenEdge.OeEnvironment;

namespace PSOpenEdge.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, NounsCustom.OeEnvironment)]
    [Alias("oeenv")]
    public class GetOeEnvironmentCmdlet : PSOpenEdgeCmdletBase
    {
        protected override void ProcessRecord()
        {           
            Console.WriteLine($"Dlc: {OeEnvironment.DLC}");
            Console.WriteLine($"Wrk: {OeEnvironment.WRK}");
            Console.WriteLine($"OeMgmt: {OeEnvironment.OeMgmt}");
            Console.WriteLine($"WrkOeMgmt: {OeEnvironment.WrkOeMgmt}");
        }
    }
}
