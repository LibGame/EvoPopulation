using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetter : MonoBehaviour
{
    public GameObject ExplosionSkillPrefab;
    public GameObject IncreseSkillPrefab;
    public GameObject MagniteSkillPrefab;
    public GameObject ReproduceSkillPrefab;
    public GameObject VirusSkillPrefab;
    public GameObject ComuflagePrefab;

    public Text StaminaCostText;
    public Image Icon;
    public Radius Radius;

    public void SetSkill(GameObject skill)
    {
        if(skill == ExplosionSkillPrefab)
        {
            SetParams<ExplosionSkill>(ExplosionSkillPrefab);
        }
        else if (skill == IncreseSkillPrefab)
        {
            SetParams<IncreseSkill>(IncreseSkillPrefab);
        }
        else if (skill == MagniteSkillPrefab)
        {
            SetParams<MagniteSkill>(MagniteSkillPrefab);
        }
        else if (skill == ReproduceSkillPrefab)
        {
            SetParams<ReproduceSkill>(ReproduceSkillPrefab);
        }
        else if (skill == VirusSkillPrefab)
        {
            gameObject.AddComponent(typeof(VirusSkill));
            var skl = GetComponent(typeof(VirusSkill)) as SkillSetting;
            var prefSkill = VirusSkillPrefab.GetComponent(typeof(VirusSkill)) as SkillSetting;
            Icon.sprite = VirusSkillPrefab.GetComponent<Image>().sprite;
            StaminaCostText.text = prefSkill.StaminaCost.ToString();
            skl.StaminaCost = prefSkill.StaminaCost;
            skl._radiusArea = prefSkill._radiusArea;
            skl._radiusSize = prefSkill._radiusSize;
            skl._targetType = prefSkill._targetType;
            GetComponent<VirusSkill>()._targetType1 = VirusSkillPrefab.GetComponent<VirusSkill>()._targetType1;
            skl._radiusArea = Radius;
        }
        else if (skill == ComuflagePrefab)
        {
            SetParams<Comuflage>(ComuflagePrefab);
        }
    }


    private void SetParams<T>(GameObject prefab)
    {
        gameObject.AddComponent(typeof(T));
        var skl = GetComponent(typeof(T)) as SkillSetting;
        var prefSkill = prefab.GetComponent(typeof(T)) as SkillSetting;
        Icon.sprite = prefab.GetComponent<Image>().sprite;
        StaminaCostText.text = prefSkill.StaminaCost.ToString();
        skl.StaminaCost = prefSkill.StaminaCost;
        skl._radiusArea = prefSkill._radiusArea;
        skl._radiusSize = prefSkill._radiusSize;
        skl._targetType = prefSkill._targetType;
        skl._radiusArea = Radius;
    }

}
