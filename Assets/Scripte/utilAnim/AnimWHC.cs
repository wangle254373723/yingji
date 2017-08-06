using UnityEngine;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System;
using VRCapture;
/*
 最初已用合成视频类
     */
namespace VRCapture
{

    public class AnimWHC : MonoBehaviour
    {
        public GameObject One;
        public GameObject Two;
        public Transform OnePictrue;
        public Transform TWOPictrue;
        public Transform ThreePictrue;
        public Transform ForePictrue;
        public Transform FivePictrue;
        public Transform SixPictrue;
        public Transform SevenPictrue;
        public UILabel mPicDes;
        public TweenAlpha mTween;
        public TweenAlpha mMTween;
        public Camera mMianCamera;
        public int mJPG = 3;       //帧率30 截图数量2
        float mTime = 0;
        bool mEnd = false;
        float time;
        int indexLabel = 0;
        Album mAlbum = null;
        public string mExePath;
        //string mCloneExePath;
        string mMergePath;
        int minTime = 0;
        int secTime = 0;
        string min;
        string sec;
        public string unityPath;
        public string videoPath;
        public bool isStart = false;
        public static AnimWHC instance;



        /// <summary>
        /// 字体动画
        /// </summary>
        void StartAnim()
        {
            mPicDes.text = "[" + mAlbum.subtitleColor + "]" + mAlbum.picDescsLis[0] + "[-]";
            mPicDes.gameObject.SetActive(true);
            mTween.PlayForward();
            OnePictrue.parent.gameObject.SetActive(true);
        }

