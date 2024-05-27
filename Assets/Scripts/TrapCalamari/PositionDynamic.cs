using UnityEngine;
using UnityEngine.Animations;

public class DynamicConstraintAssignment : MonoBehaviour
{
    public string targetObjectName = "Player"; // The name of the target object
    private PositionConstraint positionConstraint;

    // The rest position values
    public Vector3 restPositionOffset = new Vector3(0, 0, 0);

    void Start()
    {
        // Find the PositionConstraint component on this GameObject
        positionConstraint = GetComponent<PositionConstraint>();
        if (positionConstraint == null)
        {
            Debug.LogError("PositionConstraint component not found on this GameObject.");
            return;
        }

        // Find the target GameObject by name
        GameObject targetObject = GameObject.Find(targetObjectName);
        if (targetObject == null)
        {
            Debug.LogError("Target object not found.");
            return;
        }

        // Create a ConstraintSource
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = targetObject.transform;
        source.weight = 1.0f;

        // Clear existing sources and add the new one
        positionConstraint.SetSource(0, source);
        
        // Alternatively, if you want to add without clearing existing sources, use:
        // positionConstraint.AddSource(source);

        // Activate the constraint
        positionConstraint.constraintActive = true;

        // Calculate the rest position by adding the offset to the target's current position
        Vector3 targetPosition = targetObject.transform.position;
        Vector3 restPosition = targetPosition + restPositionOffset;

        // Apply the rest position as an offset to the GameObject's position
        transform.position = restPosition;
    }
}
