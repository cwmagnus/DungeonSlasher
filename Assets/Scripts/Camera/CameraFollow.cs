using UnityEngine;

/// <summary>
/// Makes the camera follow a target.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private string targetTagName;
    [SerializeField] private float damp;
    private Vector3 offset;

    /// <summary>
    /// Set the offset based on pre-existing position.
    /// </summary>
    private void Start()
    {
        offset = transform.position;
    }

    /// <summary>
    /// Damp towards the target position.
    /// </summary>
    private void Update()
    {
        Transform target = FindTarget(targetTagName);
        if (target != null)
        {
            Vector3 movementVector = offset + target.position;
            Vector3 velocity = Vector3.zero;

            transform.position = Vector3.SmoothDamp(transform.position, movementVector,
                ref velocity, Time.deltaTime * damp);
        }
    }

    /// <summary>
    /// Find the target transform.
    /// </summary>
    /// <param name="targetTag">Tag of the target to find.</param>
    /// <returns>Found target.</returns>
    private Transform FindTarget(string targetTag)
    {
        Transform target = GameObject.FindGameObjectWithTag(targetTag).transform;
        return target;
    }
}
