using UnityEngine;

public class Billboard : MonoBehaviour
{
    public float distance = 4f;
    public float smoothness = 5f;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (!_mainCamera)
        {
            _mainCamera = Camera.main;
            if (!_mainCamera) return;
        }

        Vector3 targetPosition = _mainCamera.transform.position + _mainCamera.transform.forward * distance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothness);

        Quaternion targetRotation = Quaternion.LookRotation(_mainCamera.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
    }
}