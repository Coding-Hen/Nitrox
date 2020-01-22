using System;
using System.Reflection;
using Harmony;
using NitroxClient.GameLogic;
using NitroxModel.Core;

namespace NitroxPatcher.Patches.Dynamic
{
    public class PrecursorDoorway_Patch : NitroxPatch, IDynamicPatch
    {
        public static readonly Type TARGET_CLASS = typeof(PrecursorDoorway);
        public static readonly MethodInfo TARGET_METHOD = TARGET_CLASS.GetMethod("ToggleDoor", BindingFlags.Public | BindingFlags.Instance);

        public static void Postfix(PrecursorDoorway __instance, bool open)
        {
            NitroxServiceLocator.LocateService<Precursor>().OnDoorToggledByMe(__instance, open);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TARGET_METHOD);
        }
    }
}
