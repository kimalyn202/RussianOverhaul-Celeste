using System;

namespace Celeste.Mod.RussianOverhaul;

public class RussianOverhaulModule : EverestModule
{
    public static RussianOverhaulModule Instance { get; private set; }

    public override Type SettingsType => typeof(RussianOverhaulModuleSettings);
    public static RussianOverhaulModuleSettings Settings => (RussianOverhaulModuleSettings) Instance._Settings;

    public RussianOverhaulModule()
    {
        Instance = this;
    }

    public override void Load()
    {
        Settings.OnFontChange = ProcessSettingsChange;
        On.Celeste.Dialog.MergeLanguages += OnMerge;
    }

    public override void Unload()
    {
        On.Celeste.Dialog.MergeLanguages -= OnMerge;
    }

    private static void ProcessSettingsChange()
    {
        if (Dialog.Language.Id == "russian")
        {
            Fonts.Load(Settings.Font.ToLowerInvariant());
            var previousFontFace = Dialog.Language.FontFace;
            Dialog.Language.FontFace = Settings.Font.ToLowerInvariant();
            Fonts.Unload(previousFontFace);
            global::Celeste.Settings.Instance.ApplyLanguage();
        }
    }

    private static Language OnMerge(On.Celeste.Dialog.orig_MergeLanguages orig, Language origLang, Language modLang)
    {
        var lang = orig(origLang, modLang);

        if (modLang != null && lang.Id == "russian")
        {
            lang.FontFace = Settings.Font.ToLowerInvariant();
            lang.FontFaceSize = 64;
            lang.IconPath = modLang.IconPath;
            lang.Icon = modLang.Icon;
            lang.Order = modLang.Order;
        }

        return lang;
    }
}
