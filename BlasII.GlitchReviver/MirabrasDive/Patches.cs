using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Assets;
using HarmonyLib;
using Il2CppLightbug.Kinematic2D.Implementation;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.Abilities;
using Il2CppTGK.Game.Components.Abilities.Inputs;
using Il2CppTGK.Game.Components.Attack.Data;
using Il2CppTGK.Game.Components.Prayers;
using UnityEngine;

namespace BlasII.GlitchReviver.MirabrasDive;

[HarmonyPatch(typeof(FullPrayerAbility), nameof(FullPrayerAbility.OnUpdate))]
class ChangeWeaponAbility_OnUpdate_Patch
{
    public static void Postfix(FullPrayerAbility __instance)
    {
        if (!Main.GlitchReviver.CurrentConfig.AllowMirabrasDive)
            return;

        //if (!Main.GlitchReviver.InputHandler.GetButtonDown(ModdingAPI.Input.ButtonType.NextWeapon))
        //    return;
        if (!Input.GetKeyDown(KeyCode.R))
            return;

        if (CoreCache.EquipmentManager.CountUnlockedWeapons() < 2)
            return;

        ModLog.Info("Activating module 'MirabrasDive'");

        var changeWeapon = AssetStorage.Abilities[ABILITY_IDS.ChangeWeapon];
        var fullPrayer = AssetStorage.Abilities[ABILITY_IDS.FullPrayer];

        var controller = CoreCache.PlayerSpawn.PlayerInstance.GetComponent<CharacterController2D>();
        controller.CancelAbility(fullPrayer);
        controller.ActivateAbilityByType(changeWeapon);

        WeaponID currentWeapon = CoreCache.EquipmentManager.GetCurrentWeapon();
        int nextWeaponSlot = CoreCache.EquipmentManager.GetWeaponSlot(currentWeapon) + 1;
        if (nextWeaponSlot >= CoreCache.EquipmentManager.GetNumWeaponSlots())
            nextWeaponSlot = 0;
        WeaponID nextWeapon = CoreCache.EquipmentManager.GetAssignedWeaponToSlot(nextWeaponSlot);

        //__instance.ChangeWeapon(nextWeapon);
    }
}

[HarmonyPatch(typeof(ChangeWeaponAbility), nameof(ChangeWeaponAbility.OnIgnore))]
class ChangeWeaponAbility_OnUpdate_Patch2
{
    public static void Postfix(ChangeWeaponAbility __instance)
    {

        ModLog.Warn("ignore");
    }
}

[HarmonyPatch(typeof(ChangeWeaponInputsReaderWithMeaCulpa), nameof(ChangeWeaponInputsReaderWithMeaCulpa.ConsumeInput))]
class t
{
    public static void Postfix()
    {
        ModLog.Warn("Consume");
    }
}

[HarmonyPatch(typeof(ChangeWeaponInputsReaderWithMeaCulpa), nameof(ChangeWeaponInputsReaderWithMeaCulpa.ProcessCharacterActionsInfo))]
class t2
{
    public static void Postfix()
    {
        //ModLog.Warn("process");
    }
}


// These only work if not in the prayer, so I have to use the old method

//[HarmonyPatch(typeof(ChangeWeaponAbility), nameof(ChangeWeaponAbility.RequestFastChange))]
//class ChangeWeaponAbility_RequestFastChange_Patch
//{
//    public static void Postfix()
//    {
//        ModLog.Error("request weapon change");
//    }
//}
//[HarmonyPatch(typeof(ChangeWeaponAbility), nameof(ChangeWeaponAbility.RequestFastChangeNextWeapon))]
//class ChangeWeaponAbility_RequestFastChangeNextWeapon_Patch
//{
//    public static void Postfix()
//    {
//        ModLog.Error("request weapon change");
//    }
//}
//[HarmonyPatch(typeof(ChangeWeaponAbility), nameof(ChangeWeaponAbility.RequestFastChangePrevWeapon))]
//class ChangeWeaponAbility_RequestFastChangePrevWeapon_Patch
//{
//    public static void Postfix()
//    {
//        ModLog.Error("request weapon change");
//    }
//}
