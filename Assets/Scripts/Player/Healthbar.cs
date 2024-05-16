using System;
using UnityEngine;
using UnityEngine.Assertions;
using Vector2 = UnityEngine.Vector2;

public class Healthbar : MonoBehaviour
{
    [Serializable]
    public class Gradient
    {
        public Color _color1;
        public Color _color2;
    }
    [SerializeField] private float _width;
    [SerializeField] private Gradient _gradient;
    private float _maxWidth;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _maxWidth = _width;
    }

    public void SetVisible(bool isVisible)
    {
        _spriteRenderer.enabled = isVisible;
    }

    public void SetValue(float value)
    {
        value = Math.Clamp(value, 0f, 1f);
        _width = _maxWidth * value;
        _spriteRenderer.size = new Vector2(_width, _spriteRenderer.size.y);
        _spriteRenderer.color = Color.Lerp(_gradient._color2, _gradient._color1, _width / _maxWidth);
    }

    public void Reset()
    {
        SetValue(_maxWidth);
    }
}