using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    private float _rotateSpeed = 5f;
    public Transform pivot;
    private float _maxViewAngle = 65f;
    private float _minViewAngle = -60f;

    public bool invertY;

    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;
        // pivot.transform.parent = target.transform;
        pivot.transform.parent = null;

       // Cursor.lockState = CursorLockMode.Locked; 
    }

    
    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;
        //Get the X position of the mouse & rotate the target
        float horizontal = Input.GetAxis("Mouse X") * _rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * _rotateSpeed;
        //pivot.Rotate(-vertical, 0, 0);
        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        //Limit up/down camera rotation
        if(pivot.rotation.eulerAngles.x > _maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(_maxViewAngle, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + _minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + _minViewAngle, 0, 0);
        }

        //Move the camera based on the current rotation of the target & the original offset
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y -.5f, transform.position.z);

        }
        transform.LookAt(target);
        
    }
}
