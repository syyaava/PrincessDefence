using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusSwordsController : MonoBehaviour
{
    public int MaxSwords = 6;    
    public GameObject BonusSwordPrefab;
    public int GoldUpgradeCost = 100;
    public float RotationSpeed = 8f;
    public float Radius = 4f;

    private Resource Cost;
    private List<GameObject> swords = new List<GameObject>();
    private List<GameObject> swordPositions = new List<GameObject>();

    private void Start()
    {
        swords = new List<GameObject>();
        Cost = new Resource()
        {
            Type = Resource.ResourceType.Gold,
            Count = GoldUpgradeCost
        };
        SetSwordsPositionPoints();
    }

    private void SetSwordsPositionPoints()
    {
        swordPositions = new List<GameObject>();
        var deltaAngle = 360f / MaxSwords;
        for (var i = 0; i < MaxSwords; i++)
        {
            var newPosition = new GameObject();
            newPosition.transform.SetPositionAndRotation(new Vector3(Radius * Mathf.Cos(Mathf.Deg2Rad * (i * deltaAngle)),
                Radius * Mathf.Sin(Mathf.Deg2Rad * (i * deltaAngle))), Quaternion.Euler(0, 0, i * deltaAngle));
            newPosition.transform.SetParent(transform);

            swordPositions.Add(newPosition);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f), RotationSpeed * Time.deltaTime);
    }

    public void Upgrade()
    {
        if (!ResourceController.Instance.RemoveResources(Cost))
            return;

        Cost.Count = (int)(Cost.Count * 1.5f);
        if (swords.Count == 0)
        {
            var position = swordPositions[0];
            var newSword = Instantiate(BonusSwordPrefab, position.transform);

            swords.Add(newSword);
            return;
        }

        if(swords.Count < MaxSwords)
        {            
            var position = swordPositions[swords.Count];
            var newSword = Instantiate(BonusSwordPrefab, position.transform);
            newSword.transform.SetPositionAndRotation(position.transform.position,
                position.transform.rotation);

            swords.Add(newSword);
            return;
        }

        RotationSpeed += 1f;
        foreach(var sword in swords)
        {
            var damageDealer = sword.GetComponent<DamageDealer>();
            damageDealer.Damage++;
        }
    }
}
