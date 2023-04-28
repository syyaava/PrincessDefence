using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    public TMP_Text ScoreText;
    public Resource.ResourceType ResourceType;

    private void Start()
    {
        StartCoroutine(ScoreUpdate());
    }

    private void UpdateScoreCountText()
    {
        var score = ResourceController.Instance.GetResourceCount(ResourceType);
        ScoreText.text = score.ToString();
    }

    private IEnumerator ScoreUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            UpdateScoreCountText();
        }
    }
}
