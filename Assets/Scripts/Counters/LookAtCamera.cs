using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        lookAt,
        LookAtInverted,
        cameraForward,
        cameraForwardInverted,
    }

    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        { 
            case Mode.lookAt: 
                transform.LookAt(Camera.main.transform);
                 break; 
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position +dirFromCamera);
                 break;
            case Mode.cameraForward:
                transform.forward= Camera.main.transform.forward;
                break;
            case Mode.cameraForwardInverted:
                transform.forward = Camera.main.transform.forward * -1;
                break;
        }
    }
}