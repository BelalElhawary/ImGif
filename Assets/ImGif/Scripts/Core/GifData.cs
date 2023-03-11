using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[PreferBinarySerialization]
public class GifData : ScriptableObject
{
    [HideInInspector] public byte[] Bytes;
}
