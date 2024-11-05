    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool _gameOver = false;
    private int killCount = 0;
    [SerializeField] Text killCountDisplay;

    public void AddKill()
    {
        killCount++;
        UpdateKillCountDisplay();
    }

    void UpdateKillCountDisplay()
    {
        if (killCountDisplay != null)
        {
            killCountDisplay.text = "Kills: " + killCount.ToString();
        }
    }
    
}
