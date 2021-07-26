using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvo
{
    void Move(Vector3 position);

    bool Atack();

    void Death();

    bool CheckToMove(float speed, float index , Vector3 p);

    void Reproduce();

    void FindCloseEvo();

    void CheckToReproduce();

    void MoveToReproduce();

    void EndReproduce();

    void IsPrivateToReproduce(GameObject evo);

    void Upgrade();
}
