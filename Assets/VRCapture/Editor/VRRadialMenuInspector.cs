using UnityEngine;
using VRCapture;
using UnityEditor;

[CustomEditor(typeof(VRRadialMenu))]
public class VRRadialMenuInspector : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        VRRadialMenu rMenu = (VRRadialMenu)target;
        if (GUILayout.Button("Regenerate Buttons")) {
            rMenu.RegenerateButtons();
        }
    }
}
