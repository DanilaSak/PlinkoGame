using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchiveCell : MonoBehaviour
{
    [SerializeField] private Image _imageCell;
    [SerializeField] private TextMeshProUGUI _textCell;
    [SerializeField] private Animator _anim;
    private int _currentValue;
    private GemType _type;
    private bool _isHide;

    public void ConfigCell(Sprite sprite, GemType type)
    {
        _isHide = false;
        _type = type;
        _currentValue = 0;
        _imageCell.sprite = sprite;
        _textCell.text = $"{_currentValue}/10";
        _anim.SetTrigger("Open");
    }
    
    public void UpdateCell(int value)
    {
        if (!_isHide)
        {
            _anim.SetTrigger("AddValue");
            _textCell.text = $"{value}/10";   
        }
    }
    
    public void CloseCell()
    {
        _anim.SetTrigger("Close");
    }

    public void CheckAchive(GemType type)
    {
        if (_type == type)
        {
            if (_currentValue < 10)
            {
                _currentValue++;
                UpdateCell(_currentValue);
            }
            else if(_currentValue >= 10)
            {
                _isHide = true;
                _anim.SetTrigger("Close");
            }
        }
    }
}
