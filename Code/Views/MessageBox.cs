using System.Collections;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _hideTime;

    private Coroutine _hiding;

    private void OnDisable()
    {
        if (_hiding != null)
        {
            StopCoroutine(_hiding);
            _hiding = null;
        }
    }

    public void Show(string message)
    {
        _text.text = message;

        if (_hiding != null)
        {
            StopCoroutine(_hiding);
            _hiding = null;
        }

        _hiding = StartCoroutine(Hiding());
    }

    private IEnumerator Hiding()
    {
        _canvasGroup.alpha = 1.0f;
        float currentTime = 0;

        while (enabled || currentTime < _hideTime)
        {
            currentTime += Time.deltaTime;
            float normalizeTime = currentTime / _hideTime;
            float alpha = Mathf.Lerp(1, 0, normalizeTime);
            _canvasGroup.alpha = alpha;

            yield return null;
        }

        _hiding = null;
    }
}
