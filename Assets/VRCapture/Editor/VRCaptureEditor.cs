using UnityEngine;
using UnityEditor;
using VRCapture;
using System.IO;

[CustomEditor(typeof(VRCapture.VRCapture))]
public class VRCaptureEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Prepare Standalone Build")) {
            PrepareStandaloneBuild();
        }
    }
    public string FFmpegPath {
        get {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            return VRCaptureConfig.FFMPEG_WIN_PATH;
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            return  VRCaptureConfig.FFMPEG_MAC_PATH;
#endif
        }
    }
    void PrepareStandaloneBuild() {
        if (Directory.Exists(VRCaptureConfig.FFmpegStandaloneDir()))
            Directory.Delete(VRCaptureConfig.FFmpegStandaloneDir(), true);
        Directory.CreateDirectory(VRCaptureConfig.FFmpegStandaloneDir());
        string source = VRCaptureConfig.FFmpegPackageDir() + FFmpegPath;
        string dest = VRCaptureConfig.FFmpegStandaloneDir() + FFmpegPath;
        if (!File.Exists(dest)) {
            File.Copy(source, dest);
        }
        AssetDatabase.Refresh();
    }
}

