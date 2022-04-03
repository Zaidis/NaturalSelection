using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    bool starting;
    private void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else {
            Destroy(this);
        }
        
    }

    public Terrain terrain;


    public void CheckVictory(int bunniesKilled) {
        if (bunniesKilled >= 1000) {
            Victory();
        }
    }

    void Victory() {
        Debug.Log("VICTORY");
    }

    public void CheckDefeat() {
        int pop = BunnyManager.instance.males + BunnyManager.instance.females;

        if (pop >= 1000) {
            GameOver();
        }
    }

    void GameOver() {
        if (!starting)
        {
            starting = true;
            SceneManager.LoadScene("LoseScene");
        }
        //Debug.Log("GAME OVER");
    }

}
