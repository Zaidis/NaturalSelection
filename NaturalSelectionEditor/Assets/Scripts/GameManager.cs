using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
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
        Debug.Log("GAME OVER");
    }

}
