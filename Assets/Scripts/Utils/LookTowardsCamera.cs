using UnityEngine;

public class LookTowardsCamera : MonoBehaviour
{
    private Camera _camera;
    private bool _isDisabled;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _isDisabled = false;
    }

    private void LateUpdate()
    {
        if (!_isDisabled)
            transform.forward = _camera.transform.forward;
    }

    public void DisableLookTowardsCamera()
    {
        _isDisabled = true;
    }
}