
using System;
using System.IO;
using Microsoft.Win32;

namespace PSOpenEdge.OpenEdge
{
    internal static class OeEnvironment
    {
        #region --- Constants ---
#if V115
        private const string VersionSuffix = "11.5";
#elif V117
        private const string VersionSuffix = "11.7";
#endif
        private const string _RegKey32 = @"SOFTWARE\PSC\PROGRESS\" + VersionSuffix;
        private const string _RegKey64 = @"SOFTWARE\WOW6432Node\PSC\PROGRESS\" + VersionSuffix;

        private const string _DlcKey = "installPath";
        private const string _OemDlcKey = "oemInstallPath";
        private const string _WrkKey = "WorkPath";
        private const string _OemWrkKey = "oemWorkPath";

        #endregion --- Constants ---

        #region --- Properties ---

        public static string DLC => OeEnvironment.GetVerifiedPathFromRegistry(OeEnvironment._DlcKey);

        public static string WRK => OeEnvironment.GetVerifiedPathFromRegistry(OeEnvironment._WrkKey);

        public static string OeMgmt => OeEnvironment.GetVerifiedPathFromRegistry(OeEnvironment._OemDlcKey);

        public static string WrkOeMgmt => OeEnvironment.GetVerifiedPathFromRegistry(OeEnvironment._OemWrkKey);

        #endregion --- Properties ---

        #region --- Methods ---

        private static string GetVerifiedPathFromRegistry(string regKey)
        {
            if (string.IsNullOrWhiteSpace(regKey))
                throw new ArgumentNullException(nameof(regKey));

            var dir = OeEnvironment.GetRegistryKey(regKey);
            if (!Directory.Exists(dir))
                throw new ArgumentException($"Directory '{dir}' does not exist or is inaccessible");

            return dir;
        }

        private static string GetRegistryKey(string key)
        {
            //Determin 32 or 64 bit
            var path = System.Environment.Is64BitOperatingSystem
                ? OeEnvironment._RegKey64
                : OeEnvironment._RegKey32;

            //Get registry key
            var value = OeEnvironment.GetOsRegistryKey(path, key);
            if (value == null)
                throw new ArgumentException("OpenEdge not installed");

            return value;
        }

        private static string GetOsRegistryKey(string path, string key)
        {
            var regEntry = Registry.LocalMachine.OpenSubKey(path, false);
            return regEntry?.GetValue(key)?.ToString();
        }
       
        #endregion --- Methods ---
    }
}
