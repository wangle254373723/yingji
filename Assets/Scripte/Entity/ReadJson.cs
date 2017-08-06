using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LitJson;
using System.Threading;
using VRCapture;
/*
解析XML及下载数据
     */
public class ReadJson : Sington<ReadJson>
{
    public AudioSource mAudio;
    public Transform Pictrue0;
    public Transform Pictrue1;
    public Transform Pictrue2;
    public Transform Pictrue3;
    public Transform Pictrue4;
    public Transform Pictrue5;
    public Transform Pictrue6;
    public UILabel mDownLoadPress;
    public GameObject mPicLabel;
    public GameObject mParentLabel;
    List<Texture> mPicList = new List<Texture>();
    public string unityPath;
    WWW www;
    AudioClip m_downloadClip;
    //string mMusicPath = @"D:\unity4.6Project\BuyingAndSelling\bgmusic\{0}.mp3";
    //string mOldImagePath= @"D:\unity4.6Project\BuyingAndSelling\Assets\Resources\DownLoadImage\{0}.jpg";
    //string mImagePath = @"D:\unity4.6Project\BuyingAndSelling\Assets\Resources\DownLoadImage\{0}.jpg";
    string mVedioPath;
    public string mMusicPath;
    //FileInfo file;
    Mutex tex = new Mutex();
    public Album mAlbum = null;
    public Queue<Album> mAlbunQue = new Queue<Album>();
    public Album a = new Album();

    int mProgressIndex = 0;  //进度
    int mPicindex = -1;     //图片计数器


    /// <summary>
    /// unity执行调用,主要用来舒适化路径,及解析
    /// </summary>
    void Awake()
    {
        Application.runInBackground = true;
        Application.targetFrameRate = 30;
        unityPath = Application.dataPath;
        Directory.SetCurrentDirectory(Directory.GetParent(unityPath).FullName);
        unityPath = Directory.GetCurrentDirectory();
        this.GetComponent<AnimWHC>().enabled = false;
        ReadJsonDB();
        mMusicPath = unityPath + "/video/";
        mVedioPath = unityPath + "/video/";
        CuttingScreen.instance.mPath = unityPath+ "/screen_{0}/";
        CuttingScreen.instance.mPath = string.Format(CuttingScreen.instance.mPath, a.albumId);
        CredPath(mVedioPath);
        //mMusicPath = string.Format(mMusicPath, a.albumId);
        InvokeRepeating("StartDownload", 0, 2);
    }


    public void Write(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        string toolDes = @"echo %cd% movieMaker - f image2 - i screen_{0}\%%d.jpg - i bgmusic\{1}.mp3 - ss 00:00:00 - to {2} - vcodec libx264 - r 30 - vf vflip video\{3}.mp4 - ypause";
        toolDes = string.Format(toolDes, a.albumId, a.template, "00:00:00", a.albumId);
        sw.Write(@"echo %cd% movieMaker - f image2 - i screen\%% d.jpg - i bgmusic\album_34.mp3 - ss 00:00:00 - to 00:00:16 - vcodec libx264 - r 30 - vf vflip video\album_34.mp4 - ypause");
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();
    }


