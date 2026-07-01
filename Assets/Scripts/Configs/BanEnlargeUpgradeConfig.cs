using UnityEngine;

[CreateAssetMenu(fileName = "BanEnlargeUpgradeConfig", menuName = "Upgrades/Ban Enlarge Config")]
public class BanEnlargeUpgradeConfig : ScriptableObject
{
	[Header("Ban (PlayerSmashing)")]
	public int banRank = 2;
	public int banCost = 150;

	[Header("Enlarge (x2/y2 scale)")]
	public int enlargeRank = 4;
	public int enlargeCost = 400;
}