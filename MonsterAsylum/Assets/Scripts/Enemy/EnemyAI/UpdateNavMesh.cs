using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavMesh : MonoBehaviour
{
    public NavMeshSurface Surface;
    public NavMeshData navMeshData;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) //
        {
            Surface.UpdateNavMesh(navMeshData);
        }
    }
    
}
