using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Info
{
    public class InfoObjectChanger: MonoBehaviour
    {
        [SerializeField] private List<InfoObject> _infoObjects = new List<InfoObject>();
        [SerializeField] private int index;

        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] private Image image;

        private void Start()
        {
            index = 0;
            Show();
        }

        private void Show()
        {
            var t = _infoObjects[index];
            _textMeshProUGUI.text = t.text;
            image.sprite = t.image;
        }

        public void Left()
        {
            index--;
            if (index<0)
            {
                index = _infoObjects.Count - 1;
            }
            Show();
        }

        public void Right()
        {
            index++;
            if (index>= _infoObjects.Count)
            {
                index = 0;
            }
            Show();
        }
    }
}