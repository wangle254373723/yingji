using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Threading;
/*
 已用截图并保存类
     */
public class CuttingScreen : Sington<CuttingScreen> {

    public int index = 0;
    public string mPath;
    Dictionary<string, byte[]> mBytes = new Dictionary<string, byte[]>();
    Dictionary<string, byte[]> mBytesClone;
    //string mMergePath= Application.dataPath + "/merge.cmd";
    string mImagepath;
    public void CuttToJPG()
    {
        //InvokeRepeating("WriteBytes", 0, 0.1f);
        StartCoroutine(StartCuttingScreen());
    }

    /// <summary>
    /// 截图存放路径初始化
    /// </summary>
    public override void Init()
    {
        base.Init();
        mPath = Application.dataPath + "/screen_{0}/";
    }

    IEnumerator StartCuttingScreen()
    {
        yield return new WaitForEndOfFrame();
        CuttScreen();
        yield return null;
    }


    /// <summary>
    /// 核心截图功能
    /// </summary>
    void  CuttScreen()
    {
        index++;

        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        //Texture2D new_tex = FacebookScript.ScalePic(tex, 0.8f);

        byte[] screenshot = tex.EncodeToJPG();
        tex.Compress(true);//对屏幕缓存进行压缩
        tex.Apply();
        if (!Directory.Exists(mPath)){
            Directory.CreateDirectory(mPath);
        }
        mImagepath = mPath + index + ".jpg";
        //mBytes.Add(mImagepath, screenshot);
       // MemoryStream
        File.WriteAllBytes(mImagepath, screenshot);
    }
    

    string SubStringPath(string path)
    {
        int startIndex = path.LastIndexOf("%");
        path = path.Substring(0, startIndex + 1);
        return path;
    }
    string SubStringLenght(string path)
    {
        int startIndex = path.LastIndexOf("%");
        path = path.Substring(startIndex+1);
        return path;
    }
}
