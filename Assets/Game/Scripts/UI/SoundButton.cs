using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    
    [SerializeField] private SoundController _soundController;
    [SerializeField] private List<Sprite> _sprites;
    private Image _image;
    private bool isMute;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = _sprites[0];
    }

    public void ChangeMusicState()
    {
        isMute = !isMute;
        if (!isMute)
        {
            _soundController.MusicMute(isMute);
            _image.sprite = _sprites[0];
        }
        else
        {
            _soundController.MusicMute(isMute);
            _image.sprite = _sprites[1];
        }
    }
    
    public void ChangeSoundState()
    {
        isMute = !isMute;
        if (!isMute)
        {
            _soundController.SoundMute(isMute);
            _image.sprite = _sprites[0];
        }
        else
        {
            _soundController.SoundMute(isMute);
            _image.sprite = _sprites[1];
        }
    }
}
