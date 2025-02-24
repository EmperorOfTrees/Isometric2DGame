using Unity.Cinemachine;
using UnityEngine;

public class FindCamera : MonoBehaviour
{
    private CinemachineCamera cam;
    private void Start()
    {
        cam = FindAnyObjectByType<CinemachineCamera>();

        cam.LookAt = gameObject.transform;
        cam.Follow = gameObject.transform;
    }
}
