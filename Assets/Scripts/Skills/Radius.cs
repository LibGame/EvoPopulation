using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour
{
    [SerializeField] protected float _radius;
    [SerializeField] private LayerMask _layerMask;
    public SkillSetting _skill;
    private Vector3 offset;

    public void RadiusSize(Vector3 size)
    {
        transform.localScale = size;
        _radius = size.x * 2;
    }

    public void SelectEvos()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _layerMask);
        if (hitColliders.Length > 0)
        {
            _skill.UseSkill(hitColliders);
        }
        GameManager.Instance.IsUseSkill = false;

    }

    public void Update()
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);

        if (Input.GetMouseButtonUp(0))
        {
            SelectEvos();
            gameObject.SetActive(false);
        }
    }

}
