using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosHelper : MonoBehaviour {

    public Color color;

    public _gizmoTypeList GizmoType;

    public bool GetParentSize;

    public Vector3 CubeSize;

    public int SphereRadius;

    public bool DisplaySidePlacement;
    public float divideBy = 2;

    // The selectable values for the dropdown, with custom names.
    public enum _gizmoTypeList
    {
        WireCube,
        WireSphere
    };


    void OnDrawGizmos()
    {
        color.a = 1;
        Gizmos.color = color;
        if (GizmoType == _gizmoTypeList.WireSphere)
        {
            Gizmos.DrawWireSphere(transform.position, SphereRadius);
        }
        else if (GizmoType == _gizmoTypeList.WireCube)
        {
            if (GetParentSize)
            {
                CubeSize = transform.localScale;
            }

            Gizmos.DrawWireCube(transform.position, CubeSize);
        }

        if (DisplaySidePlacement)
        {
            var minPos = transform.TransformPoint(Vector3.left / divideBy);
            var maxPos = transform.TransformPoint(Vector3.right / divideBy);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(minPos, 1);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(maxPos, 1);
        }

    }
}
