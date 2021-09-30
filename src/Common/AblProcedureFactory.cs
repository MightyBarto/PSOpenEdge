using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Reflection;
using PSOpenEdge.OpenEdge;

namespace PSOpenEdge.Common
{
    internal static class AblProcedureFactory
    {
        private static Assembly Assembly => typeof(AblProcedureFactory).Assembly;
        private static string AssemblyName => AblProcedureFactory.Assembly.GetName().Name;

        /// <summary>
        /// Places an abl procedure, which must be found in the embedded resources, into the wrk directory. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public static string GetAblProcedure(string procedureName)
        {
            if (string.IsNullOrWhiteSpace(procedureName))
                throw new ArgumentNullException(nameof(procedureName));

            procedureName = procedureName.Replace(".p", ".r");
            var procedure = LoadResourceAsBinary($"ablprocedures.{OeEnvironment.VersionCode}.{procedureName}");

            var assemblyName = AblProcedureFactory.AssemblyName;    
            var folder = Path.Combine(OeEnvironment.WRK, assemblyName);

            if(!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fullPath = Path.Combine(folder, procedureName);

            File.WriteAllBytes(fullPath, procedure);

            return fullPath;
        }

        /// <summary>
        /// Loads resource from embedded resources and returns the textual content.
        /// </summary>
        /// <param name="name">The name of the resource to load. The assembly containing the resources should not be included and will be appended.</param>
        /// <returns>The textual contents of the resource.</returns>
        private static string LoadResourceAsString(string name)
        {
            var assembly = AblProcedureFactory.Assembly;
            var resourceName = $"{assembly.GetName().Name}.{name}";

            using var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(resourceStream);

            return reader.ReadToEnd();
        }

        /// <summary>
        /// Loads resource from embedded resources and returns the binary content.
        /// </summary>
        /// <param name="name">The name of the resource to load. The assembly containing the resources should not be included and will be appended.</param>
        /// <returns>The binary contents of the resource.</returns>
        private static byte[] LoadResourceAsBinary(string name)
        {
            var assembly = AblProcedureFactory.Assembly;
            var resourceName = $"{assembly.GetName().Name}.{name}";

            using var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using var memSteam = new MemoryStream();

            resourceStream.CopyTo(memSteam);
            return memSteam.ToArray();
        }

    }

}