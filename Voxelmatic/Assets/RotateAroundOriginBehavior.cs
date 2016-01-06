using UnityEngine;
using System.Collections;

/// <summary>
/// Rotates an object around the origin, ensuring that it always faces 0,0,0.
/// Starts rotation at theta of 0
/// object rotates around the XZ plane.
/// </summary>
public class RotateAroundOriginBehavior : MonoBehaviour {
    public float DegreesPerSecond = 0.01f;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        transform.RotateAround(Vector3.zero, Vector3.up, DegreesPerSecond * Time.deltaTime);
    }
}