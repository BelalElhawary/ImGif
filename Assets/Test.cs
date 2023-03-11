using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GifData data;

    private void Start() {
        Debug.Log(data.Bytes.Length);
    }
}
