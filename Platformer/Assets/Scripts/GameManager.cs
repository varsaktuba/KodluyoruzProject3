using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    private int _currentGold = 0;
    public TextMeshProUGUI goldText;

 

        public void AddGold(int goldToAdd)
    {
        _currentGold += goldToAdd;
        goldText.text = "Gold:" + _currentGold;

    }
     
    }
