using HarmonyLib;
using Il2CppTGK.Game.Components.Attack.Requesters;

namespace BlasII.GlitchReviver.MeaCulpaHover;

/// <summary>
/// Always set numProjectiles to 1.  Not exactly sure why this works but it does
/// </summary>
[HarmonyPatch(typeof(MeaCulpaProjectileAirAttackRequester), nameof(MeaCulpaProjectileAirAttackRequester.Evaluate))]
class MeaCulpaProjectileAirAttackRequester_Evaluate_Patch
{
    public static void Postfix(MeaCulpaProjectileAirAttackRequester __instance)
    {
        if (Main.GlitchReviver.CurrentConfig.AllowMeaCulpaHover)
            __instance.numOfProjectiles = 1;
    }
}
[HarmonyPatch(typeof(MeaCulpaProjectileDirectionalAirAttackRequester), nameof(MeaCulpaProjectileDirectionalAirAttackRequester.Evaluate))]
class MeaCulpaProjectileDirectionalAirAttackRequester_Evaluate_Patch
{
    public static void Postfix(MeaCulpaProjectileDirectionalAirAttackRequester __instance)
    {
        if (Main.GlitchReviver.CurrentConfig.AllowMeaCulpaHover)
            __instance.numOfProjectiles = 1;
    }
}
