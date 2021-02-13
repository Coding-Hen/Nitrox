using System.IO;

namespace NitroxLauncher.Patching
{
    public class PlatformDetection
    {
        public static bool IsEpic(string subnauticaPath)
        {

            if (Directory.Exists(Path.Combine(subnauticaPath, ".egstore")))
            {
                return true;
            }
            return false;
        }

        public static bool IsSteam(string subnauticaPath)
        {
#if DEBUG
            if (File.Exists(Path.Combine(subnauticaPath, "Subnautica_Data", "Plugins", "CSteamworks.dll")))
#elif BELOWZERO
            if (File.Exists(Path.Combine(subnauticaPath, "SubnauticaZero_Data", "Plugins", "x86_64", "CSteamworks.dll")))
#endif
            {
                return true;
            }
            return false;
        }
        public static bool IsMicrosoftStore(string subnauticaPath)
        {
            if (File.Exists(Path.Combine(subnauticaPath, "appxmanifest.xml")))
            {
                return true;
            }
            return false;
        }
    }
}
