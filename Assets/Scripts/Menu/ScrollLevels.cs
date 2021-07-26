using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLevels : MonoBehaviour
{
    public GameObject Case;
    private Vector3 screenPoint, offset;
    private float _lockedYPos = 2;
    private float endPos = -18f;
    public RectTransform rect;
    [SerializeField] private float _max;
    [SerializeField] private float _min;

    private void Start()
    {
        _lockedYPos = rect.transform.position.y;
    }

    public void SetCase(GameObject _case, float lastPos)
    {
        Case = _case;
        endPos = lastPos;
    }

    private void OnMouseDown()
    {
        offset = Case.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.y = _lockedYPos;
        curPosition.x = Mathf.Clamp(curPosition.x, _min, _max);
        Case.transform.position = curPosition;
    }
}
