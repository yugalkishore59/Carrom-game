using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
   [SerializeField] TMP_Text cpuScore;
   [SerializeField] TMP_Text playerScore;
   [SerializeField] TMP_Text UITimer;
   [SerializeField] float timer = 121;
   public int movingPucks = 0;
   public int turn = 1; // 1 player, -1 cpu, 0 none
   [SerializeField] GameObject striker;
   StrikerScript strikerScript;
   public int pucksCount = 10;
   public bool strikerThrown = false;
   public bool isPotted = false;
   [SerializeField] GameObject gameOver;

   private void Awake() {
     Time.timeScale = 1;
     strikerScript = striker.GetComponent<StrikerScript>();
   }

     private void FixedUpdate() {
          UITimer.text = ((int)timer).ToString();
          timer -= Time.deltaTime;
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

     if(timer <= 0 || pucksCount <= 0){
          GameOver();
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

   void GameOver(){
     Time.timeScale = 0;
     UITimer.text = "0";
     timer = 0;
     gameOver.SetActive(true);
   }

   public void PlayAgain(){
     SceneManager.LoadScene("Game");
   }
}
