using UnityEngine;

[PreferBinarySerialization]
public class GifData : ScriptableObject
{
    [HideInInspector] public byte[] Bytes;
}
