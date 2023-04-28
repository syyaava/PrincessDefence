using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject ThrowingObj;
    public Transform ThrowPoint;
    public float ThrowDistance = 5f;
    public float ThrowCheckDelay = 0.2f;

    private EnemyMover mover;

    void Start()
    {
        if (ThrowingObj == null)
            throw new ArgumentNullException($"{nameof(ThrowingObj)} is null.");

        mover = GetComponent<EnemyMover>();
        StartCoroutine(ExpectThrow());
    }

    private IEnumerator ExpectThrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(ThrowCheckDelay);
            if (Vector2.Distance(mover.Princess.position, transform.position) <= ThrowDistance)
                break;
        }

        Throw();
    }

    private void Throw()
    {
        var throwingObj = Instantiate(ThrowingObj, ThrowPoint.position, transform.rotation, transform);
        throwingObj.GetComponent<EnemyMover>().Princess = mover.Princess;
    }
}
