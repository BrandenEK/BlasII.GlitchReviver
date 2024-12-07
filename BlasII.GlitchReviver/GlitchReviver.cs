using BlasII.ModdingAPI;

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
    }
}
