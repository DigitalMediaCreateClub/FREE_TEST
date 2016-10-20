using UnityEngine;

/// <summary>
/// GameビューにてSceneビューのようなカメラの動きをマウス操作によって実現する
/// </summary>
[RequireComponent(typeof(Camera))]
public class SceneViewCamera : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)]
    private float wheelSpeed = 1f;

    [SerializeField, Range(0.1f, 10f)]
    private float moveSpeed = 0.3f;

    [SerializeField, Range(0.1f, 10f)]
    private float rotateSpeed = 0.3f;

    [SerializeField, Range(0.1f, 10f)]
    private float parentRotateSpeed = 5f;

    public Vector3 defPositon;
    public Quaternion defRotation;
    public Vector3 defPositonPatent;
    public Quaternion defRotationParent;

    private Vector3 preMousePos;

    void Start()
    {
        defPositon = transform.position;
        defRotation = transform.rotation;
        defPositonPatent = transform.parent.transform.position;
        defRotationParent = transform.parent.transform.rotation;
    }

    private void Update()
    {
        MouseUpdate();
        return;
    }

    private void MouseUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0f)
            MouseWheel(scrollWheel);

        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
            preMousePos = Input.mousePosition;
        else if(Input.GetKeyDown(KeyCode.X))
        {
            transform.parent.transform.position = defPositonPatent;
            transform.parent.transform.rotation = defRotationParent;
            transform.position = defPositon;
            transform.rotation= defRotation;
        }

        MouseDrag(Input.mousePosition);
    }

    private void MouseWheel(float delta)
    {
        transform.position += transform.forward * delta * wheelSpeed;
        return;
    }

    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - preMousePos;

        if (diff.magnitude < Vector3.kEpsilon)
            return;

        if (Input.GetMouseButton(2))
        {
            transform.Translate(diff * Time.deltaTime * moveSpeed);Debug.Log(transform.position);
        }
        else if (Input.GetMouseButton(1))
            CameraRotate(new Vector2(-diff.y, diff.x) * rotateSpeed);
        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.Z))
        {
           
            diff.Set(-diff.y, -diff.x, -diff.z);
            transform.parent.transform.Rotate(-diff * Time.deltaTime * parentRotateSpeed);
        }

        preMousePos = mousePos;
    }

    public void CameraRotate(Vector2 angle)
    {
        transform.RotateAround(transform.position, transform.right, angle.x);
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }
}