        #region 数据初始化及图片动画的播放
        private void Start()
        {
            instance = this;
            unityPath = Application.dataPath;
            Directory.SetCurrentDirectory(Directory.GetParent(unityPath).FullName);
            unityPath = Directory.GetCurrentDirectory();
            //手动拷贝
            //mExePath = Application.dataPath + "/Resources/moviemaker.exe";
            //mCloneExePath = Application.dataPath + "/moviemaker.exe";
            mMergePath = unityPath + "/merge.cmd";
            mExePath = unityPath + "/ffmpeg.exe";
            //StartClone();
            mAlbum = ReadJson.instance.mAlbum;
            videoPath = unityPath + "/video/" + mAlbum.albumId + ".mp4";
            //VRCapture.Instance.mMoviePath = videoPath;
            //VRCaptureVideo.instance.FilePath = videoPath;
            mTween = mPicDes.GetComponent<TweenAlpha>();
            StartAnim();
            StartTask();
            vp_Timer.In(0.5f, delegate ()
            {
                mTween.PlayReverse();
                vp_Timer.In(0f, delegate ()
                {
                    OnePictrue.parent.gameObject.SetActive(true);
                    mPicDes.text = "[" + mAlbum.subtitleColor + "]" + mAlbum.picDescsLis[0] + "[-]";
                    mTween.PlayForward();
                });
            });
            vp_Timer.In(5f, delegate ()
            {
                mTween.PlayReverse();
                mMTween.enabled = true;
                vp_Timer.In(0.8f, delegate ()
                {
                    mMTween.gameObject.GetComponent<UITexture>().alpha = 0;
                    mMTween.enabled = false;
                    OnePictrue.parent.gameObject.SetActive(false);
                    mMianCamera.orthographicSize = 6f;
                    sizeTime = 0;
                    TWOPictrue.parent.gameObject.SetActive(true);
                    mPicDes.text = "[" + mAlbum.subtitleColor + "]" + mAlbum.picDescsLis[1] + "[-]";
                    mTween.PlayForward();
                });
            });
            vp_Timer.In(12, delegate ()
            {
                mTween.PlayReverse();
                mMTween.enabled = true;
                vp_Timer.In(0.8f, delegate ()
                {
                    mMTween.gameObject.GetComponent<UITexture>().alpha = 0;
                    mMTween.enabled = false;
                    TWOPictrue.parent.gameObject.SetActive(false);
                    mMianCamera.orthographicSize = 6f;
                    sizeTime = 0;
                    ThreePictrue.parent.gameObject.SetActive(true);
                    mPicDes.text = "[" + mAlbum.subtitleColor + "]" + mAlbum.picDescsLis[2] + "[-]";
                    mTween.PlayForward();
                });
            });
            vp_Timer.In(20, delegate ()
            {
                mTween.PlayReverse();
                mMTween.enabled = true;
                vp_Timer.In(0.8f, delegate ()
                {
                    mMTween.enabled = false;
                    mMTween.gameObject.GetComponent<UITexture>().alpha = 0;
                    ThreePictrue.parent.gameObject.SetActive(false);
                    mMianCamera.orthographicSize = 6f;
                    sizeTime = 0;
                    ForePictrue.parent.gameObject.SetActive(true);
                    mPicDes.text = "[" + mAlbum.subtitleColor + "]" + mAlbum.picDescsLis[3] + "[-]";
                    mTween.PlayForward();
                });
            });
            vp_Timer.In(25, delegate ()
            {
                mTween.PlayReverse();
                mMTween.enabled = true;
                vp_Timer.In(1.5f, delegate ()
                {
                    mMTween.enabled = false;
                    mMTween.gameObject.GetComponent<UITexture>().alpha = 0;
                    ForePictrue.parent.gameObject.SetActive(false);
                    mMianCamera.orthographicSize = 6f;
                    sizeTime = 0;
                    FivePictrue.parent.gameObject.SetActive(true);
                    mPicDes.text = "[" + mAlbum.subtitleColor + "]" + mAlbum.picDescsLis[4] + "[-]";
                    mTween.PlayForward();
                });
            });
            vp_Timer.In(30, delegate ()
            {
                mTween.PlayReverse();
                mMTween.enabled = true;
                vp_Timer.In(0.8f, delegate ()
                {
                    mMTween.enabled = false;
                    mMTween.gameObject.GetComponent<UITexture>().alpha = 0;
                    FivePictrue.parent.gameObject.SetActive(false);
                    mMianCamera.orthographicSize = 6f;
                    sizeTime = 0;
                    SixPictrue.parent.gameObject.SetActive(true);
                    mPicDes.text = "[" + mAlbum.subtitleColor + "]" + mAlbum.picDescsLis[5] + "[-]";
                    mTween.PlayForward();
                });
            });
            vp_Timer.In(35, delegate ()
            {
                mTween.PlayReverse();
                mMTween.enabled = true;
                vp_Timer.In(0.8f, delegate ()
                {
                    mMTween.enabled = false;
                    mMTween.gameObject.GetComponent<UITexture>().alpha = 0;
                    SixPictrue.parent.gameObject.SetActive(false);
                    mMianCamera.orthographicSize = 6f;
                    sizeTime = 0;
                    SevenPictrue.parent.gameObject.SetActive(true);
                    mPicDes.text = "[" + mAlbum.subtitleColor + "]" + mAlbum.picDescsLis[6] + "[-]";
                    mTween.PlayForward();
                });
            });
            vp_Timer.In(42, delegate ()
            {
                //改用VR插件后,结束VR的合成逻辑
                VRCapture.Instance.EndCaptureSession();


                #region 计算视频时间
                //isStart = true;
                //minTime = CuttingScreen.instance.index / 25;
                //print("截图数" + CuttingScreen.instance.index);
                //print("时间" + minTime);
                //if (minTime > 60)
                //{
                //    secTime = minTime % 60;
                //    minTime = minTime / 60;
                //    if (minTime > 10)
                //    {
                //        min = minTime.ToString();
                //    }
                //    else
                //    {
                //        min = "0" + minTime;
                //    }
                //    if (secTime > 10)
                //    {
                //        sec = secTime.ToString();
                //    }
                //    else
                //    {
                //        sec = "0" + secTime;
                //    }
                //}
                //else
                //{
                //    min = "00";
                //    if (minTime > 10)
                //    {
                //        sec = minTime.ToString();
                //    }
                //    else
                //    {
                //        sec = "0" + minTime;
                //    }
                //}
                //print("分钟" + min + "秒" + sec);
                //Write(mMergePath);
                //mPicDes.gameObject.SetActive(false);
                //mMergePath = unityPath + "/merge";
                //RunShellThreadStart(mMergePath);
                #endregion

                vp_Timer.In(3, delegate ()
                {
                //最后判断是否有该文件,及请求服务器结束
                isFinish = HaveDir(videoPath);
                    EndTask();
                });
            });
        }
        #endregion


        /// <summary>
        /// 请求合成视频开始
        /// </summary>
        void StartTask()
        {
            print("请求合成开始Start");
            string startPath = "http:" + "//album.hemudi.cn/index-albummake-notice.html?action=start&serverId={0}&albumId={1}&authToken={2}";
            startPath = string.Format(startPath, mAlbum.serverId, mAlbum.albumId, mAlbum.authToken);
            StartCoroutine(SendPost(0, startPath));
            VRCapture.Instance.BeginCaptureSession();
        }



