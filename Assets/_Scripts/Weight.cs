using System;
using UnityEngine;

public class Weight : RodBalance
{
    public int finalWeight;
    void Update()
    {
        GetComponent<Renderer>().materials[0].SetTexture("_MainTex", Resources.Load<Texture2D>("Label3_" + finalWeight.ToString()));
        transform.rotation = Quaternion.identity;
    }
}
