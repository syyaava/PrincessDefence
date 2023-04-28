using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSwordContainer : MonoBehaviour
{
    public GameObject SelectedSword;

    private void Start()
    {
        SelectedSword = null;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSword(GameObject sword)
    {
        SelectedSword = sword;
    }
}
