using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoSpawnControll : MonoBehaviour
{
    public GameObject[] EpicEvo;
    public GameObject[] LuckyEvo;
    public GameObject[] GoldEvo;
    public GameObject[] AllEvo;

    public void Start()
    {
        List<GameObject> Evos = new List<GameObject>(AllEvo);
        int evoID = 3;
        int lvl = 1;
        if (PlayerPrefs.HasKey("SelectedEvo"))
        {
            evoID = PlayerPrefs.GetInt("SelectedEvo");
            lvl = PlayerPrefs.GetInt("Evo" + evoID);
        }
        GameObject evo = Evos[evoID];
        Evos.RemoveAt(evoID);
        Interface.Instance.SetSkills(evo.GetComponent<EvoForm>().EvoSource.Skills);
        GameManager.Instance.SpawnEvos(GameManager.Instance.FirstIsland, evo, EvoSettigns.EvoCompany.Blue);
        GameManager.Instance.SpawnEvos(GameManager.Instance.SecondIsland, Evos[Random.Range(0, Evos.Count - 1)], EvoSettigns.EvoCompany.Red);
        GameManager.Instance.SpawnEvos(GameManager.Instance.ThreeIsland, Evos[Random.Range(0, Evos.Count - 1)], EvoSettigns.EvoCompany.Yellow);
        GameManager.Instance.UpgradeAllBlueEvosPerLvl(lvl);
        Interface.Instance.StartTimer();
    }
}
