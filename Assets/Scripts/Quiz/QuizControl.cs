using System;
using System.Collections.Generic;
using Doozy.Engine;
using Mono.Cecil;
using TMPro;
using UnityEngine;

namespace Quiz
{
    public class QuizControl : MonoBehaviour
    {
        [SerializeField] private List<QuizItem> _items = new List<QuizItem>();

        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI indexText;

        [SerializeField] private int index;

        public void SetAnswer(bool result)
        {
            if (result == _items[index].answer)
            {
                index++;
                if (index >= _items.Count)
                {
                    Debug.Log("Win");

                    GameEventMessage.SendEvent("Win");
                    //WIN
                }
                else
                {
                    Show();
                    
                }
            }
            else
            {
                Debug.Log("Lose");
                GameEventMessage.SendEvent("Lose");
                //LOSE
            }
        }

        private void OnEnable()
        {
            index = 0;
            Show();
            
        }

        private void Show()
        {
            text.text = _items[index].text;
            indexText
        }
    }
}