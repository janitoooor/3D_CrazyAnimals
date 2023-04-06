using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGizmo : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Ray ray = new (transform.position, transform.forward);
        Gizmos.DrawRay(ray);
    }
}
