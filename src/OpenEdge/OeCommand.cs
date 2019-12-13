using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PSOpenEdge.OpenEdge
{
    internal class OeCommand
    {        
        public string Command { get; }
        public string WorkingDirectory { get; private set; }               

        /// <summary>
        /// Used to pass in arguments through std input after the process has started.
        /// </summary>
        /// <typeparam name="string">List inf arguments to input sequentially.</typeparam>
        /// <returns></returns>
        public IList<string> CustomInputs { get; } = new List<string>();

        public OeCommand(string command, string workingDirectory)
        {
            this.Command = command;
            this.WorkingDirectory = workingDirectory;
        }

        public OeCommand(string command, DirectoryInfo workingDirectory = null)
            : this(command, workingDirectory?.FullName)
        { }

        public void Run(params string [] args)
        {
            this.PrintDebugInfo(args);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = this.GetCommandFullName(),
                    WorkingDirectory = this.GetActualWorkingDirectory(),
                    Arguments = this.GetArgumentString(args),
                    RedirectStandardInput = this.CustomInputs.Count > 0,

                    //Set DLC environment var.
                    //Add DLC\bin to beginning of path.
                    EnvironmentVariables =
                    {
                        ["DLC"] = OeEnvironment.DLC,
                        ["PATH"] = this.GetExtendedPath(),
                    },
                   
                    UseShellExecute = false,
                    //RedirectStandardOutput = true,                    
                },
            };            

            process.Start();

            foreach (var input in this.CustomInputs)
            {
#if DEBUG
                Console.WriteLine($"Inputting '{input}'");
#endif
                process.StandardInput.WriteLine(input);
            }

            process.WaitForExit();
        }

        private string GetExtendedPath() =>
            $"{Path.Combine(OeEnvironment.DLC, "bin")}{Path.PathSeparator}{Environment.GetEnvironmentVariable("PATH")}";

        private string GetArgumentString(IEnumerable<string> arguments) => 
            arguments == null
            ? null
            : string.Join(" ", arguments);

        private string GetCommandFullName() =>
            Path.Combine(OeEnvironment.DLC, "bin", this.Command);

        private string GetActualWorkingDirectory() =>
            string.IsNullOrWhiteSpace(this.WorkingDirectory) ||
            !Directory.Exists(this.WorkingDirectory)
                ? OeEnvironment.WRK
                : this.WorkingDirectory;

        [Conditional("DEBUG")]
        private void PrintDebugInfo(IEnumerable<string> arguments)
        {
            Console.WriteLine($"DLC: {OeEnvironment.DLC}");
            Console.WriteLine($"WRK: {OeEnvironment.WRK}");
            Console.WriteLine($"WorkingDirectory: {this.GetActualWorkingDirectory()}");            

            Console.WriteLine($"Command: {this.GetCommandFullName()}");
            Console.WriteLine($"Arguments: {this.GetArgumentString(arguments)}");

            Console.WriteLine($"PATH: {this.GetExtendedPath()}");
        }
    }
}
