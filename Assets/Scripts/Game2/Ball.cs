using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Doozy.Engine.Utils.ColorModels;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float speed;


        [SerializeField] private List<Vector2> _touchPositions = new List<Vector2>();

        public void Move(List<Vector2> touchPositions)
        {
            _touchPositions = touchPositions;
            transform.DOPath(touchPositions.Select(n => new Vector3(n.x, n.y)).ToArray(), 1f).OnComplete(
                () =>
                {
                    
                }
            );
        }
    }
}