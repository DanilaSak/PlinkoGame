using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    [SerializeField] private List<AchiveCell> _achiveCells;
    [SerializeField] private List<Sprite> _achiveSprite;
    [SerializeField] private List<GemType> _gemTypes;

    public void ConfigurateAchive()
    {
        if (_achiveSprite.Count < 2 || _achiveCells.Count < 2)
        {
            Debug.LogError("Недостаточно изображений или слотов!");
            return;
        }

        ShuffleImages(); 
        SetRandomImages();
    }
    
    void ShuffleImages()
    {
        for (int i = _achiveSprite.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Sprite temp = _achiveSprite[i];
            GemType tempType = _gemTypes[i];
            _gemTypes[i] = _gemTypes[randomIndex];
            _gemTypes[randomIndex] = tempType;
            _achiveSprite[i] = _achiveSprite[randomIndex];
            _achiveSprite[randomIndex] = temp;
        }
    }

    void SetRandomImages()
    {
        for (int i = 0; i < 2; i++)
        {
            _achiveCells[i].ConfigCell(_achiveSprite[i],_gemTypes[i]);
        }
    }

    public void CheckAchive(GemType type)
    {
        foreach (var achiveCell in _achiveCells)
        {
            achiveCell.CheckAchive(type);
        }
    }
    
    public void HideCells()
    {
        foreach (var achiveCell in _achiveCells)
        {
            achiveCell.CloseCell();
        }
    }
}
