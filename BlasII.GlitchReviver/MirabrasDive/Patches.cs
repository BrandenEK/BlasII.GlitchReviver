using BlasII.ModdingAPI.Assets;
using HarmonyLib;
using Il2CppLightbug.Kinematic2D.Implementation;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.Abilities;
using Il2CppTGK.Game.Components.Attack.Data;

namespace BlasII.GlitchReviver.MirabrasDive;

[HarmonyPatch(typeof(ChangeWeaponAbility), nameof(ChangeWeaponAbility.OnUpdate))]
class ChangeWeaponAbility_OnUpdate_Patch
{
    public static void Postfix()
    {
        if (!Main.GlitchReviver.CurrentConfig.AllowMirabrasDive)
            return;

        if (!Main.GlitchReviver.InputHandler.GetButtonDown(ModdingAPI.Input.ButtonType.NextWeapon))
            return;

        if (CoreCache.EquipmentManager.CountUnlockedWeapons() < 2)
            return;

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

        CoreCache.PlayerSpawn.PlayerControllerRef.GetAbility<ChangeWeaponAbility>().ChangeWeapon(nextWeapon);
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
