#if DEBUG
using System.Reflection;
using NitroxModel.Helper;
using NitroxPatcher.Patches.Dynamic;

namespace NitroxPatcher.Patches.Persistent;

public sealed partial class MainGameController_ShouldPlayIntro_Patch : NitroxPatch, IPersistentPatch
{
#if SUBNAUTICA
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method(() => MainGameController.ShouldPlayIntro());
#elif BELOWZERO
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((IntroVignette t) => t.ShouldPlayIntro());
#endif

    public static void Postfix(ref bool __result)
    {
        __result = false;
#if SUBNAUTICA
        uGUI_SceneIntro_IntroSequence_Patch.SkipLocalCinematic(uGUI.main.intro);
#elif BELOWZERO
        //TODO: Patch this for BZ
        //uGUI_ExpansionIntro_IntroSequence_Patch.SkipLocalCinematic(uGUI.main.intro);
#endif
    }
}
#endif
