using ImGif;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GifData))]
public class GifDataEditor : Editor
{
    GifData data;
    GifPreview preview;

    private void OnEnable()
    {
        data = target as GifData;
    }

    public override void OnInspectorGUI()
    {
        if(preview == null)
            preview = GifUtility.GetPreview(data.Bytes);

        GUILayout.Label($"Size: {(data.Bytes.Length / 1000f).ToString("0.00")} kb");
        GUILayout.Label($"Frames: {preview.frames}");
        GUILayout.Label("Preview: ");
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), preview.texture);
    }
}
