using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeScript : MonoBehaviour
{
    public void Mode1Player(){
        SceneManager.LoadScene("Game");
    }
    public void mode2Players(){
        SceneManager.LoadScene("2 Players");
    }
}
