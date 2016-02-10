using System;

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
        


    /// <summary>
    /// Target to follow
    /// </summary>
    public VisualCharacter Target
    {
        get; set;
    }

    void OnChanged()
    {

    }

}

