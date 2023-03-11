using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace ImGif
{
    public class GifEditor : EditorWindow
    {
        [MenuItem("Window/ImGif/Import")]
        [ContextMenu("ImGif/Import")]
        public static void ShowWindow()
        {
            var path = EditorUtility.OpenFilePanel("Open gif file", "", "gif");
            if (path != string.Empty)
                {
                    var bytes = File.ReadAllBytes(path);
                    var fileName = path.Split('/').Last().Replace(".gif", "");
                    GifData asset = ScriptableObject.CreateInstance<GifData>();
                    asset.Bytes = bytes;
                    AssetDatabase.CreateAsset(asset, $"Assets/{fileName}.asset");
                    AssetDatabase.SaveAssets();
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = asset;
                }
        }
    }
}
