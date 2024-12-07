using MelonLoader;

namespace BlasII.GlitchReviver;

internal class Main : MelonMod
{
    public static GlitchReviver GlitchReviver { get; private set; }

    public override void OnLateInitializeMelon()
    {
        GlitchReviver = new GlitchReviver();
    }
}