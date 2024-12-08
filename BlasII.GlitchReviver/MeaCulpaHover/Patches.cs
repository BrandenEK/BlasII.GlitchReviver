using HarmonyLib;
using Il2CppTGK.Game.Components.Attack.Requesters;

namespace BlasII.GlitchReviver.MeaCulpaHover;

//[HarmonyLib.HarmonyPatch(typeof(MeaCulpaPhantomTeleportAbility), nameof(MeaCulpaPhantomTeleportAbility.CanUseProjectileSpawnerAttack))]
//class t
//{
//    public static void Postfix(ref bool __result)
//    {
//        __result = true;
//    }
//}

//[HarmonyLib.HarmonyPatch(typeof(MeaCulpaProjectileDirectionalAirAttackRequester), nameof(MeaCulpaProjectileDirectionalAirAttackRequester.OnAttack))]
//class t2
//{
//    public static void Prefix(MeaCulpaProjectileDirectionalAirAttackRequester __instance)
//    {
//        ModLog.Info("On attack");
//        __instance.ResetNumOfProjectiles();
//        __instance.numOfProjectiles = 0;
//    }

//    public static void Postfix(MeaCulpaProjectileDirectionalAirAttackRequester __instance)
//    {
//        ModLog.Info("On attack");
//        __instance.ResetNumOfProjectiles();
//        __instance.numOfProjectiles = 0;
//    }
//}

[HarmonyPatch(typeof(MeaCulpaProjectileAirAttackRequester), nameof(MeaCulpaProjectileAirAttackRequester.Evaluate))]
class t3
{
    public static void Postfix(MeaCulpaProjectileAirAttackRequester __instance, ref bool __result)
    {
        //ModLog.Info("Evaluating horizontal");
        //__result = Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.W);
        __instance.numOfProjectiles = 1;
        //__result = __instance.attackRequested;
    }
}

[HarmonyPatch(typeof(MeaCulpaProjectileDirectionalAirAttackRequester), nameof(MeaCulpaProjectileDirectionalAirAttackRequester.Evaluate))]
class t4
{
    public static void Postfix(MeaCulpaProjectileDirectionalAirAttackRequester __instance, ref bool __result)
    {
        //ModLog.Info("Evaluating vertical");
        //__result = Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.W);
        __instance.numOfProjectiles = 1;
        //__result = __instance.attackRequested;
    }
}
