using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BoxBezie : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField] float _t;
    [Space]
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private Transform _p3;
    [SerializeField] private Transform _p4;

    private void Update()
    {
        transform.position = Bezier.GetPoint(_p1.position, _p2.position, _p3.position, _p4.position, _t);
        transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(_p1.position, _p2.position, _p3.position, _p4.position, _t));
    }

    private void OnDrawGizmos()
    {
        int segmentAmount = 20;
        Vector3 previousPoint = _p1.position;

        for (int i = 0; i < segmentAmount + 1; i++)
        {
            float parameter = (float)i / segmentAmount;
            Vector3 point = Bezier.GetPoint(_p1.position, _p2.position, _p3.position, _p4.position, parameter);
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