    /// <summary>
    /// 判断是否有路径,否则创建
    /// </summary>
    /// <param name="path"></param>
    void CredPath(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// 解析XML
    /// </summary>
    void ReadJsonDB()
    {
        TextAsset ts = Resources.Load<TextAsset>("album_43");
        JsonData jd = JsonMapper.ToObject(ts.ToString());
        a.authToken = (string)jd["authToken"];
        a.webServerNoticeUrl = (string)jd["webServerNoticeUrl"];
        a.serverId = (int)jd["serverId"];
        a.albumId = (int)jd["albumId"];
        a.title = (string)jd["title"];
        a.movieMaker = (string)jd["movieMaker"];
        a.template = (int)jd["template"];
        a.subtitleColor = (string)jd["subtitleColor"];
        a.movieStory = (string)jd["movieStory"];
        a.movieCover = (string)jd["movieCover"];
        JsonData jdchild1 = jd["bgMusic"];
        for (int i = 0; i < jdchild1.Count; i++)
        {
            a.bgMusicLis.Add((string)jdchild1[i]);
        }
        JsonData jdchild2 = jd["picUrls"];
        for (int i = 0; i < jdchild2.Count; i++)
        {
            a.picUrlsLis.Add((string)jdchild2[i]);
        }
        JsonData jdchild3 = jd["picDescs"];
        for (int i = 0; i < jdchild3.Count; i++)
        {
            a.picDescsLis.Add((string)jdchild3[i]);
        }
        JsonData jdchild4 = jd["effects"];
        mAlbunQue.Enqueue(a);
    }





    void StartDownload()
    {
        if (mAlbunQue.Count>0)
        {
            mAlbum = mAlbunQue.Dequeue();
            mProgressIndex = mAlbum.picUrlsLis.Count + 1;
            mProgressIndex = 100 / mProgressIndex;
            StartCoroutine(DownloadSound(mAlbum.bgMusicLis[0]));
            vp_Timer.In(3.5f, delegate ()
            {
                StartCoroutine(DownloadImage(mAlbum));
            });
        }
    }



    #region 获取服务器数据

    #region 下载音乐
    /// <summary>
    /// 下载音乐
    /// </summary>
    /// <param name="mp3Path"></param>
    /// <returns></returns>
    IEnumerator DownloadSound(string mp3Path)
    {
        www = new WWW(mp3Path);
        yield return www;
        byte[] bytes = www.bytes;
        CreatFileMusic(bytes);
        //StartCoroutine(StartSaveMusic(bytes));
    }
    
    /// <summary>
    /// 写入音乐
    /// </summary>
    /// <param name="bytes"></param>
    void CreatFileMusic(byte[] bytes)
    {
        CredPath(mMusicPath);
        mMusicPath = mMusicPath + "{0}.mp3";
        mMusicPath = string.Format(mMusicPath, mAlbum.template);
        File.WriteAllBytes(mMusicPath, bytes);
        //PlayLocalFile(mMusicPath);
    }
    #endregion

    #region 下载图片并引入到物体
    /// <summary>
    /// 下载图片
    /// </summary>
    /// <param name="album"></param>
    /// <returns></returns>
    IEnumerator DownloadImage(Album album)
    {
        mPicindex++;
        if (mPicindex == album.picUrlsLis.Count)
        {
            //TODO初始化模板
            mParentLabel.SetActive(false);
            SetMaterial();
            // SetDes();
            this.GetComponent<AnimWHC>().enabled = true;
            yield break;
        }
        //mImagePath = mOldImagePath;
        //mImagePath = string.Format(mImagePath, SubString(album.picUrlsLis[mPicindex]));
        www = new WWW(album.picUrlsLis[mPicindex]);
        yield return www;
        byte[] bytes = www.bytes;
        CreatFilePic(bytes);
        mDownLoadPress.text = Mathf.Abs(mProgressIndex * (mPicindex + 2)) + "%";//下载进度
        if (Mathf.Abs(mProgressIndex * (mPicindex + 2)) > 90)
        {
            mDownLoadPress.text = "100%";
        }
        yield return new WaitForSeconds(0.5f);
        logingImage();
    }

    void logingImage()
    {
        StartCoroutine(DownloadImage(mAlbum));
    }

    /// <summary>
    /// 加入到集合,引用时候取出
    /// </summary>
    /// <param name="bytes"></param>
    void CreatFilePic(byte[] bytes)
    {
        lock (this)
        {
            tex.WaitOne();
            // File.WriteAllBytes(picName, bytes);
            tex.ReleaseMutex();
            Texture2D texture = new Texture2D(512, 256);
            texture.LoadImage(bytes);
            vp_Timer.In(0.1f, delegate ()
            {
                mPicList.Add(texture);
            });
        }
    }
    
    /// <summary>
    /// 给物体引入从网上获取的图片
    /// </summary>
    void SetMaterial()
    {
        Pictrue0.GetComponent<MeshRenderer>().material.mainTexture = mPicList[0];
        print(mPicList[0].ToString());
        Pictrue1.GetComponent<MeshRenderer>().material.mainTexture = mPicList[1];
        Pictrue2.GetComponent<MeshRenderer>().material.mainTexture = mPicList[2];
        Pictrue3.GetComponent<MeshRenderer>().material.mainTexture = mPicList[3];
        Pictrue4.GetComponent<MeshRenderer>().material.mainTexture = mPicList[4];
        Pictrue5.GetComponent<MeshRenderer>().material.mainTexture = mPicList[5];
        Pictrue6.GetComponent<SkinnedMeshRenderer>().material.mainTexture = mPicList[6];
    }
    #endregion


    #endregion




    #region 暂时没用
    /// <summary>
    /// 暂时没用播放
    /// </summary>
    /// <param name="audioPath"></param>
    void PlayLocalFile(string audioPath)
    {
        var exists = File.Exists(audioPath);
        Debug.LogFormat("{0}，存在:{1}", audioPath, exists);
        StartCoroutine(LoadAudio(audioPath, (audioClip) =>
        {
            mAudio.clip = audioClip;
            mAudio.Play();
        }));
    }

    IEnumerator LoadAudio(string filePath, System.Action<AudioClip> loadFinish)
    {
        //安卓和PC上的文件路径
        //filePath = "file://" + filePath;
        //filePath = filePath.Replace("/", @"\");

      Debug.LogFormat("local audioclip :{0}", filePath);
        WWW www = new WWW(filePath);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            AudioClip audioClip = null;

            //if (filePath.EndsWith("ogg"))
            //{
            //    audioClip = www.GetAudioClip(false, true, AudioType.OGGVORBIS);
            //}
            //else
            {
                audioClip = www.GetAudioClip();
            }
            loadFinish(audioClip);
        }
        else
        {
            Debug.LogErrorFormat("www load file error:{0}", www.error);
        }
    }


    string SubString(string name)
    {
        int startIndex = -1;
        int endIndex = -1;
        startIndex = name.LastIndexOf("/") + 1;
        if (startIndex == 0) startIndex = 1;
        endIndex = name.LastIndexOf(".");
        endIndex = endIndex - startIndex;
        name = name.Substring(startIndex, endIndex);
        return name;
    }


    //设置字幕
    void SetDes()
    {
        for (int i=0;i< mPicList.Count;i++)
        {
            mPicLabel.transform.GetChild(i).GetComponent<UILabel>().text = "["+ mAlbum .subtitleColor+ "]"+mAlbum.picDescsLis[i]+"[-]";
        }
    }
    #endregion
}


#region XML实体类
public class Album  {
    public string authToken { get; set; }
    public string webServerNoticeUrl { get; set; }
    public int serverId { get; set; }
    public int albumId { get; set; }
    public string title { get; set; }
    public string movieMaker { get; set; }
    public int template { get; set; }
    public string subtitleColor { get; set; }
    public string movieStory { get; set; }
    public string movieCover { get; set; }
    public List<string> bgMusicLis = new List<string>();
    public List<string> picUrlsLis = new List<string>();
    public List<string> picDescsLis = new List<string>();
    public List<string> effectsLis = new List<string>();

}
#endregion


