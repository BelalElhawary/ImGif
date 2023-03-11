using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifData : ScriptableObject
{
    private byte[] bytes;
    public byte[] Bytes { get => bytes; set => bytes = value;}
}
