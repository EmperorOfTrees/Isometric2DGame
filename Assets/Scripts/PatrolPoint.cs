using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    // a patrol point for enemies

    public Vector3 myPosition;

    private void Awake()
    {
        myPosition = transform.position;
    }
}
