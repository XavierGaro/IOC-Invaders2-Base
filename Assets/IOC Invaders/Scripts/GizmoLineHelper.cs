using UnityEngine;

public class GizmoLineHelper : MonoBehaviour
{
    void OnDrawGizmos()
    {
        DrawGizmos(transform);
    }

    private void DrawGizmos(Transform trans)
    {
        if (trans.childCount == 0)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(trans.position, .012f);

        Vector3 lastPosition = trans.GetChild(0).position;
        for (int i = 1; i < trans.childCount; i++)
        {
            Gizmos.DrawLine(lastPosition, trans.GetChild(i).position);
            lastPosition = trans.GetChild(i).position;
        }
    }
}