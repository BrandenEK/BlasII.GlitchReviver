﻿using BlasII.ModdingAPI;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.GlitchReviver;

/// <summary>
/// Reintroduces a variety of useful glitches that have been patched
/// </summary>
public class GlitchReviver : BlasIIMod
{
    internal GlitchReviver() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    /// <summary>
    /// The current settings for which glitches are allowed
    /// </summary>
    public GlitchConfig CurrentConfig { get; private set; }

    /// <summary>
    /// Loads the initial config and registers keybindings
    /// </summary>
    protected override void OnInitialize()
    {
        CurrentConfig = ConfigHandler.Load<GlitchConfig>();

        InputHandler.RegisterDefaultKeybindings(new Dictionary<string, KeyCode>()
        {
            { "Activator", KeyCode.F6 },
            { "MirabrasDive", KeyCode.Keypad1 },
            { "MeaCulpaHover", KeyCode.Keypad2 },
        });
    }

    /// <summary>
    /// Checks for input and updates modules
    /// </summary>
    protected override void OnUpdate()
    {
        // If a glitch status was updated, save the config
        if (ProcessInput())
            ConfigHandler.Save(CurrentConfig);
    }

    /// <summary>
    /// Checks for glitch input and returns whether the config was updated
    /// </summary>
    private bool ProcessInput()
    {
        if (!InputHandler.GetKey("Activator"))
            return false;

        if (InputHandler.GetKeyDown("MirabrasDive"))
        {
            CurrentConfig.AllowMirabrasDive = !CurrentConfig.AllowMirabrasDive;
            ModLog.Info($"Toggling module 'AllowMirabrasDive' to {CurrentConfig.AllowMirabrasDive}");
            return true;
        }

        if (InputHandler.GetKeyDown("MeaCulpaHover"))
        {
            CurrentConfig.AllowMeaCulpaHover = !CurrentConfig.AllowMeaCulpaHover;
            ModLog.Info($"Toggling module 'AllowMeaCulpaHover' to {CurrentConfig.AllowMeaCulpaHover}");
            return true;
        }

        return false;
    }
}
