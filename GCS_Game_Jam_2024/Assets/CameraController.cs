// View documentation at https://github.com/JDoddsNAIT/Unity-Scripts/blob/main/Scripts/Follow-Transform/
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public List<Transform> transforms = new List<Transform>();
    public Vector3 targetOffset;
    public float deadZoneRadius;
    public float followSpeed;

    public bool2 useAxes;

    void Update()
    {
        Vector3 followPosition;
        followPosition = AveragePositions(transforms);

        Vector3 targetPosition = transform.position + targetOffset;
        Vector3 targetDirection = Vector3.Normalize(followPosition - targetPosition);
        float targetDistance = Vector3.Distance(followPosition, targetPosition) - deadZoneRadius;

        if (targetDistance >= 0)
        {
            Vector2 translation;

            translation.x = useAxes.x ? 
                followSpeed * targetDistance * Time.deltaTime * targetDirection.x : 0;
            translation.y = useAxes.y ?
                followSpeed * targetDistance * Time.deltaTime * targetDirection.y : 0;

            transform.Translate(translation);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 followPosition = AveragePositions(transforms);

        Vector3 targetPosition = transform.position + targetOffset;
        Vector3 targetDirection = Vector3.Normalize(followPosition - targetPosition);
        Vector3 gizmoPosition = targetPosition + targetDirection * deadZoneRadius;
        float targetDistance = Vector3.Distance(followPosition, targetPosition) - deadZoneRadius;

        Gizmos.color = targetDistance > 0 ? 
            Color.green : Color.red;
        
        foreach (var transform in transforms)
        {
            Gizmos.DrawLine(transform.position, followPosition);
        }

        Gizmos.DrawLine(gizmoPosition, followPosition);
        Gizmos.DrawWireSphere(targetPosition, deadZoneRadius);
    }

    public Vector3 AveragePositions(List<Transform> positions)
    {
        Vector3 returnPosition = Vector3.zero;
        foreach (Transform t in positions)
        {
            returnPosition += t.position;
        }
        return returnPosition / positions.Count;
    }

    public void RemoveTransform(Transform transform)
    {
        transforms.Remove(transform);
        Debug.Log($"Transform \"{transform}\" has been removed.");
    }
    public void AddTransform(Transform transform)
    {
        transforms.Add(transform);
        Debug.Log($"Transform \"{transform}\" has been added.");
    }
}
