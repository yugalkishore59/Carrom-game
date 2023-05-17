using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
   [SerializeField] TMP_Text cpuScore;
   [SerializeField] TMP_Text playerScore;
   public int movingPucks = 0;
   public int turn = 1; // 1 player, -1 cpu, 0 none
   [SerializeField] GameObject striker;
   StrikerScript strikerScript;
   public int pucksCount = 10;
   public bool strikerThrown = false;
   public bool isPotted = false;

   private void Awake() {
     strikerScript = striker.GetComponent<StrikerScript>();
   }

   private void Update() {

     if(strikerThrown && movingPucks == 0){
          strikerThrown = false;
          if(!isPotted){
               if(turn == 1)
                    turn = -1;
               else
                    turn = 1;
          }else{
               isPotted = false;
          }
          

          strikerScript.ChangeSide();
     }
   }

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
