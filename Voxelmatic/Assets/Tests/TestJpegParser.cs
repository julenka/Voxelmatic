using UnityEngine;
using System.Collections;

public class TestJpegParser : MonoBehaviour {

    // Use this for initialization
    void Start () {
        // Application.StreamingAssetsPath
        Texture2D texture = JpegParser.GetTexture2D(Application.streamingAssetsPath + @"\David-50.jpg");
        GameObject quad = GameObject.Find("TestsQuad");
        quad.GetComponent<Renderer>().material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update () {
    
    }
}
