
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLayer : MonoBehaviour
{
    public int Layer = 0;
    public int Layer2 = 1;
    void Start()
    {
        Physics.IgnoreLayerCollision(3, 6);
    }
}
