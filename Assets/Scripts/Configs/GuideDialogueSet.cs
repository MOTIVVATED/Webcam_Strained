using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GuideDialogueSet", menuName = "Guide/Dialogue Set")]
public class GuideDialogueSet : ScriptableObject
{
	public List<string> lines;
}
