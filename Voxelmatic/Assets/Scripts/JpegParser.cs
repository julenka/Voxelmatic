using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Parses an input image (Jpeg, PNG) into a Texture2D
/// </summary>
class JpegParser
{
    private Texture2D m_imageTexture;

    public static Texture2D GetTexture2D(String path)
    {
        
        byte[] bytes = File.ReadAllBytes(path);
        int[] imageDimensions = GetJpegDimensions(bytes);
        Texture2D result = new Texture2D(imageDimensions[0], imageDimensions[1]);
        result.LoadImage(bytes);
        return result;
    }

    private static int getShort(byte[] p, int i)
    {
        int p0 = p[i] & 0xFF;
        int p1 = p[i + 1] & 0xFF;
        return p1 | (p0 << 8);
    }

    /// <summary>
    /// http://stackoverflow.com/questions/18264357/how-to-get-the-width-height-of-jpeg-file-without-using-library
    /// </summary>
    /// <param name="b"></param>
    /// <returns>{width, height}</returns>
    private static int[] GetJpegDimensions(byte[] b)
    {
        int nIndex;
        int height = 0, width = 0, size = 0;
        int nSize = b.Length;

        // marker FF D8  starts a valid JPEG
        if (getShort(b, 0) == 0xFFD8)
            for (nIndex = 2; nIndex < nSize - 1; nIndex += 4)
                if (b[nIndex] == 0xFF/*FF*/ && b[nIndex + 1] == 0xC0 /*C0*/)
                {
                    int w = getShort(b, nIndex + 7);
                    int h = getShort(b, nIndex + 5);
                    if (w * h > size)
                    {
                        size = w * h;
                        width = w;
                        height = h;
                    }
                }
        return new int[] { width, height };
    }
}
