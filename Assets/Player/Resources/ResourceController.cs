using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public static ResourceController Instance { get; private set; }
    public int StartResourcesCount = 0;
    [SerializeField]
    private List<Resource> resources = new List<Resource>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        var resources = Enum.GetValues(typeof(Resource.ResourceType)).Cast<Resource.ResourceType>();
        foreach (var resource in resources)
        {
            var newResource = new Resource()
            {
                Type = resource,
                Count = StartResourcesCount,
            };
            if (resource == Resource.ResourceType.Score)
                newResource.Count = 0;
            this.resources.Add(newResource);
        }
    }

    public void AddResources(params Resource[] resources)
    {
        if (resources == null || resources.Length == 0)
            return;

        foreach (var resource in resources)
        {
            var res = this.resources.FirstOrDefault(x => x.Type == resource.Type);
            if (res != null)
                res.Count += resource.Count;
        }

        var str = string.Join<Resource>(" ", resources);
    }

    public bool RemoveResources(params Resource[] resources)
    {
        if (resources == null || resources.Length == 0)
            return false;

        var haveResources = HaveResources(resources);
        if (!haveResources) return false;

        foreach (var resource in resources)
        {
            var res = this.resources.FirstOrDefault(x => x.Type == resource.Type);
            if (res != null)
            {
                res.Count -= resource.Count;
                if (res.Count < 0)
                    res.Count = 0;
            }
        }

        var str = string.Join<Resource>(" ", resources);
        return true;
    }

    public bool HaveResource(Resource resource)
    {
        var res = this.resources.FirstOrDefault(x => x.Type == resource.Type);
        return res.Count >= resource.Count;
    }

    public bool HaveResources(params Resource[] resources)
    {
        foreach (var res in resources)
            if (!HaveResource(res))
                return false;
        return true;
    }

    public float GetResourceCount(Resource.ResourceType type)
    {
        var resource = resources.FirstOrDefault(x => x.Type == type);
        if (resource == null)
            return 0;

        return resource.Count;
    }

    public Dictionary<Resource.ResourceType, float> GetResourcesCount(params Resource.ResourceType[] types)
    {
        var resources = new Dictionary<Resource.ResourceType, float>();
        foreach(var type in types)
            resources.Add(type, GetResourceCount(type));

        return resources;
    }
}
