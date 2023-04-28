using UnityEngine;

public abstract class OrbitFlight : MonoBehaviour
{    
    public OrbitFlightStats OrbitStats;
    public abstract void OrbitMove(Vector2 center, float axis);
    public abstract void Upgrade();
}
