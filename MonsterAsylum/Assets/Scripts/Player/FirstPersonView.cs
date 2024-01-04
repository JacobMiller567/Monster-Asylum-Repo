using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera pantsCamera;

    void Start()
    {
        pantsCamera.cullingMask = 1 << LayerMask.NameToLayer("Pants");
        pantsCamera.clearFlags = CameraClearFlags.Depth;
        pantsCamera.depth = firstPersonCamera.depth + 1;
    }
}
