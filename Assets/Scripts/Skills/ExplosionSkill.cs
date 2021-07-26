using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : SkillSetting
{
    public override void Start()
    {
        base.Start();
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance.IsEndGame)
            SelectSkill();
    }

    public override void SelectSkill()
    {
        _radiusArea._skill = GetComponent<ExplosionSkill>();
        base.SelectSkill();
    }

    public override void UseSkill(Collider2D[] _evos)
    {
        if (GlobalSettings.Instance.InterfaceSettings.Stamina >= StaminaCost)
        {
            GlobalSettings.Instance.InterfaceSettings.Stamina -= StaminaCost;

            for (int i = 0; i < _evos.Length; i++)
            {

                Evo evo = _evos[i].gameObject.GetComponent<Evo>() as Evo;

                if (evo.EvoType == _targetType)
                {
                    int k = 0;
                    while (evo.CheckToMove(6, 1, _radiusArea.transform.position))
                    {
                        k++;
                        if (k >= 100)
                            break;
                    }
                }
            }
            _radiusArea.gameObject.SetActive(false);
            GameManager.Instance.IsUseSkill = false;
        }
    }
}
