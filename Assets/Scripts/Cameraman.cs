using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Cameraman : MonoBehaviour
{
    [SerializeField] private CameraReference camReference;

    private void Awake()
    {
        camReference.camera = GetComponent<Camera>();
    }
}