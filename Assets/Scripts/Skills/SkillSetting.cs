using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillSetting : MonoBehaviour
{
    public EvoSettigns.EvoCompany _targetType;
    public Radius _radiusArea;
    public Vector3 _radiusSize;
    public int StaminaCost;
    public Text StaminaCostText;
    public Image Icon;
    public SkillSetting _firstSkill;

    public virtual void Start()
    {
    }

    public virtual void SetSkill(object s)
    {
    }

    public virtual void SelectSkill()
    {
        SoundController.Instance.PlayButtonClick();
        GameManager.Instance.IsUseSkill = true;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _radiusArea.transform.position = pos;
        _radiusArea.RadiusSize(_radiusSize);
        _radiusArea.gameObject.SetActive(transform);
    }

    public virtual void UseSkill(Collider2D[] _evos)
    {

    }

}
