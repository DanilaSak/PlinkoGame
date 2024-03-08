using System;
using System.Collections.Generic;
using Doozy.Engine;
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
            indexText.text = $"{index+1}/{_items.Count}";
        }
    }
}
/*
1. In free fall, a body moves at a constant velocity. (No)
   2. Vacuum is the best medium to study free fall. (Yes)
   (3) A body always falls vertically downward. (Yes)
   4. All objects on Earth fall with the same acceleration. (No)
   5. The acceleration of free fall on Earth is approximately 9.8 m/s². (Yes)
   6. The acceleration of free fall depends on the mass of the falling body. (No)
   7. The force of friction affects free fall. (No)
   8. The velocity of a falling body increases in proportion to the time of fall. (No)
   9. In free fall, an open parachute causes an increase in drag force. (Yes)
   10. The fall of a body on the moon occurs at a speed that is less than on Earth. (Yes)
   11. During free fall, the vertical velocity of an object increases. (Yes)
   12. The acceleration of free fall is always the same on different planets. (No)
   13. A body falling on Earth falls at a speed that increases by 9.8 m/s every second. (Yes)
   14. The density of a falling body affects its free-fall velocity. (No)
   15. The acceleration of free fall depends on the height of the fall. (No)
   16. The size and shape of a falling body affect its free-fall acceleration. (No)
   17. The time for a body to fall increases with increasing height. (No)
   18. The free fall acceleration of a falling object is equal to the gravitational acceleration. (Yes)
   19.The trajectory of free fall is always a straight line. (Yes)
   20. Energy is conserved during free fall. (Yes)

*/