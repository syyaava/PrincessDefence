using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleOrbitFlight : OrbitFlight
{
    //public bool GoingThroughCollision = false;
    [SerializeField]
    private float angle = 90f;

    private void Start()
    {
        angle = 90f;
    }

    public override void OrbitMove(Vector2 center, float axis)
    {
        if (axis == 0) return;
        angle = (angle + axis * OrbitStats.Speed * Time.fixedDeltaTime) % 360;
        transform.localPosition = new Vector2(OrbitStats.Radius * Mathf.Cos(Mathf.Deg2Rad * angle), OrbitStats.Radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        transform.localRotation = GetRotation();
    }

    private Quaternion GetRotation()
    {
        return Quaternion.Euler(0f, 0f, angle - 90);
    }

    public override void Upgrade()
    {
        OrbitStats = OrbitStats.LevelUp();
    }
}
