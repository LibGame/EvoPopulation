using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool startBool = true;

    private float zoomMin = 6;
    private float zoomMax = 12;
    Vector3 hit_position = Vector3.zero;
    Vector3 current_position = Vector3.zero;
    Vector3 camera_position = Vector3.zero;
    public float Speed;
    [SerializeField] private float _xMin;
    [SerializeField] private float _xMax;
    [SerializeField] private float _yMin;
    [SerializeField] private float _yMax;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float Inertia = 0.9f;
    public float Scroll = 1 / 20f;
    float screenWidth;
    float screenHeight;
    Vector3 lastPosition;
    Vector3 velocity;
    float z = 0.0f;
    public Text t;
    Vector3 _direction;
    Vector3 _lastPosition;
    public float Sensitivity;
    private Vector3 touch;
    public Text Text;
    private Vector3 oldPosition;

    private void Start()
    {
        transform.position = start.position;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
      
        if (startBool == true)
        {
            transform.position = Vector3.Lerp(transform.position, end.position, 0.01f);
            if (Vector3.Distance(transform.position, end.position) < 0.1f)
            {
                startBool = false;
            }
        }

        if (!GameManager.Instance.IsSelectEvo && !GameManager.Instance.IsUseSkill && !GameManager.Instance.IsEndGame)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit;

            hit = Physics2D.Raycast(pos, Vector3.forward);

            if (hit.transform)
            {
                if (hit.transform.GetComponent<Island>() && hit.transform.gameObject.layer != _mask)
                {
                    Controll();
                }
            }
            else
            {
                Controll();
            }
        }else if (GameManager.Instance.IsSelectEvo)
        {
            MoveCameraByMouseDirection();

        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _xMin, _xMax), Mathf.Clamp(transform.position.y, _yMin, _yMax), transform.position.z);

    }


    public void ControllCamera()
    {
        Vector3 delta;

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchZeroOnePos = touchOne.position - touchOne.deltaPosition;

            float distTouch = (touchZeroLastPos - touchZeroOnePos).magnitude;
            float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistTouch - distTouch;

            Zoom(difference * 0.01f);
        }
        //if (Input.GetMouseButton(0))
        //{
        //    velocity = delta = lastPosition - Input.mousePosition;
        //}
        //else
        //{
        //    velocity *= Inertia;
        //    delta = velocity;
        //}

        //move camera
        //transform.Translate(new Vector3(delta.x, delta.y, 0) * Sensitivity * Time.deltaTime, Space.World);
        lastPosition = Input.mousePosition;
    }


    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomMin, zoomMax);
    }

    public void MoveCameraByMouseDirection()
    {

        if (Input.mousePosition.x <= 200)
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }
        if (Input.mousePosition.x >= screenWidth - 200)
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }
        if (Input.mousePosition.y <= 200)
        {
            transform.Translate(Vector3.down * Time.deltaTime * Speed);
        }
        if (Input.mousePosition.y >= screenHeight - 200)
        {
            transform.Translate(Vector3.up * Time.deltaTime * Speed);
        }
    }

    void Controll()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _direction = Vector3.zero;
            velocity = Vector3.zero;
            camera_position = transform.position;
            hit_position = Input.mousePosition;
            //Sensitivity = 2;
            oldPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Sensitivity = ((Input.mousePosition.x - oldPosition.x) + (Input.mousePosition.y - oldPosition.y)) / Time.deltaTime;
            Sensitivity /= 20000;
            Sensitivity = Mathf.Abs(Sensitivity);

        }

        if (Input.GetMouseButton(0))
        {
            current_position = Input.mousePosition;
            LeftMouseDrag();
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchZeroOnePos = touchOne.position - touchOne.deltaPosition;

            float distTouch = (touchZeroLastPos - touchZeroOnePos).magnitude;
            float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistTouch - distTouch;

            Zoom(difference * 0.01f);
        }
        //else if (Input.GetMouseButton(0))
        //{
        //    current_position = Input.mousePosition;
        //    LeftMouseDrag();
        //}
        //else
        //{
        //    //velocity = _direction * Inertia;
        //    //transform.Translate(new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * Sensitivity);
        //    //Sensitivity = Mathf.Lerp(Sensitivity, 0, Time.deltaTime * 2);
        //}

    }

    void LeftMouseDrag()
    {
        // From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
        // with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
        current_position.z = hit_position.z = camera_position.y;

        // Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
        // anyways.  
        Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);
        // Invert direction to that terrain appears to move with the mouse.
        direction = direction * -1;
        _direction = direction;
        Vector3 position = camera_position + direction;

        //position.x = Mathf.Clamp(position.x, _xMin, _xMax);
        //position.y = Mathf.Clamp(position.y, _yMin, _yMax);

        transform.position = position;
    }
}
