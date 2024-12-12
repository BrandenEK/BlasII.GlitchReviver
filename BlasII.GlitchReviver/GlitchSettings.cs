
namespace BlasII.GlitchReviver;

/// <summary>
/// Info about which glitches are allowed
/// </summary>
public class GlitchSettings
{
    /// <summary>
    /// Whether ruego/mc dives can be activated by canceling mirabras
    /// </summary>
    public bool MirabrasDive { get; set; }

    /// <summary>
    /// Whether mc projectiles can be spawned by swapping directions
    /// </summary>
    public bool MeaCulpaHover { get; set; }
}
