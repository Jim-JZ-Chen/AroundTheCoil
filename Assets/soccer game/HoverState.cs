using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverState : MonoBehaviour
{

    public float maxFloatHeight = 1.7f;// added to starting transform
    public float minFloatHeight = 1; // removed from starting transform
    public float yRotSpeed = 0.6f;
    public float floatSpeed = 0.4f;
    public bool xRotenabled = true;

    private float baseTransform;
    private bool goingUP = true;
    


    void Start()
    {
        baseTransform = transform.position.y+Random.value * maxFloatHeight;
    }




    // Update is called once per frame
    void Update()
    {
        float yVal = goingUP ? baseTransform + maxFloatHeight : baseTransform - minFloatHeight;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, yVal, transform.position.z), floatSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.position.y - yVal) < 1f)
        {
            goingUP = goingUP ? false : true;
        }
        if (xRotenabled)
        {
            transform.Rotate(0, yRotSpeed, 0);
        }
    }
}
