using UnityEngine;
using System.Diagnostics;

public class RunShell : ScriptableObject
{
    //[MenuItem("CMD/RunShell")]
    //public static void RunShellStart()
    //{
    //    // 这里不开线程的话，就会阻塞住unity的主线程，当然如果你需要阻塞的效果的话可以不开
    //    Thread newThread = new Thread(new ThreadStart(RunShellThreadStart));
    //    newThread.Start();
    //}
    private static RunShell mInstance;

    public static RunShell instance {

        get
        {
            if (mInstance==null)
            {
                mInstance = new RunShell();
            }
            return mInstance;
        }
    }

    public static void RunShellThreadStart(string path)
    {
        //        string cmdTxt = @"echo test
        //notepad C:\Users\wxy\Desktop\1.txt";
        //string cmdTxt = @"\unity4.6Project\BuyingAndSelling\merge";
        //string cmdTxt = "echo %cd%";
        path.Replace("\\","/");
        RunCommand(path);
        //RunProcessCommand("merge", cmdTxt);
    }

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

    private static void RunProcessCommand(string command, string argument)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = command;
        start.Arguments = argument;

        start.CreateNoWindow = false;
        start.ErrorDialog = true;
        start.UseShellExecute = false;

        Process p = Process.Start(start);
        p.WaitForExit();
        p.Close();
    }
}
