
namespace BlasII.GlitchReviver;

/// <summary>
/// Info about which glitches are allowed
/// </summary>
public class GlitchSettings
{
    /// <summary>
    /// Whether the Mirabras prayer can be cancelled by weapon swapping
    /// </summary>
    public bool MirabrasCancel { get; set; }

    /// <summary>
    /// Whether mc projectiles can be spawned by swapping directions
    /// </summary>
    public bool MeaCulpaHover { get; set; }
}
