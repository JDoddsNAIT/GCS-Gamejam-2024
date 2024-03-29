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
    public float numspeed;

    public bool2 useAxis;

    public bool drawGizmos = true;
    private Vector3 _startPosition = Vector3.zero;

    private void Start()
    {
        FindPlayers();
        _startPosition = this.transform.position;
    }

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

            translation.x = useAxis.x ?
                followSpeed * targetDistance * Time.deltaTime * targetDirection.x : followSpeed * Time.deltaTime;
            translation.y = useAxis.y ?
                followSpeed * targetDistance * Time.deltaTime * targetDirection.y : 0;

            transform.Translate(translation);
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
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
    public void AddTransform(Transform transform)
    {
        transforms.Add(transform);
        Debug.Log($"Transform \"{transform}\" has been added.");
    }
    public void RemoveTransform(Transform transform)
    {
        transforms.Remove(transform);
        Debug.Log($"Transform \"{transform}\" has been removed.");
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
    }

    public void CameraFreeze(float numspeed)
    {
        followSpeed = numspeed;
    }


    public void FindPlayers()
    {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            transforms.Add(gameObject.transform);
        }
    }
}
