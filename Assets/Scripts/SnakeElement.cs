using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SnakeController;


public class SnakeElement : MonoBehaviour
{

        //public GameObject gameObject { get; set; }     // the attached GameObject

        public DIRECTION direction { get;  set; }    //the direction the SnakeElement is heading

        //private float scale=1f; // the scale of the SnakeElement

        //public SnakeElement(GameObject gameObject, DIRECTION direction)
        //{
        //    this.direction = direction;
        //}

        //public SnakeElement(GameObject gameObject, DIRECTION direction, float initialSize) : this(gameObject,direction)
        //{
        //    GrowHead(initialSize);
        //}



        public void Dispose()
        {
            //print("dispose");
            Destroy(gameObject);
        }

        public float GetLength()
        {
            if (direction == DIRECTION.UP || direction == DIRECTION.DOWN)
                return gameObject.transform.lossyScale.z;
            else
                return gameObject.transform.lossyScale.x;
        }

        public void GrowHead(float magnitude)
        {
            switch (direction)
            {
                case DIRECTION.UP:
                    gameObject.transform.Translate(0f, 0f, magnitude / 2, Space.Self);
                    gameObject.transform.localScale += new Vector3(0f, 0f, magnitude);
                    break;
                case DIRECTION.DOWN:
                    gameObject.transform.Translate(0f, 0f, -magnitude / 2, Space.Self);
                    gameObject.transform.localScale += new Vector3(0f, 0f, magnitude);
                    break;

                case DIRECTION.LEFT:
                    gameObject.transform.Translate(-magnitude / 2, 0f, 0f, Space.Self);
                    gameObject.transform.localScale += new Vector3(magnitude, 0f, 0f);
                    break;
                case DIRECTION.RIGHT:
                    gameObject.transform.Translate(magnitude / 2, 0f, 0f, Space.Self);
                    gameObject.transform.localScale += new Vector3(magnitude, 0f, 0f);
                    break;
                default:
                    break;
            }
        }

        public void ShrinkTail(float magnitude)
        {

            switch (direction)
            {
                case DIRECTION.UP:
                    gameObject.transform.Translate(0f, 0f, magnitude / 2, Space.Self);
                    gameObject.transform.localScale -= new Vector3(0f, 0f, magnitude);
                    break;
                case DIRECTION.DOWN:
                    gameObject.transform.Translate(0f, 0f, -magnitude / 2, Space.Self);
                    gameObject.transform.localScale -= new Vector3(0f, 0f, magnitude);
                    break;

                case DIRECTION.LEFT:
                    gameObject.transform.Translate(-magnitude / 2, 0f, 0f, Space.Self);
                    gameObject.transform.localScale -= new Vector3(magnitude, 0f, 0f);
                    break;
                case DIRECTION.RIGHT:
                    gameObject.transform.Translate(magnitude / 2, 0f, 0f, Space.Self);
                    gameObject.transform.localScale -= new Vector3(magnitude, 0f, 0f);
                    break;
                default:
                    break;
            }
        }

        // Returns the center of box with radius or width of GameObject
        public Vector3 GetHeadBoxCenter()
        {
            // Get coordinates according to direction
            Vector3 posPivot = gameObject.transform.position;   //the pivot point of gameObject (== center)
            Vector3 coordinates;
            switch (direction)
            {
                case DIRECTION.UP:
                    coordinates = new Vector3(posPivot.x, posPivot.y, posPivot.z + gameObject.transform.lossyScale.z / 2 - gameObject.transform.lossyScale.x / 2);
                    break;
                case DIRECTION.DOWN:
                    coordinates = new Vector3(posPivot.x, posPivot.y, posPivot.z - gameObject.transform.lossyScale.z / 2 + gameObject.transform.lossyScale.x / 2);
                    break;
                case DIRECTION.LEFT:
                    coordinates = new Vector3(posPivot.x - gameObject.transform.lossyScale.x / 2 + gameObject.transform.lossyScale.y / 2, posPivot.y, posPivot.z);
                    break;
                case DIRECTION.RIGHT:
                    coordinates = new Vector3(posPivot.x + gameObject.transform.lossyScale.x / 2 - gameObject.transform.lossyScale.y / 2, posPivot.y, posPivot.z);
                    break;
                default:
                    coordinates = new Vector3(0, 0, 0);
                    break;

            }

            return coordinates;
        }

    private void OnCollisionEnter(Collision collision)
    {
        transform.parent.gameObject.GetComponent<SnakeController>().OnCollisionEnter(collision);
    }


}
