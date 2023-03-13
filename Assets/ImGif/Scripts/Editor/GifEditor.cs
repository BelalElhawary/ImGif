using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using static UnityEngine.GraphicsBuffer;

namespace ImGif
{
    [CustomEditor(typeof(Gif))]
    public class GifEditor : Editor
    {
        [MenuItem("Assets/ImGif/Import")]
        public static void ImportGifAsset()
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
