﻿using System.Collections.Generic;
using System.IO;

namespace NitroxModel.Discovery.InstallationFinders
{
    /// <summary>
    ///     Tries to read a local file that contains the installation directory of Subnautica.
    /// </summary>
    public class ConfigFileGameFinder : IFindGameInstallation
    {
        private const string FILENAME = "path.txt";

        public string FindGame(IList<string> errors = null)
        {
            if (!File.Exists(FILENAME))
            {
                errors?.Add($@"Game installation directory config file is not set. Create a '{FILENAME}' in directory: '{Directory.GetCurrentDirectory()}' with the path to the Subnautica installation directory.");
                return null;
            }

            string path = File.ReadAllText(FILENAME).Trim();
            if (string.IsNullOrEmpty(path))
            {
                errors?.Add($@"Config file {Path.GetFullPath(FILENAME)} was found empty. Please enter the path to the Subnautica installation.");
                return null;
            }
#if SUBNAUTICA
            if (!Directory.Exists(Path.Combine(path, "Subnautica_Data", "Managed")))
#elif BELOWZERO
            if (!Directory.Exists(Path.Combine(path, "SubnauticaZero_Data", "Managed")))
#endif
            {
                errors?.Add($@"Game installation directory config file {path} is invalid. Please enter the path to the Subnautica installation.");
                return null;
            }

            return path;
        }
    }
}
