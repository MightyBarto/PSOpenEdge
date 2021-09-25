
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
#elif V121
        private const string VersionSuffix = "12.1";        
#elif V122
        private const string VersionSuffix = "12.2";
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

        /// <summary>
        /// Returns the path that is stored in the specified key.
        /// </summary>
        /// <param name="regKey">the key in the Windows registry.</param>        
        private static string GetVerifiedPathFromRegistry(string regKey)
        {
            if (string.IsNullOrWhiteSpace(regKey))
                throw new ArgumentNullException(nameof(regKey));

            var dir = OeEnvironment.GetRegistryKey(regKey);
            if (!Directory.Exists(dir))
                throw new ArgumentException($"Directory '{dir}' does not exist or is inaccessible");

            return dir;
        }

        /// <summary>
        /// Returns the value for the specified key.
        /// Determines 32-bit or 64-bit, and get's the key from the related path.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>        
        private static string GetRegistryKey(string key)
        {            
#if V122
            // In OE 12.2 there is no 64-bit key.
            var path = OeEnvironment._RegKey32;
#else
            //Determin 32 or 64 bit
            var path = System.Environment.Is64BitOperatingSystem
                ? OeEnvironment._RegKey64
                : OeEnvironment._RegKey32;
#endif
            //Get registry key
            var value = OeEnvironment.GetOsRegistryKey(path, key);
            if (value == null)
                throw new ArgumentException("OpenEdge not installed");

            return value;
        }

        /// <summary>
        /// Assists in getting the key's valueµ.
        /// </summary>
        /// <param name="path">The path to the key.</param>
        /// <param name="key">The key for which to get the value.</param>        
        private static string GetOsRegistryKey(string path, string key)
        {
            var regEntry = Registry.LocalMachine.OpenSubKey(path, false);
            return regEntry?.GetValue(key)?.ToString();
        }
       
        #endregion --- Methods ---
    }
}
