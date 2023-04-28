using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static float HorizontalAxis = 0f;
    public static KeyCode UpgradeMenuOpenKey;
    public static KeyCode PauseMenuOpenKey;
    private List<KeyCode> positiveHorizontalAxis = new List<KeyCode>();
    private List<KeyCode> negativeHorizontalAxis = new List<KeyCode>();    

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadDefaultSettings();
    }

    void Update()
    {
        foreach (var positive in positiveHorizontalAxis)
            if (Input.GetKey(positive))
            {
                HorizontalAxis = 1f;
                return;
            }

        foreach (var negative in negativeHorizontalAxis)
            if(Input.GetKey(negative))
            {
                HorizontalAxis = -1f;
                return;
            }

        HorizontalAxis = 0f;
    }

    public void LoadDefaultSettings()
    {
        positiveHorizontalAxis = new List<KeyCode>();
        negativeHorizontalAxis = new List<KeyCode>();

        negativeHorizontalAxis.Add(KeyCode.D);
        negativeHorizontalAxis.Add(KeyCode.RightArrow);
        //negativeHorizontalAxis.Add(KeyCode.Mouse1);

        positiveHorizontalAxis.Add(KeyCode.A);
        positiveHorizontalAxis.Add(KeyCode.LeftArrow);
        //positiveHorizontalAxis.Add(KeyCode.Mouse0);

        UpgradeMenuOpenKey = KeyCode.U;
        PauseMenuOpenKey = KeyCode.Escape;
    }
}
