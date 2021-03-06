﻿using UnityEngine;

namespace VRCapture.Demo {

    public class VideoCaptureManager : MonoBehaviour {

        bool enableMainCamera = false;
        bool enable360Camera = false;
        bool enableTopDownCamera = false;
        bool enableLeftRightCamera = true;
        bool enableOnlyAudio = false;

        void Start() {
            VRCapture.Instance.RegisterSessionCompleteDelegate(HandleCaptureFinish);
            //Application.runInBackground = true;
        }

        void OnGUI() {
            //enableMainCamera = GUI.Toggle(
            //    new Rect(50, 50, 150, 50),
            //    enableMainCamera,
            //    " Enable Main Camera");
            //enableTopDownCamera = GUI.Toggle(
            //    new Rect(50, 100, 150, 50),
            //    enableTopDownCamera,
            //    " Enable Top-Down Camera");
            //enableLeftRightCamera = GUI.Toggle(
            //    new Rect(50, 150, 150, 50),
            //    enableLeftRightCamera,
            //    " Enable Left-Right Camera");
            //enable360Camera = GUI.Toggle(
            //    new Rect(50, 200, 150, 50),
            //    enable360Camera,
            //    " Enable 360 Camera");
            //enableOnlyAudio = GUI.Toggle(
            //    new Rect(50, 250, 150, 50),
            //    enableOnlyAudio,
            //    " Enable Only Audio");
            if (enable360Camera)
            {
                enableMainCamera = false;
                enableTopDownCamera = false;
                enableLeftRightCamera = false;
                enableOnlyAudio = false;
            }
            if (enableOnlyAudio)
            {
                enableMainCamera = false;
                enableTopDownCamera = false;
                enableLeftRightCamera = false;
                enable360Camera = false;
            }
            if (enableMainCamera)
            {
                VRCapture.Instance.GetCaptureVideo(0).isEnabled = true;
            }
            else
            {
                VRCapture.Instance.GetCaptureVideo(0).isEnabled = false;
            }
            if (enableTopDownCamera)
            {
                VRCapture.Instance.GetCaptureVideo(1).isEnabled = false;
            }
            else
            {
                VRCapture.Instance.GetCaptureVideo(1).isEnabled = false;
            }
            if (enableLeftRightCamera)
            {
                VRCapture.Instance.GetCaptureVideo(2).isEnabled = true;
            }
            else
            {
                VRCapture.Instance.GetCaptureVideo(2).isEnabled = false;
            }
            if (enable360Camera)
            {
                VRCapture.Instance.GetCaptureVideo(3).isEnabled = false;
            }
            else
            {
                VRCapture.Instance.GetCaptureVideo(3).isEnabled = false;
            }
            //if (GUI.Button(new Rect(50, 350, 150, 50), "Capture Start"))
            //{
            //    print("Capture Start");
            //    VRCapture.Instance.BeginCaptureSession();
            //}
            //if (GUI.Button(new Rect(50, 450, 150, 50), "Capture Stop"))
            //{
            //    print("Capture Stop");
            //    VRCapture.Instance.EndCaptureSession();
            //}
            //if (GUI.Button(new Rect(50, 550, 150, 50), "Open Video Folder"))
            //{
            //    System.Diagnostics.Process.Start(VRCapture.Instance.FolderPath);
            //}
        }

        void HandleCaptureFinish() {
            print("Capture Finish");
        }
    }
}