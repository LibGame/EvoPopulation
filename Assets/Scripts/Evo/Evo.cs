using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evo : EvoSettigns, IEvo
{
    public int ID;

    private Vector2[] _direction = new Vector2[4]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    [SerializeField] private float _jumpMinDistance;
    [SerializeField] private float _jumpMaxDistance;

    [SerializeField] private float _angleJumpX;
    [SerializeField] private float _angleJumpY;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _reproduceMark;
    [SerializeField] private GameObject _prefab;
    public GameObject[] Skills;
    public bool IsCanReproduce;
    public bool IsMainReproduceEvo;
    public bool IsAtack;
    public bool IsDeath;
    public GameObject ReproduceEvoTarget;
    public GameObject EvoAtackTarget;
    [SerializeField] private GameObject _selfPrefab;
    [SerializeField] private EvoForm _evoForm;
    [SerializeField] private LayerMask _island;
    [SerializeField] private LayerMask _uiLayer;
    [SerializeField] private Color _colorInCamuflage;
    [SerializeField] private Color _colorDefault;
    [SerializeField] private GameObject _bloomEffect;
    public bool IsComuflage;
    private Rigidbody2D _rigidBody;
    [SerializeField] private Behaviour _behaviour = Behaviour.Move;
    public EvoCompany EvoType;
    private LayerMask Layer;

    public EvoIsland evoIsland;
    private Dictionary<string, EvoIsland> _evoIslands = new Dictionary<string, EvoIsland>
    {
        ["Island1"] = EvoIsland.First,
        ["Island2"] = EvoIsland.Second,
        ["Island3"] = EvoIsland.Third,

    };

    private Coroutine _jumpReproduceCoroutine;
    private Coroutine _jumpToPosition;
    private Coroutine _increseSizeCoroutine;
    private Coroutine _atackCoroutine;
    private Vector3 offset;

    private void Start()
    {
        Layer = ~LayerMask.NameToLayer("Evo");
        _rigidBody = GetComponent<Rigidbody2D>();
        _jumpToIncrese = Random.Range(7, 30);
        _reproduceJumpCount = Random.Range(8, 20);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector3.forward, Mathf.Infinity, _island);

        if (hit.transform != null)
        {
            evoIsland = _evoIslands[hit.transform.tag];
        }
        StartCoroutine(LifeLoop());
        _behaviour = Behaviour.Move;
    }

    public void UpgradePerLevel(int lvl)
    {
        _damage *= (_incriseParamFactor * lvl);
        _moveSpeed *= (_incriseParamFactor * (lvl -(lvl/4)));
        _reproduceJumpCount *= (int)(_incriseParamFactor * lvl);
        _health *= (_incriseParamFactor * lvl);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Radius>())
        {
            _bloomEffect.SetActive(true);
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Radius>())
        {
            _bloomEffect.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (_behaviour != Behaviour.Reproduce && EvoType == EvoCompany.Blue && !GameManager.Instance.IsEndGame)
        {
            if (_jumpToPosition != null)
                StopCoroutine(_jumpToPosition);
            if (_jumpReproduceCoroutine != null)
                StopCoroutine(_jumpReproduceCoroutine);
            if (_atackCoroutine != null)
                StopCoroutine(_atackCoroutine);

            if(ReproduceEvoTarget != null)
            {
                ReproduceEvoTarget.GetComponent<Evo>().ReproduceEvoTarget = null;
                ReproduceEvoTarget.GetComponent<Evo>().StopReproduce();
                ReproduceEvoTarget = null;
            }

            GameManager.Instance.IsSelectEvo = true;
            _behaviour = Behaviour.Selected;
            _animator.SetBool("IsJump", false);   

            offset = gameObject.transform.position -
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        }

    }

    public void StopReproduce()
    {
        if (_jumpToPosition != null)
            StopCoroutine(_jumpToPosition);
        if (_jumpReproduceCoroutine != null)
            StopCoroutine(_jumpReproduceCoroutine);
        if (_atackCoroutine != null)
            StopCoroutine(_atackCoroutine);
        _behaviour = Behaviour.Move;

    }

    public void CreateEvoVirus()
    {
        if (!IsDeath)
        {
            var evo = GameManager.Instance.CallToCreateEvo(transform.position, EvoCompany.Blue , ID);
            if (_jumpToPosition != null)
                StopCoroutine(_jumpToPosition);
            if (_jumpReproduceCoroutine != null)
            {
                StopCoroutine(_jumpReproduceCoroutine);
            }
            if (_atackCoroutine != null)
                StopCoroutine(_atackCoroutine);

            if (evo != null)
                evo.EvoSetParams(transform.localScale, _damage, _health);
            Death();
        }
    }

    public void EvoSetParams(Vector3 size, float damage, float health)
    {
        transform.localScale = size;
        _damage = damage;
        _health = health;
    }

    void OnMouseDrag()
    {
        if (_behaviour != Behaviour.Reproduce && EvoType == EvoCompany.Blue && !GameManager.Instance.IsEndGame)
        {
            if (_jumpToPosition != null)
                StopCoroutine(_jumpToPosition);
            if (_jumpReproduceCoroutine != null)
                StopCoroutine(_jumpReproduceCoroutine);
            if (_atackCoroutine != null)
                StopCoroutine(_atackCoroutine);

            if (ReproduceEvoTarget != null)
            {
                ReproduceEvoTarget.GetComponent<Evo>().ReproduceEvoTarget = null;
                ReproduceEvoTarget.GetComponent<Evo>().StopReproduce();
                ReproduceEvoTarget = null;
            }

            GameManager.Instance.IsSelectEvo = true;
            _behaviour = Behaviour.Selected;
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
    }

    void OnMouseUp()
    {
        if (_behaviour != Behaviour.Reproduce && EvoType == EvoCompany.Blue && !GameManager.Instance.IsEndGame)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit;
            GameManager.Instance.IsSelectEvo = false;

            hit = Physics2D.Raycast(pos, Vector3.forward, Mathf.Infinity, _island);
            Debug.Log(hit.transform);
            if (hit.transform != null)
            {
                GameManager.Instance.IsSelectEvo = false;
                _behaviour = Behaviour.Move;
                evoIsland = _evoIslands[hit.transform.tag];
            }
            else
            {
                Death();
            }
        }
    }

    public void InCamuflage()
    {
        IsComuflage = true;
        GetComponent<SpriteRenderer>().color = _colorInCamuflage;
        Invoke(nameof(OutCamuflage), 5f);
    }

    public void OutCamuflage()
    {
        GetComponent<SpriteRenderer>().color = _colorDefault;
        IsComuflage = false;
    }

    public void Upgrade()
    {
        if (transform.localScale.x <= _maxSize.x)
        {
            _damage *= _increaseFactor;
            _health *= _increaseFactor;
            Vector3 size = new Vector3(transform.localScale.x * _increaseFactor, transform.localScale.y * _increaseFactor, 1);
            if (_jumpToPosition != null)
                StopCoroutine(_jumpToPosition);
            if (_jumpReproduceCoroutine != null)
                StopCoroutine(_jumpReproduceCoroutine);
            _increseSizeCoroutine = StartCoroutine(UpgradeLoop(size));
        }

    }

    public bool Atack()
    {
        if (!IsComuflage && !IsDeath)
        {
            var evo = GameManager.Instance.FindEnemy(gameObject, EvoType , evoIsland);

            if (evo != null)
            {
                if (_jumpToPosition != null)
                    StopCoroutine(_jumpToPosition);
                if (_jumpReproduceCoroutine != null)
                    StopCoroutine(_jumpReproduceCoroutine);
                _behaviour = Behaviour.Atack;
                EvoAtackTarget = evo;
                _atackCoroutine = StartCoroutine(MoveToAtack());
                return true;
            }
        }
        return false;
    }

    private IEnumerator LifeLoop()
    {
        int amountMove = 0;
        Vector3 pervousPos = Vector3.zero ;

        while (!GameManager.Instance.IsEndGame && !IsDeath)
        {


            if (_behaviour == Behaviour.Move && ReproduceEvoTarget == null && EvoAtackTarget == null)
            {
                _jumpAmount++;
                if (!Atack())
                {
                    if (_jumpAmount % _reproduceJumpCount == 0 || IsCanReproduce)
                    {
                        IsCanReproduce = true;
                        _reproduceMark.SetActive(true);
                        CheckToReproduce();
                    }
                    if (_jumpAmount % _jumpToIncrese == 0 &&
                        transform.localScale.x < _maxSize.x &&
                        transform.localScale.y < _maxSize.y)
                    {
                        Upgrade();
                        _behaviour = Behaviour.Upgrade;
                    }
                    else
                    {
                        CheckToMove(_moveSpeed, 0, transform.position);
                    }
                }

            }
            if(pervousPos == transform.position && amountMove >= 4)
            {
                EvoAtackTarget = null;
                if (_jumpToPosition != null)
                    StopCoroutine(_jumpToPosition);
                if (_jumpReproduceCoroutine != null)
                    StopCoroutine(_jumpReproduceCoroutine);
                if (_atackCoroutine != null)
                    StopCoroutine(_atackCoroutine);

                if (ReproduceEvoTarget != null)
                {
                    ReproduceEvoTarget.GetComponent<Evo>().ReproduceEvoTarget = null;
                    ReproduceEvoTarget.GetComponent<Evo>().StopReproduce();
                    ReproduceEvoTarget = null;
                }
                _behaviour = Behaviour.Move;
                amountMove = 0;
            }
            else
            {
                pervousPos = transform.position;
                amountMove++;
            }

            yield return new WaitForSeconds(_jumpTimeRate);
        }

        StopAllCoroutines();
    }

    private IEnumerator UpgradeLoop(Vector3 size)
    {
        while (true)
        {
            float distance = Vector3.Distance(transform.localScale, size);
            transform.localScale = Vector3.MoveTowards(transform.localScale, size, Time.deltaTime);
            if (distance <= 0.01f)
            {
                _behaviour = Behaviour.Move;
                break;
            }

            yield return null;
        }
    }

    public bool CheckToMove(float speed, float index, Vector3 p)
    {
        float radius = Random.Range(_jumpMinDistance + index, _jumpMaxDistance + index);
        float angle = Random.Range(_angleJumpX, _angleJumpY);
        var x = Mathf.Cos(angle * 5) * radius;
        var y = Mathf.Sin(angle * 5) * radius;
        Vector3 pos = new Vector3(x, y, 0) + p;
        if (CheckToPos(pos))
        {
            StartMove(pos, speed);
            return true;
        }
        return false;
    }

    public void StartMove(Vector3 pos, float speed)
    {
        if(ReproduceEvoTarget == null)
        {
            if (_jumpToPosition != null)
                StopCoroutine(_jumpToPosition);
            if (_jumpReproduceCoroutine != null)
                StopCoroutine(_jumpReproduceCoroutine);
            if (_atackCoroutine != null)
                StopCoroutine(_atackCoroutine);
            _jumpToPosition = StartCoroutine(JumpToPosition(pos, speed));
        }
    }

    public bool CheckToPos(Vector3 pos)
    {
        int layerMaskOnlyPlayer = 1 << 9;
        // получаем маску, которая затрагивает все слои, кроме слоя Player
        int layerMaskWithoutPlayer = ~layerMaskOnlyPlayer;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(pos, Vector3.forward, Mathf.Infinity, layerMaskWithoutPlayer);
        // получаем маску, которая затрагивает только слой Player
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.layer != _uiLayer && hit.transform.gameObject.layer == 8)
                return true;
        }
        return false;
    }

    public void Death()
    {
        if (!IsDeath)
        {
            GameManager.Instance.RemoveEvo(gameObject.GetComponent<Evo>());
            _animator.SetBool("IsJump", false);
            _animator.SetBool("IsDeath", true);
            if (_jumpToPosition != null)
                StopCoroutine(_jumpToPosition);
            if (_jumpReproduceCoroutine != null)
                StopCoroutine(_jumpReproduceCoroutine);
            if (_atackCoroutine != null)
                StopCoroutine(_atackCoroutine);
            IsDeath = true;
            if (ReproduceEvoTarget != null)
            {                
                ReproduceEvoTarget.GetComponent<Evo>().ReproduceEvoTarget = null;
                ReproduceEvoTarget.GetComponent<Evo>().StopReproduce();
                ReproduceEvoTarget = null;
            }
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        _evoForm.DestroyFrom();
        Destroy(gameObject);
    }

    public bool GetDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Death();
            return true;
        }
        return false;
    }

    private IEnumerator MoveToAtack()
    {
        float nextTime = 0.0F;
        float timeRate = 0.3f;

        while (EvoAtackTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                EvoAtackTarget.transform.position, _moveReproduceSpeed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, EvoAtackTarget.transform.position);

            if (distance >= 6)
            {
                EvoAtackTarget = null;
                IsAtack = false;
                _behaviour = Behaviour.Move;
                break;
            }

            if (distance <= 0.01f && Time.time > nextTime)
            {
                if (EvoAtackTarget.GetComponent<Evo>().GetDamage(_damage))
                {
                    EvoAtackTarget = null;
                    IsAtack = false;
                    _behaviour = Behaviour.Move;
                    Upgrade();
                    break;
                }
                nextTime = Time.time + timeRate;
            }

            yield return null;
        }
        _behaviour = Behaviour.Move;
    }

    public void FindCloseEvo()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
    }

    public void CheckToReproduce()
    {

        if (!IsDeath)
        {

            var evo = GameManager.Instance.Reproduce(gameObject, ChosseMassive());
            if (evo != null)
            {
                if (_jumpToPosition != null)
                    StopCoroutine(_jumpToPosition);
                if (_increseSizeCoroutine != null)
                    StopCoroutine(_increseSizeCoroutine);

                Evo evoTarget = evo.GetComponent<Evo>();
                evoTarget.IsPrivateToReproduce(gameObject);
                ReproduceEvoTarget = evo;
                IsCanReproduce = false;
                IsMainReproduceEvo = true;
                _behaviour = Behaviour.MoveToReproduce;
                MoveToReproduce();
            }
        }

    }

    public List<Evo> ChosseMassive()
    {
        if (EvoType == EvoCompany.Red)
        {
            return GameManager.Instance.RedEvos;
        }
        else if (EvoType == EvoCompany.Blue)
        {
            return GameManager.Instance.BlueEvos;
        }
        else
        {
            return GameManager.Instance.YellowEvos;
        }
    }

    public void IsPrivateToReproduce(GameObject evo)
    {
        _behaviour = Behaviour.MoveToReproduce;
        if (_jumpToPosition != null)
            StopCoroutine(_jumpToPosition);
        if (_increseSizeCoroutine != null)
            StopCoroutine(_increseSizeCoroutine);
        IsCanReproduce = false;
        IsMainReproduceEvo = false;
        ReproduceEvoTarget = evo;
        MoveToReproduce();
    }

    public void MoveToReproduce()
    {

        if (_jumpToPosition != null)
            StopCoroutine(_jumpToPosition);
        if (_increseSizeCoroutine != null)
            StopCoroutine(_increseSizeCoroutine);
        _jumpReproduceCoroutine = StartCoroutine(JumpToReproduce());
    }

    private IEnumerator JumpToReproduce()
    {
        _animator.SetBool("IsJump", true);
        _animator.SetBool("IsDeath", false);
        if (_jumpToPosition != null)
            StopCoroutine(_jumpToPosition);
        if (_increseSizeCoroutine != null)
            StopCoroutine(_increseSizeCoroutine);
        int i = 0;

        while (ReproduceEvoTarget != null)
        {
            i++;
            transform.position = Vector3.MoveTowards(transform.position,
                ReproduceEvoTarget.transform.position, Time.deltaTime * _moveReproduceSpeed);
            float distance = Vector3.Distance(transform.position, ReproduceEvoTarget.transform.position);
            if (distance <= 0.1f)
            {
                StartReproduce();
                break;
            }

            yield return null;
        }
    }

    private IEnumerator JumpToPosition(Vector3 target, float speed)
    {
        _behaviour = Behaviour.MoveToPosition;
        _animator.SetBool("IsJump", true);
        _animator.SetBool("IsDeath", false);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                target, speed * Time.deltaTime);

            //float distance = Vector3.Distance(transform.position, target);

            if (transform.position.Equals(target))
            {
                _behaviour = Behaviour.Move;
                _animator.SetBool("IsJump", false);
                _animator.SetBool("IsDeath", false);

                break;
            }

            yield return null;
        }
    }

    private Vector3 CalculateReproducePosition(Vector3 pos1, Vector3 pos2)
    {
        return new Vector3((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2);
    }

    public void Reproduce()
    {
        _animator.SetBool("IsJump", false);
        _animator.SetBool("IsDeath", false);
        Invoke(nameof(EndReproduce), 2f);
    }

    public void EndReproduce()
    {
        if (IsMainReproduceEvo)
        {
            GameManager.Instance.CallToCreateEvo(transform.position, EvoType , ID);

        }
        ReproduceEvoTarget = null;
        IsMainReproduceEvo = false;
        IsCanReproduce = false;
        _reproduceMark.SetActive(false);
        _behaviour = Behaviour.Move;
    }


    public void CreateEvoSkill()
    {
        GameManager.Instance.CallToCreateEvo(transform.position, EvoType , ID);
    }

    private void StartReproduce()
    {
        _behaviour = Behaviour.Reproduce;
        if (_jumpToPosition != null)
            StopCoroutine(_jumpToPosition);
        if (_increseSizeCoroutine != null)
            StopCoroutine(_increseSizeCoroutine);
        Reproduce();
    }
}
