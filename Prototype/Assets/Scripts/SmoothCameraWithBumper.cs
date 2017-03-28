using UnityEngine;
using System.Collections;

public class SmoothCameraWithBumper : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float distance = 5.0f;
    [SerializeField]
    private float height = 1.0f;
    [SerializeField]
    private float damping = 5.0f;
    [SerializeField]
    private bool smoothRotation = true;
    [SerializeField]
    private float rotationDamping = 10.0f;

    [SerializeField]
    private Vector3 targetLookAtOffset; // allows offsetting of camera lookAt, very useful for low bumper heights

    [SerializeField]
    private float bumperDistanceCheck = 5f; // length of bumper ray
    [SerializeField]
    private float bumperCameraHeight = 1.0f; // adjust camera height while bumping
    [SerializeField]
    private Vector3 bumperRayOffset; // allows offset of the bumper ray from target origin

    public Camera cameraZ;

    /// <Summary>
    /// If the target moves, the camera should child the target to allow for smoother movement. DR
    /// </Summary>
    private void Awake()
    {
        cameraZ.transform.parent = target;
    }

    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody>()) {
            //GetComponent<Rigidbody>.freezeRotation;
        }
        Vector3 wantedPosition = target.TransformPoint(0, height, -distance);

        // check to see if there is anything behind the target
        RaycastHit hit;
        
        Vector3 back = target.transform.TransformDirection(-1 * Vector3.forward);

        // cast the bumper ray out from rear and check to see if there is anything behind
        if (Physics.Raycast(target.TransformPoint(bumperRayOffset), back, out hit, bumperDistanceCheck)
            && hit.transform != target) // ignore ray-casts that hit the user. DR
            Debug.Log(hit);
        {
            // clamp wanted position to hit position
            wantedPosition.x = Mathf.Lerp(hit.point.x + bumperCameraHeight, wantedPosition.x, Time.deltaTime * damping);
            wantedPosition.z = Mathf.Lerp(hit.point.z + bumperCameraHeight, wantedPosition.z, Time.deltaTime * damping);
            wantedPosition.y = Mathf.Lerp(hit.point.y + bumperCameraHeight, wantedPosition.y, Time.deltaTime * damping);
        }

        cameraZ.transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        Vector3 lookPosition = target.TransformPoint(targetLookAtOffset);

        if (smoothRotation)
        {
            Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
            cameraZ.transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        }
        else
            cameraZ.transform.rotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
    }
}
