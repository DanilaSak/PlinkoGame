using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Game1
{
    public class Node : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private GameObject selectLabel;
        [SerializeField] private Ball _ball;
        [SerializeField] private Sprite Gold;
        [SerializeField] private Sprite Purple;
        [SerializeField] private Sprite Red;
        [SerializeField] private Sprite Green;
        [SerializeField] private Sprite Gray;
        [SerializeField] private Sprite Blue;
        [SerializeField] private Sprite DarkBlue;
        [SerializeField] private Sprite LightGreen;
        [SerializeField] private Sprite Pink;
        [SerializeField] private Sprite Orange;

        public event Action<Node> Click; 
        
        public void Randomize()
        {
            _ball = (Ball)Random.Range(1,11);
            _renderer.sprite = _ball switch
            {
                Ball.Gold => Gold,
                Ball.Purple => Purple,
                Ball.Red => Red,
                Ball.Green => Green,
                Ball.Gray => Gray,
                Ball.Blue => Blue,
                Ball.DarkBlue => DarkBlue,
                Ball.LightGreen => LightGreen,
                Ball.Pink => Pink,
                Ball.Orange => Orange,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(name);
            Click?.Invoke(this);
        }

        public void Select()
        {
            selectLabel.SetActive(true);
        }

        public void Diselect()
        {
            selectLabel.SetActive(false);

        }
    }
}