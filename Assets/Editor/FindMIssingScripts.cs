using UnityEngine;
using UnityEditor;
using Codice.CM.Common;

public class FindMissingScripts :EditorWindow
{
  [MenuItem("Tools/Find Missing Scripts")]
  public static void Run()
  {
    int found = 0;
    foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
    {
      foreach (Component c in go.GetComponents<Component>())
      {
        if (c == null)
        {
          Debug.LogWarning($"Missing script on: {GetFullPath(go)}", go);
          found++;
        }
      }
    }
    Debug.Log($"Search complete. {found} missing script(s) found.");
  }

  private static string GetFullPath(GameObject go)
  {
    string path = go.name;
    Transform t = go.transform.parent;
    while (t != null)
    {
      path = t.name + "/" + path;
      t = t.parent;
    }
    return path;
  }
}
