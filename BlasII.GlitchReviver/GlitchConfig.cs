
namespace BlasII.GlitchReviver;

/// <summary>
/// Info about which glitches are allowed
/// </summary>
public class GlitchConfig
{
    /// <summary>
    /// Whether ruego/mc dives can be activated by canceling mirabras
    /// </summary>
    public bool AllowMirabrasDive { get; set; }

    /// <summary>
    /// Whether mc projectiles can be spawned by swapping directions
    /// </summary>
    public bool AllowMeaCulpaHover { get; set; }
}
