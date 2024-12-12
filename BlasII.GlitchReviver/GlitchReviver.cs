﻿using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BlasII.GlitchReviver;

/// <summary>
/// Reintroduces a variety of useful glitches that have been patched
/// </summary>
public class GlitchReviver : BlasIIMod
{
    internal GlitchReviver() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private readonly List<BaseModule> _modules = [];
    private bool _toggleStatus = false;

    /// <inheritdoc cref="GlitchSettings"/>
    public GlitchSettings CurrentSettings { get; private set; }

    /// <inheritdoc/>
    protected override void OnInitialize()
    {
        CurrentSettings = ConfigHandler.Load<GlitchSettings>();
        LoadModules();

        // Initialize handlers
        InputHandler.RegisterDefaultKeybindings(SetupInput());

        // Call OnStart for all modules
        foreach (var module in _modules)
            module.OnStart();
    }

    /// <inheritdoc/>
    protected override void OnUpdate()
    {
        // If module status was updated, save the config
        if (ProcessInput())
            ConfigHandler.Save(CurrentSettings);

        if (!SceneHelper.GameSceneLoaded)
            return;

        // Call OnUpdate for all modules
        foreach (var module in _modules)
            module.OnUpdate();
    }

    /// <summary>
    /// Creates the keybindings dictionary from all the modules
    /// </summary>
    private Dictionary<string, KeyCode> SetupInput()
    {
        var input = new Dictionary<string, KeyCode>
        {
            { "Activator", KeyCode.F6 },
            { "Toggle_All", KeyCode.KeypadEnter },
        };

        foreach (var module in _modules)
            input.Add($"Toggle_{module.Name}", Enum.Parse<KeyCode>($"Keypad{module.Order}"));

        return input;
    }

    /// <summary>
    /// Checks for qol input and returns whether the config was updated
    /// </summary>
    private bool ProcessInput()
    {
        // Check if activator key is held
        if (!InputHandler.GetKey("Activator"))
            return false;

        // Check if toggle all key was pressed
        if (InputHandler.GetKeyDown("Toggle_All"))
        {
            _toggleStatus = !_toggleStatus;

            foreach (var module in _modules)
                SetModuleStatus(module.Name, _toggleStatus);

            ModLog.Info($"Toggling all modules to {_toggleStatus}");
            return true;
        }

        bool modified = false;
        foreach (var module in _modules)
        {
            // Check if this module's key was pressed
            if (!InputHandler.GetKeyDown($"Toggle_{module.Name}"))
                continue;

            // Toggle the config setting
            ToggleModuleStatus(module.Name);
            modified = true;
        }

        return modified;
    }


    /// <summary>
    /// Toggles the module's setting in the config
    /// </summary>
    private void ToggleModuleStatus(string name)
    {
        PropertyInfo property = typeof(GlitchSettings).GetProperty(name);
        bool status = (bool)property.GetValue(CurrentSettings, null);
        property.SetValue(CurrentSettings, !status);

        ModLog.Info($"Toggling module '{name}' to {!status}");
    }

    /// <summary>
    /// Sets the module's setting in the config
    /// </summary>
    private void SetModuleStatus(string name, bool status)
    {
        PropertyInfo property = typeof(GlitchSettings).GetProperty(name);
        property.SetValue(CurrentSettings, status);

        //ModLog.Info($"Setting module '{name}' to {status}");
    }

    /// <summary>
    /// Loads all modules using reflection
    /// </summary>
    private void LoadModules()
    {
        var modules = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.IsSubclassOf(typeof(BaseModule)))
            .Select(x => (BaseModule)Activator.CreateInstance(x))
            .OrderBy(x => x.Order);

        _modules.AddRange(modules);
        ModLog.Info($"Loaded {_modules.Count} modules");
    }
}
