using System;
using UnityEngine;

[RequireComponent(typeof(OrbitFlight))]
public class SwordController : MonoBehaviour
{
    public GameObject Sword;
    public Vector2 Center = Vector2.zero;
    public int Level => swordUpgrader.CurrentLevel;

    private OrbitFlight swordOrbit;
    private OrbitFlight localSwordOrbit;
    private SwordUpgrader swordUpgrader;
    private GameObject currentSword;

    protected virtual void Awake()
    {
        try
        {
            Sword = GetSword();
        }
        catch(ArgumentNullException ex)
        {
            Debug.Log(ex.Message + "\nDefault sword was load.");
        }
        swordUpgrader = GetComponentInParent<SwordUpgrader>();
        Initialize();
    }

    private GameObject GetSword()
    {
        var swordContainer = GameObject.FindGameObjectWithTag("SwordContainer");
        if(swordContainer == null)
            throw new ArgumentNullException("Sword container not found.");

        var sword = swordContainer.GetComponent<SelectedSwordContainer>();
        Destroy(swordContainer);
        return sword.SelectedSword;
    }

    protected virtual void Initialize()
    {
        currentSword = Instantiate(Sword, Vector2.zero, Quaternion.identity, gameObject.transform); 
        var orbitFlightComponents = GetComponentsInChildren<OrbitFlight>();
        if (orbitFlightComponents.Length == 0)
            throw new MissingComponentException("Orbit flight components not found in this gameobject and in it's childrens.");

        swordOrbit = orbitFlightComponents[0];
        swordOrbit.OrbitStats = currentSword.GetComponent<SwordCharacteristics>().SwordStats;

        var startPosition = new Vector2(Center.x, swordOrbit.OrbitStats.Radius);
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = Quaternion.identity;

        if (orbitFlightComponents.Length > 1)
            localSwordOrbit = orbitFlightComponents[1];
        else
            localSwordOrbit = null;
    }

    protected virtual void Update()
    {
        swordOrbit.OrbitMove(Center, InputSystem.HorizontalAxis);

        if (localSwordOrbit != null)
            localSwordOrbit.OrbitMove(transform.position, 1f);
    }

    public virtual bool Upgrade()
    {
        if (!swordUpgrader.CanUpgrade(swordOrbit))
            return false;

        if (localSwordOrbit != null && !swordUpgrader.CanUpgrade(localSwordOrbit))
            return false;

        swordUpgrader.UpgradeOrbit(swordOrbit);
        if(localSwordOrbit != null)
            swordUpgrader.UpgradeOrbit(localSwordOrbit);

        return true;
    }

    public void ChangeSword()
    {
        Destroy(currentSword);
        Initialize();        
    }
}