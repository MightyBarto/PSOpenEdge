using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

namespace PSOpenEdge.Common
{
    internal static class TemplateFactory
    {
        /// <summary>
        /// Loads an ABL template from embedded resources, and fills the templated fields.
        /// </summary>
        /// <param name="name">The name of the Abl template (embedded resource notation)</param>
        /// <param name="templateFields">Contains the name-value pairs by which the template fields will be replaced.</param>
        /// <returns>The Abl script, being the template with the templated fields replaced.</returns>
        public static string LoadAblScript(string name, (string Key, string Value) [] templateFields = null)
        {
            var template = new StringBuilder(LoadResourceAsString(name));

            if(templateFields == null)
                return template.ToString();

            foreach(var field in templateFields)
                template.Replace($"[[{field.Key}]]",field.Value);
            
            return template.ToString();
        }

        /// <summary>
        /// Loads resource from embedded resources and returns the textual content.
        /// </summary>
        /// <param name="name">The name of the resource to load. The assembly containing the resources should not be included and will be appended.</param>
        /// <returns>The textual contents of the resource.</returns>
        private static string LoadResourceAsString(string name)
        {
            var assembly = typeof(TemplateFactory).Assembly;
            var resourceName = $"{assembly.GetName().Name}.{name}";
            var resourceStream = assembly.GetManifestResourceStream(resourceName);

            using(var reader = new StreamReader(resourceStream))            
                return reader.ReadToEnd();    
        }

    }

}