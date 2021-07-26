using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoSettigns : MonoBehaviour
{
    [SerializeField] protected float _jumpTimeRate;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _health;
    [SerializeField] protected float _moveReproduceSpeed;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _jumpToIncrese;
    [SerializeField] protected float _increaseFactor; // Множитель роста 
    [SerializeField] protected int _reproduceJumpCount; // Может размножаться через N прыжков
    [SerializeField] protected float _incriseParamFactor; // Это множитель увелечения параметров с левелом
    [SerializeField] protected Vector3 _maxSize;
    protected int _jumpAmount = 0;

    public enum Behaviour { Move, MoveToPosition , MoveToReproduce , Atack , Reproduce, Selected , Upgrade}
    public enum EvoCompany { Red , Blue , Yellow}
    public enum EvoIsland { First, Second, Third }
}
