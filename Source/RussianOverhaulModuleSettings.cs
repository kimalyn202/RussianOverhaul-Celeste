using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Celeste.Mod.RussianOverhaul;

public class RussianOverhaulModuleSettings : EverestModuleSettings
{
	[SettingIgnore, YamlIgnore]
	private static List<string> Fonts => ["Ubuntu", "Nunito", "Noto Sans", "Century Gothic"];

	public string Font { get; set; } = Fonts[0];

	[SettingIgnore, YamlIgnore]
	internal Action OnFontChange { get; set; }

	public void CreateFontEntry(TextMenu menu, bool inGame)
	{
		if (inGame)
		{
			return;
		}

		menu.Add(
			new TextMenu.Slider(Dialog.Clean("modoptions_russianoverhaul_font"), index => Fonts[index], 0, Fonts.Count - 1, Math.Max(0, Fonts.IndexOf(Font)))
				.Change(
					x =>
					{
						Font = Fonts[x];
						OnFontChange();
					}));
	}
}
