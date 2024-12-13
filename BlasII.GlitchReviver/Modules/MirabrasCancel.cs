using BlasII.ModdingAPI.Assets;
using HarmonyLib;
using Il2CppLightbug.Kinematic2D.Implementation;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.Abilities;
using Il2CppTGK.Game.Components.Prayers;

namespace BlasII.GlitchReviver.Modules;

internal class MirabrasCancel : BaseModule
{
    public override string Name { get; } = "MirabrasCancel";
    public override int Order { get; } = 1;
}

/// <summary>
/// Perform weapon swap when pressing the button during a prayer
/// </summary>
[HarmonyPatch(typeof(FullPrayerAbility), nameof(FullPrayerAbility.OnUpdate))]
class FullPrayerAbility_OnUpdate_Patch
{
    public static void Postfix(FullPrayerAbility __instance)
    {
        if (!Main.GlitchReviver.CurrentSettings.MirabrasCancel)
            return;

        if (__instance.GetState() != AbilityState.EXECUTING)
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

        CoreCache.PlayerSpawn.PlayerControllerRef.GetAbility<ChangeWeaponAbility>().RequestFastChangeNextWeapon();
    }
}