using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
   [SerializeField] TMP_Text cpuScore;
   [SerializeField] TMP_Text playerScore;
   public int movingPucks = 0;
   public int turn = 0; // 1 player, -1 cpu, 0 none

   public void UpdatePlayerScore(int score){
        int old = int.Parse(playerScore.text);
        old+=score;
        playerScore.text = old.ToString();
   }

    public void UpdateCPUScore(int score){
        int old = int.Parse(cpuScore.text);
        old+=score;
        cpuScore.text = old.ToString();
   }
}
