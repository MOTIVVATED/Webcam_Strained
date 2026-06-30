using System;

public static class GameSceneOrder
{
	public static readonly string[] Scenes =
	{
		"MafuLegenda",
		"ApexFunk",
		"PampyBam",
		"EnterYou",
		"SmellySam",
		"MadiMeows"
	};

	public static int IndexOf(string sceneName) => Array.IndexOf(Scenes, sceneName);
}
