using TMPro;
using UnityEngine;
using System.Collections;

public class TiltView : MonoBehaviour
{
  [SerializeField] private TMP_Text _tiltText;

  private void Start()
  {
    StartCoroutine(Init());
  }
  private IEnumerator Init()
  {
    while (TiltManager.Instance == null)
    yield return null;

    TiltManager.Instance.OnTiltIncreased += UpdateTilt;
    TiltManager.Instance.OnTiltDecreased += UpdateTilt;
    UpdateTilt(TiltManager.Instance.Tilt);
  }
  private void OnDestroy()
  {
    if (TiltManager.Instance != null)
        
    TiltManager.Instance.OnTiltIncreased -= UpdateTilt;
    TiltManager.Instance.OnTiltDecreased -= UpdateTilt;
  }
  private void UpdateTilt(int newTilt)
  {
    _tiltText.text = newTilt.ToString() + "%";
  }
}
