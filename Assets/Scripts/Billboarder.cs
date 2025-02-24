using UnityEngine;

public class Billboarder : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);

    }
}
