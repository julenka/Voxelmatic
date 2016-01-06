using UnityEngine;
using System.Collections;

/// <summary>
/// Rotates an object around the origin, ensuring that it always faces 0,0,0.
/// Starts rotation at theta of 0
/// object rotates around the XZ plane.
/// </summary>
public class RotateAroundOriginBehavior : MonoBehaviour {
    public float DegreesPerSecond = 0.01f;

    public bool UseKeys = true;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        float multiplier = 0;
        Vector3 direction = Vector3.up;
        if(UseKeys)
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                multiplier = 1;
            } else if (Input.GetKey(KeyCode.RightArrow))
            {
                multiplier = -1;
            } else if (Input.GetKey(KeyCode.UpArrow))
            {
                multiplier = 1;
                direction = Vector3.left;
            } else if (Input.GetKey(KeyCode.DownArrow))
            {
                multiplier = -1;
                direction = Vector3.left;
            }
        } else
        {
            multiplier = 1;
        }
        transform.RotateAround(Vector3.zero, direction, multiplier * DegreesPerSecond * Time.deltaTime);
    }
}