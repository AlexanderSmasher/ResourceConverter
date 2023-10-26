using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIConverterButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private UnityEvent _onClampAction;

    private bool _isClamped = false; // Button is clamped
    private float _timer = 0f; // Timer (limit = 0.5 sec)

    // EVENT when button pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        _isClamped = true;
        StartCoroutine(ClampingButton());
    }

    // EVENT when button released
    public void OnPointerUp(PointerEventData eventData) => _isClamped = false;

    // EVENT when the cursor leaves the button border
    public void OnPointerExit(PointerEventData eventData) => _isClamped = false;

    // COROUTINE onClampAction every 0.5 sec
    public IEnumerator ClampingButton()
    {
        while (_isClamped)
        {
            _timer = 0f;
            _onClampAction?.Invoke();
            while (_timer <= 0.5f)
            {
                if (!_isClamped)
                    yield break;
                yield return new WaitForSeconds(Time.deltaTime);
                _timer += Time.deltaTime;
            }
        }
        yield break;
    }
}