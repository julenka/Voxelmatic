using UnityEngine;
using System.Collections;

public class RectangularVoxelizerBehavior : MonoBehaviour {
    public GameObject VoxelPrototype;
    public string JpegNameInStreamingAssetFolder = "David-50.jpg";

    public float Width;
    public float Height;
    public float Depth;
    
    private struct Bounds
    {
        public float left, right, top, bottom, front, back;
    }

    private GameObject[] m_voxels;

    // Use this for initialization
    void Start () {
        Texture2D texture = JpegParser.GetTexture2D(Application.streamingAssetsPath + @"\" + JpegNameInStreamingAssetFolder) ;

        for(int y = 0; y < texture.height; y++)
        {
            for(int x = 0; x < texture.width; x++)
            {
                Color pixelColor = texture.GetPixel(x, y);
                if (Random.Range(0, 1) > pixelColor.grayscale )
                {
                    
                    continue;
                }
                GameObject voxel = MakeVoxel(x, y, texture.width, texture.height, pixelColor);
                voxel.transform.parent = transform;
            }
        }
    }

    private GameObject MakeVoxel(int pixelX, int pixelY, int imageWidth, int imageHeight, Color color)
    {
        GameObject result = Instantiate(VoxelPrototype);
        result.GetComponent<Renderer>().material.color = color;
        float pctX = pixelX / (float)imageWidth;
        float pctY = pixelY / (float)imageHeight;
        Bounds bounds = GetBounds();
        result.transform.localPosition = new Vector3(
            pctX * Width + bounds.left, 
            pctY * Height + bounds.bottom, 
            Random.Range(bounds.back, bounds.front));
        return result;
    }
    
    private Bounds GetBounds()
    {
        Bounds result = new Bounds();
        result.left = -Width / 2;
        result.right = Width / 2;
        result.top = Height / 2;
        result.bottom = -Height / 2;
        result.front = Depth / 2;
        result.back = -Depth / 2;
        return result;
    }

    void OnDrawGizmos()
    {
        Bounds bounds = GetBounds();
        
        // 1-2
        // | |
        // 0-3
        Vector3 p0 = new Vector3(bounds.left,  bounds.bottom, bounds.front);
        Vector3 p1 = new Vector3(bounds.left,  bounds.top,    bounds.front);
        Vector3 p2 = new Vector3(bounds.right, bounds.top,    bounds.front);
        Vector3 p3 = new Vector3(bounds.right, bounds.bottom, bounds.front);
        Vector3 p4 = new Vector3(bounds.left,  bounds.bottom, bounds.back);
        Vector3 p5 = new Vector3(bounds.left,  bounds.top,    bounds.back);
        Vector3 p6 = new Vector3(bounds.right, bounds.top,    bounds.back);
        Vector3 p7 = new Vector3(bounds.right, bounds.bottom, bounds.back);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(p4, p5);
        Gizmos.DrawLine(p5, p6);
        Gizmos.DrawLine(p6, p7);
        Gizmos.DrawLine(p7, p4);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(p0, p4);
        Gizmos.DrawLine(p1, p5);
        Gizmos.DrawLine(p2, p6);
        Gizmos.DrawLine(p3, p7);
    }

    // Update is called once per frame
    void Update () {
    
    }
}
