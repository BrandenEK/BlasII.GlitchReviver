using HarmonyLib;
using Il2CppTGK.Game.Components.Attack.Requesters;

namespace BlasII.GlitchReviver.Modules;

internal class MeaCulpaHover : BaseModule
{
    public override string Name { get; } = "MeaCulpaHover";
    public override int Order { get; } = 2;
}

/// <summary>
/// Always set numProjectiles to 1.  Not exactly sure why this works but it does
/// </summary>
[HarmonyPatch(typeof(MeaCulpaProjectileAirAttackRequester), nameof(MeaCulpaProjectileAirAttackRequester.Evaluate))]
class MeaCulpaProjectileAirAttackRequester_Evaluate_Patch
{
    public static void Postfix(MeaCulpaProjectileAirAttackRequester __instance)
    {
        if (!Main.GlitchReviver.CurrentSettings.MeaCulpaHover)
            return;

        __instance.numOfProjectiles = 1;
    }
}
[HarmonyPatch(typeof(MeaCulpaProjectileDirectionalAirAttackRequester), nameof(MeaCulpaProjectileDirectionalAirAttackRequester.Evaluate))]
class MeaCulpaProjectileDirectionalAirAttackRequester_Evaluate_Patch
{
    public static void Postfix(MeaCulpaProjectileDirectionalAirAttackRequester __instance)
    {
        if (!Main.GlitchReviver.CurrentSettings.MeaCulpaHover)
            return;

        __instance.numOfProjectiles = 1;
    }
}
