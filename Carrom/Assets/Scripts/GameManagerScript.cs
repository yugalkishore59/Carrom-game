using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
   [SerializeField] TMP_Text cpuScore;
   [SerializeField] TMP_Text playerScore;
   [SerializeField] TMP_Text winner;
   [SerializeField] TMP_Text UITimer;
   [SerializeField] float timer = 121;

   public int movingPucks = 0;
   public int turn = 1; // 1 player, -1 cpu, 0 none
   public int blackPucksCount = 9;
   public int whitePucksCount = 9;
   public int queenPucksCount = 1;
   public bool strikerThrown = false; //has striker took any shot
   public bool isPotted = false; //did any correct puck potted (for giving another turn)

   [SerializeField] GameObject gameOver;
   [SerializeField] GameObject striker;
   StrikerScript strikerScript;

   private void Awake() {
     Time.timeScale = 1; //resume the game
     strikerScript = striker.GetComponent<StrikerScript>();
   }

     private void FixedUpdate() {
          //timer
          UITimer.text = ((int)timer).ToString();
          timer -= Time.deltaTime;
     }

   private void Update() {

     if(strikerThrown && movingPucks == 0){ //if all pucks stopped after a shot
          strikerThrown = false;
          if(!isPotted){
               if(turn == 1)
                    turn = -1;
               else
                    turn = 1;
          }else{ //another turn ie. no changes
               isPotted = false;
          }
          

          strikerScript.ChangeSide(); //changing side ie. resetting the striker position according to "turn"
     }

     if(timer <= 0 || (queenPucksCount <= 0 && (blackPucksCount <= 0 || whitePucksCount <=0 ))){
          GameOver();
     }
   }

   public void UpdatePlayerScore(int score){ //increasing player score
        int old = int.Parse(playerScore.text);
        old+=score;
        playerScore.text = old.ToString();
   }

   public void UpdateCPUScore(int score){ //increasing cpu/player 2 score
        int old = int.Parse(cpuScore.text);
        old+=score;
        cpuScore.text = old.ToString();
   }

   void GameOver(){
     Time.timeScale = 0; //pausing the game
     UITimer.text = "0";
     timer = 0;
     int playerScoreCount = int.Parse(playerScore.text);
     int cpuScoreCount = int.Parse(cpuScore.text);

     //showing winner name according to mode(1 player or 2players) and score
     if(SceneManager.GetActiveScene().name == "Game"){
          if(playerScoreCount>cpuScoreCount){
               winner.text = "Player";
          }else if(cpuScoreCount>playerScoreCount){
               winner.text = "CPU";
          }else{
               winner.text = "No one";
          }
     }else{
          if(playerScoreCount>cpuScoreCount){
               winner.text = "Player 1";
          }else if(cpuScoreCount>playerScoreCount){
               winner.text = "Player 2";
          }else{
               winner.text = "No one";
          }
     }
     
     gameOver.SetActive(true);
   }

   public void PlayAgain(){ //restarting the game
     SceneManager.LoadScene("Main");
   }
}
