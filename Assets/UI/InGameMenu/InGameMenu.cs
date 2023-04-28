using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject UpgradeMenu;
    public GameObject GameOverMenu;
    public GameObject GamePauseMenu;

    private bool isGameOver = false;
    private TimeManager timeManager;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        UpgradeMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        GamePauseMenu.SetActive(false);
        timeManager = FindAnyObjectByType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        if(Input.GetKeyDown(InputSystem.UpgradeMenuOpenKey))
            SetActiveObject(UpgradeMenu);

        if (Input.GetKeyDown(InputSystem.PauseMenuOpenKey))
        {            
            SetActiveObject(GamePauseMenu);
            if (GamePauseMenu.activeSelf)
                timeManager.StopGame();
            else
                timeManager.ResumeGame();
        }
    }

    private void SetActiveObject(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void GameOver()
    {
        isGameOver = true;
        GameOverMenu.SetActive(true);
    }
}
