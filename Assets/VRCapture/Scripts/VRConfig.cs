using UnityEngine;
/*
 VR 把音乐,视频合成完整音效视频
     */
namespace VRCapture {
    /// <summary>
    /// Common config.
    /// </summary>
    public class VRCommonConfig {
        public const float EPSILON = 0.000001f;
    }
    /// <summary>
    /// Basic config for VRCapture.
    /// </summary>
    public class VRCaptureConfig {
        public static string FFmpegPackageDir() { return ffmpegStorageDir; }
        public static string FFmpegStandaloneDir() { return ffmpegStandaloneDir; }
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        private static string ffmpegStorageDir = ffmpegStorageDir = Application.dataPath + "/VRCapture/FFmpeg/Win/";
        private static string ffmpegStandaloneDir = ffmpegStandaloneDir = Application.streamingAssetsPath + "/VRCapture/FFmpeg/Win/";
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        private static string ffmpegStorageDir = ffmpegStorageDir = Application.dataPath + "/VRCapture/FFmpeg/Mac/";
        private static string ffmpegStandaloneDir = ffmpegStandaloneDir = Application.streamingAssetsPath + "/VRCapture/FFmpeg/Mac/";
#endif
        public const string CAPTURE_FOLDER = "VRCapture";
        public const string FFMPEG_WIN_PATH = "ffmpeg.exe";
        public const string FFMPEG_MAC_PATH = "ffmpeg";
    }
    /// <summary>
    /// Basic config for VRReplay.
    /// </summary>
    public class VRReplayConfig {
        public const string REPLAY_FOLDER = "VRCapture/VRReplay";
        public const string REPLAY_FILE = "replaytrace.bin";
        public const int MAX_DEVICE_NUM = 16;
    }
}