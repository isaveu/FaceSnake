using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class CollisionEvent : UnityEvent<Collision> { }


public class SnakeController : Singleton<SnakeController>
{
    public  GameObject boxPrefab;
    public GameObject firstSnakeElement;
    public CollisionEvent collisionEvent;

    private float speed = 3f;

    private int ignoreCollision = 0;

    [HideInInspector]
    public  enum DIRECTION
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };


    private List<SnakeElement> snake = new List<SnakeElement>();

    private void Awake()
    {
        collisionEvent.AddListener(GameManager.Instance.OnSnakeCollision);
    }


    // Start is called before the first frame update
    void Start()
    {
        // Add the first existing element to our snake queue
        //snake.Add(new SnakeElement(firstSnakeElement, DIRECTION.UP));
        SnakeElement snakeElement = firstSnakeElement.GetComponent<SnakeElement>();
        snakeElement.direction = DIRECTION.UP;
        snake.Add(snakeElement);
        ChangeDirection(DIRECTION.RIGHT);
    }

    // Update is called once per frame
    void Update()
    {
        Move(Time.deltaTime * speed);
    }

    public void IncreaseSpeed(float increaseBy)
    {
        speed += increaseBy;
        print(speed);
    }

    public void ChangeDirection(DIRECTION direction)
    {
        // Get last snake element
        SnakeElement elementLast = snake[snake.Count - 1];


        // If snake changes direction, instantiate prefab module
        if(direction != elementLast.direction 
        && ( (int)direction + (int)elementLast.direction ) != 1
        && ((int)direction + (int)elementLast.direction) != 5)

        {


            // Locate tail position where to spawn new prefab instance
            Vector3 spawnPosition = elementLast.GetHeadBoxCenter();
            ignoreCollision = 2;    // We want to ignore two collisions

            // TODO: All this here can be put in a static method of SnakeElement that will instiantiate and set the direction according to parameter

            // Create new GameObject from Prefab
            GameObject newElement = Instantiate(boxPrefab, spawnPosition,boxPrefab.transform.rotation);

            // Set it as child of SnakeController
            newElement.transform.parent = transform;

            // Get SnakeElement Component
            SnakeElement snakeElement = newElement.GetComponent<SnakeElement>();
            snakeElement.direction = direction;

            // Add to our snake list
            snake.Add(snakeElement);
        }
    }


    public void Move(float magnitude)
    {

        // Grow fist snake element
        snake[snake.Count - 1].GrowHead(magnitude);

       
        // Check if tail element is so small it should be destroyed
        if (snake[0].GetLength() <= 1)
        {

            // As length gets only checked once per frame, snakeelement will be already a bit smaller than 1. This difference needs to be subtracted from next element, otherways snake will grow over turns
            float lag = 1 - snake[0].GetLength();
            snake[1].ShrinkTail(lag);

            snake[0].Dispose();
            snake.RemoveAt(0);
        }

        // Shrink last snake element
        snake[0].ShrinkTail(magnitude);


        float length=0f;
        foreach(var elem in snake)
        {
            length += elem.GetLength();
        }
        //print(string.Format("Overall length {0}", length - snake.Count-1));
    }

    public void OnCollisionEnter(Collision collision)
    {

        // We always have to ignore two collisions as we are spawning inside the snake (last element and new element both firing a OnCollisionEnter)
        if (ignoreCollision != 0)
        {
            ignoreCollision--;
        }
        else

        {
            collisionEvent.Invoke(collision);

            //print("COLLISION: " + collision.gameObject.name);
        }
    }

}
