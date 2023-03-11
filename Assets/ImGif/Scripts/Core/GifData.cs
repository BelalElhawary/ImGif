using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[PreferBinarySerialization]
public class GifData : ScriptableObject
{
    public byte[] Bytes;

    //public byte[] Bytes { get => bytes; set => bytes = value;}
}
