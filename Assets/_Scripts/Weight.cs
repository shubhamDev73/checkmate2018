using System;
using UnityEngine;

public class Weight : RodBalance
{
    public int finalWeight;
    void Update()
    {
        GetComponent<Renderer>().materials[0].SetTexture("_Maintex", Resources.Load<Texture2D>("Label3_5"));// + finalWeight.ToString()));
    }
}
