using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private void Start()
    {
    
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnSnack();
    }

    private void SpawnSnack()
    {
        LootManager.Instance.SpawnSnack();
    }

    public void OnSnakeCollision(Collision collision)
    {
        print("[GameManager] SnakeCollision with" + collision.gameObject.name);

        if(collision.gameObject.tag == "Snack")
        {
            Destroy(collision.gameObject);
            SnakeController.Instance.IncreaseSpeed(10);
            return;
        }

        AsyncOperation ao = SceneManager.UnloadSceneAsync("Snake");
        ao.completed += x => SceneManager.LoadSceneAsync("Snake",LoadSceneMode.Additive); 
        

    }


    // As milook engine does not have a proper initializer, we want to check for the first event
    public void OnTrack(milook.MilookEngine.TrackerData tdata)
    {
        print("[GameManager] OnTrack()");
        SceneManager.LoadSceneAsync("Snake",LoadSceneMode.Additive);
        // Remove this gameobject from listener list, so we do not get anymore events
        GameObject.Find("MiloTracker").GetComponent<milook.MilookEngine>().listenerList.Remove(gameObject);
    }
}
