using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private float maxDragDistance = 1;

    public Vector2 input;
    
    private RectTransform _rectTransform;
    private Vector2 _joystickDefaultPosition;
    private Vector2 _mousePosition;
    private Vector2 _mouseStartPosition;
    private Vector2 _mouseDelta;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _joystickDefaultPosition = _rectTransform.anchoredPosition;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _mouseStartPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _mouseDelta = Vector2.ClampMagnitude(_mousePosition - _mouseStartPosition, maxDragDistance);
            _rectTransform.anchoredPosition = _joystickDefaultPosition + _mouseDelta;
        }
        else
        {
            _rectTransform.anchoredPosition = _joystickDefaultPosition;
        }
        input = _rectTransform.anchoredPosition / maxDragDistance;
    }
}
