using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Accessibility;

public class InputManager : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        CheckKeysAndMoveSnake();

    }

    private void CheckKeysAndMoveSnake()
    {
        SnakeController snakeController;
        try
        {
            snakeController = GameObject.Find("Snake").GetComponent<SnakeController>();
        }
        catch (System.NullReferenceException e)
        { return; }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            snakeController.ChangeDirection(SnakeController.DIRECTION.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            snakeController.ChangeDirection(SnakeController.DIRECTION.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            snakeController.ChangeDirection(SnakeController.DIRECTION.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            snakeController.ChangeDirection(SnakeController.DIRECTION.DOWN);
        }
    }


    public void OnReceiveFaceStateChange(milook.MilookEngine.FaceEX faceEX)
    {
        SnakeController snakeController;
        try
        {
            snakeController = GameObject.Find("Snake").GetComponent<SnakeController>();
        }
        catch (System.NullReferenceException e)
        { return; }

        //print("[InputManager]");
        switch (faceEX)
        {
            case milook.MilookEngine.FaceEX.EX_M_OPEN_SMILE:
                print("[InputManager] Open Smile > LEFT");
                snakeController.ChangeDirection(SnakeController.DIRECTION.LEFT);
                break;

            case milook.MilookEngine.FaceEX.EX_B_RAISE_R:
            case milook.MilookEngine.FaceEX.EX_B_RAISE_L:
                print("[InputManager] Brow Raise > UP");
                snakeController.ChangeDirection(SnakeController.DIRECTION.UP);
                break;

            case milook.MilookEngine.FaceEX.EX_M_AH:
            case milook.MilookEngine.FaceEX.EX_M_EH:
            case milook.MilookEngine.FaceEX.EX_M_OH:
                print("[InputManager] Mouth OhEhAh > RIGHT");
                snakeController.ChangeDirection(SnakeController.DIRECTION.RIGHT);
                break;

            case milook.MilookEngine.FaceEX.EX_E_CLOSE_L:
            case milook.MilookEngine.FaceEX.EX_E_CLOSE_R:
                print("[InputManager] Eye close > DOWN");
                snakeController.ChangeDirection(SnakeController.DIRECTION.DOWN);
                break;
        }

    }

}