        /// <summary>
        /// 合成结束
        /// </summary>
        void EndTask()
        {
            string endPath;
            string action;
            string errorCode;
            if (isFinish)
            {
                action = "finish";
            }
            else
            {
                action = "fail";
            }
            if (!isFinish)
            {
                errorCode = "111";
                endPath = "http:" + "//album.hemudi.cn/index-albummake-notice.html?action={0}&serverId={1}&albumId={2}&authToken={3}&videoUrl={4}&errorCode={5}";
                endPath = string.Format(endPath, action, mAlbum.serverId, mAlbum.albumId, mAlbum.authToken, mAlbum.webServerNoticeUrl + "/" + mAlbum.albumId + ".mp4", errorCode);
            }
            else
            {
                endPath = "http:" + "//album.hemudi.cn/index-albummake-notice.html?action={0}&serverId={1}&albumId={2}&authToken={3}&videoUrl={4}";
                endPath = string.Format(endPath, action, mAlbum.serverId, mAlbum.albumId, mAlbum.authToken, mAlbum.webServerNoticeUrl + "/" + mAlbum.albumId + ".mp4");
            }
            StartCoroutine(SendPost(1, endPath));
        }

        bool isFinish = false;   //是否成功
        IEnumerator SendPost(int biaoshi, string _url)
        {
            print("发送请求加标识===" + biaoshi);
            print("请求网址" + _url);
            WWW postData = new WWW(_url);
            yield return postData;
            if (postData.error != null)
            {
            }
            if (biaoshi == 1)
            {
                DelectDir(CuttingScreen.instance.mPath);
                Application.Quit();
            }
        }

        /// <summary>
        /// 判断文件是否存在且大于0字节
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool HaveDir(string path)
        {

            if (!File.Exists(path)) return false;
            FileStream stream = new FileInfo(path).OpenRead();
            byte[] buffer = new byte[stream.Length + 1];

            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
            if (buffer.Length < 0)
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// 删除截图
        /// </summary>
        /// <param name="imagePath"></param>
        public static void DelectDir(string imagePath)
        {
            if (Directory.Exists(imagePath))
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(imagePath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                        }
                        else
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }
            else
            {
                Directory.CreateDirectory(imagePath);
            }
        }



        /// <summary>
        /// 调用合成命令
        /// </summary>
        /// <param name="path"></param>
        public static void RunShellThreadStart(string path)
        {
            print("合成命令调用" + path);
            //        string cmdTxt = @"echo test
            //notepad C:\Users\wxy\Desktop\1.txt";
            //string cmdTxt = @"\unity4.6Project\BuyingAndSelling\merge";
            //string cmdTxt = "echo %cd%";
            path.Replace("\\", "/");
            RunCommand(path);
            //RunProcessCommand("merge", cmdTxt);
        }


        //正式调用命令
        private static void RunCommand(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = command;

            process.StartInfo.CreateNoWindow = false; // 获取或设置指示是否在新窗口中启动该进程的值（不想弹出powershell窗口看执行过程的话，就=true）
            process.StartInfo.ErrorDialog = true; // 该值指示不能启动进程时是否向用户显示错误对话框
            process.StartInfo.UseShellExecute = true;
            //process.StartInfo.RedirectStandardError = true;
            //process.StartInfo.RedirectStandardInput = true;
            //process.StartInfo.RedirectStandardOutput = true;

            process.Start();

            //process.StandardInput.WriteLine(@"explorer.exe D:\");
            //process.StandardInput.WriteLine("pause");

            process.WaitForExit();
            process.Close();
        }


        /// <summary>
        /// 写入bat脚本
        /// </summary>
        /// <param name="path"></param>
        public void Write(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            string toolDes = "echo %cd% \r\n moviemaker -f image2 -i screen_{0}\\%%d.jpg -i bgmusic\\{1}.mp3 -ss 00:00:00 -to 00:{2}:{3} -vcodec libx264 -r 30 video\\{4}.mp4 -y \r\n pause";
            toolDes = string.Format(toolDes, mAlbum.albumId, mAlbum.template, min, sec, mAlbum.albumId);
            sw.Write(toolDes);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        float timeFPS = 0;
        float sizeTime = 0;


        //没用VR时每帧截图
        //private void FixedUpdate()
        //{
        //    if (isStart) return;
        //    timeFPS += Time.deltaTime;
        //    if (timeFPS >= 0.3f)
        //    {
        //        timeFPS = 0;
        //        for (int i = 0; i < mJPG; i++)
        //        {
        //            CuttingScreen.instance.CuttToJPG();
        //        }
        //    }
        //    sizeTime += Time.deltaTime;
        //    mMianCamera.orthographicSize -= sizeTime*0.0001f;
        //    if (mMianCamera.orthographicSize <= 5.3f)
        //    {
        //        mMianCamera.orthographicSize = 5.3f;
        //    }
        //}
    }
}
