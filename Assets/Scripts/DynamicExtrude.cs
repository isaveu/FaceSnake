using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Events;

public class DynamicExtrude : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()

    {

        //StartCoroutine(ScaleAndTranslate());
       

    }

    // Update is called once per frame
    void Update()
    {




    }






    public IEnumerator ScaleAndTranslate()
    {

        float seconds = 10;
        int speed = 10;
        Transform cubeTransform = GameObject.Find("Cube").transform;


        for (float elapsedTime = 0f; elapsedTime < 2f; elapsedTime += Time.deltaTime)

        {
            cubeTransform.Translate(0f, 0f, -speed * Time.deltaTime / 2);
            cubeTransform.localScale += new Vector3(0f, 0f, -speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }


        for (float elapsedTime = 0f; elapsedTime < 2f; elapsedTime += Time.deltaTime)
        {
            cubeTransform.Translate(-speed * Time.deltaTime / 2, 0f, 0f);
            cubeTransform.localScale += new Vector3(-speed * Time.deltaTime, 0f, 0f);
            yield return new WaitForEndOfFrame();
        }
    }

    public delegate void Test(int x);

    Test test;

   


    private void OnCollisionEnter(Collision collision)
    {
    
    }



}


