using System;

public static class GameSceneOrder
{
	public static readonly string[] Scenes =
	{
		"MafuLegenda",
		"ApexFunk",
		"EnterYou",
		"MadiMeows",
		"SmellySam",
		"PampyBam"
	};

	public static int IndexOf(string sceneName) => Array.IndexOf(Scenes, sceneName);
}
