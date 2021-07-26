using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Evo> _evos = new List<Evo>();
    public static GameManager Instance;
    [SerializeField] private GameObject _bluePrefab;
    [SerializeField] private GameObject _redPrefab;
    [SerializeField] private GameObject _yellowPrefab;

    public List<GameObject> AllEvos = new List<GameObject>();
    public Dictionary<int, GameObject> EvosByKey;

    public Transform[] FirstIsland;
    public Transform[] SecondIsland;
    public Transform[] ThreeIsland;

    [SerializeField] private Image _redIndicator;
    [SerializeField] private Image _blueIndicator;
    [SerializeField] private Image _yellowIndicator;

    [SerializeField] private Text _redText;
    [SerializeField] private Text _yellowText;
    [SerializeField] private Text _blueText;

    [SerializeField] private Text _redRusultText;
    [SerializeField] private Text _yellowRusultText;
    [SerializeField] private Text _blueRusultText;
    [SerializeField] private Text Result;
    [SerializeField] private GameObject _endGamePanel;

    [SerializeField] private float _maxAmountIndicatorPop;
    [SerializeField] private LayerMask _uiLayer;


    public List<Evo> RedEvos = new List<Evo>();
    public List<Evo> BlueEvos = new List<Evo>();
    public List<Evo> YellowEvos = new List<Evo>();

    private int _redAmount;
    private int _blueAmount;
    private int _yellowAmount;

    public int RedAmount { get { return _redAmount; } }
    public int BlueAmount { get { return _blueAmount; } }
    public int YellowAmount { get { return _yellowAmount; } }

    public bool IsSelectEvo;
    public bool IsUseSkill;
    public bool IsEndGame;

    public Evo Evos
    {
        set
        {
            _evos.Add(value);
        }
    }

    private void Awake()
    {
        Instance = GetComponent<GameManager>();
        EvosByKey = new Dictionary<int, GameObject>
        {
            [0] = AllEvos[0],
            [1] = AllEvos[1],
            [2] = AllEvos[2],
            [3] = AllEvos[3],
            [4] = AllEvos[4],
            [5] = AllEvos[5],
            [6] = AllEvos[6],
            [7] = AllEvos[7],

        };
    }


    public void UpgradeAllBlueEvosPerLvl(int lvl)
    {
        foreach(var evo in BlueEvos)
        {
            evo.UpgradePerLevel(lvl);
        }
    }

    public void EndGame()
    {
        if (!IsEndGame)
        {
            if(!PlayerPrefs.HasKey("Offer"))
                ShowAd();
            _endGamePanel.SetActive(true);
            _redRusultText.text = _redAmount.ToString();
            _blueRusultText.text = _blueAmount.ToString();
            _yellowRusultText.text = _yellowAmount.ToString();
            IsEndGame = true;

            Interface.Instance.SortingByPlace();
            StopAllCoroutines();
        }
  
    }


    public GameObject CreateEvo(Vector3 pos , GameObject pref , EvoSettigns.EvoCompany evoComp)
    {
        var evo = Instantiate(pref, pos, Quaternion.identity);
        var evofrom = evo.GetComponent<EvoForm>().EvoSource;
        evofrom.EvoType = evoComp;
        CheckEvoAddValue(evo.GetComponent<EvoForm>().EvoSource.EvoType, 1);
        _evos.Add(evo.GetComponent<EvoForm>().EvoSource);

        if (evofrom.EvoType == EvoSettigns.EvoCompany.Red)
        {
            RedEvos.Add(evofrom.GetComponent<Evo>());
        }
        else if (evofrom.EvoType == EvoSettigns.EvoCompany.Yellow)
        {
            YellowEvos.Add(evofrom.GetComponent<Evo>());
        }
        else
        {
            BlueEvos.Add(evofrom.GetComponent<Evo>());
        }
        return evofrom.gameObject;
    }

    public void UpdateIndicatorPopulation(Image img , float amount)
    {
        float fill = amount / _maxAmountIndicatorPop;
        img.fillAmount = fill;
    }

    public Evo CallToCreateEvo(Vector3 pos, EvoSettigns.EvoCompany type , int ID)
    {
        GameObject evo;
        GameObject prefab = EvosByKey[ID];
        evo = CreateEvo(pos, prefab, type);


        return evo.GetComponent<Evo>();
    }

    public void RemoveEvo(Evo evo)
    {
        CheckEvoAddValue(evo.EvoType, -1);
        _evos.Remove(evo);

        if(evo.EvoType == EvoSettigns.EvoCompany.Red)
        {
            RedEvos.Remove(evo);
        }else if (evo.EvoType == EvoSettigns.EvoCompany.Blue)
        {
            BlueEvos.Remove(evo);
        }
        else if (evo.EvoType == EvoSettigns.EvoCompany.Yellow)
        {
            YellowEvos.Remove(evo);
        }
    }

    public void CheckEvoAddValue(EvoSettigns.EvoCompany type, int index)
    {
        if(type == EvoSettigns.EvoCompany.Red)
        {
            _redAmount += index;
            _redText.text = _redAmount.ToString();
            UpdateIndicatorPopulation(_redIndicator, _redAmount);
        }
        else if (type == EvoSettigns.EvoCompany.Blue)
        {
            _blueAmount += index;
            _blueText.text = _blueAmount.ToString();
            UpdateIndicatorPopulation(_blueIndicator, _blueAmount);
        }
        else if (type == EvoSettigns.EvoCompany.Yellow)
        {
            _yellowAmount += index;
            _yellowText.text = _yellowAmount.ToString();
            UpdateIndicatorPopulation(_yellowIndicator, _yellowAmount);
        }
    }

    public void SpawnEvos(Transform[] pos,GameObject pref , EvoSettigns.EvoCompany evoComp)
    {
        int i = Random.Range(2, 6);
        for(int j = 0; j < i; j++)
        {
            Vector3 p = pos[Random.Range(0, pos.Length)].position;
            CreateEvo(new Vector3(p.x,p.y, pref.transform.position.z), pref, evoComp);
        }
    }


    public GameObject Reproduce(GameObject evo , List<Evo> evos)
    {
        for (int i = 0; i < evos.Count; i++)
        {
            if (evos[i].gameObject != evo)
            {
                float distance = Vector3.Distance(evos[i].transform.position, evo.transform.position);
                if (distance <= 5 && evos[i].IsCanReproduce
                    && evos[i].ReproduceEvoTarget == null && !evos[i].IsAtack && !evos[i].IsDeath)
                {
                    return evos[i].gameObject;
                }
            }
        }
        return null;
    }

    public GameObject FindEnemy(GameObject evo, EvoSettigns.EvoCompany type , EvoSettigns.EvoIsland island)
    {
        for (int i = 0; i < _evos.Count; i++)
        {
            if (!_evos[i].IsComuflage && _evos[i].EvoType != type && !_evos[i].IsDeath 
                && _evos[i].evoIsland == island)
            {
                if(evo != null && _evos[i] != null)
                {
                    float distance = Vector3.Distance(_evos[i].transform.position, evo.transform.position);
                    if (distance <= 4)
                    {
                        return _evos[i].gameObject;
                    }
                }
            }
        }
        return null;
    }



    private InterstitialAd interstitialAd;

#if UNITY_ANDROID
    private const string interstitialUnitId = "ca-app-pub-3940256099942544/8691691433"; //тестовый айди
#elif UNITY_IPHONE
    private const string interstitialUnitId = "";
#else
    private const string interstitialUnitId = "unexpected_platform";
#endif
    void OnEnable()
    {
        interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
}
