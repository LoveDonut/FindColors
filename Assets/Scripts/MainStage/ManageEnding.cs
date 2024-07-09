using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageEnding : MonoBehaviour
{
    [SerializeField] List<GameObject> hearts;
    [SerializeField] GameObject blackHearts;
    [SerializeField] GameObject completeCanvas;
    [SerializeField] TextMeshProUGUI endingText;

    GameManager gameManager;

    int stageCount = 3;
    int endingFurcationIndex = 3;
    string sadEndingText = "";
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        CheckSadEnding();

        CheckAllStageClear();
    }
    bool CheckSadEnding()
    {
        if (gameManager.isSadEnding && gameManager.GetStageClearance(endingFurcationIndex))
        {
            sadEndingText = " But you lost 3 colors...";
            blackHearts.SetActive(true);
            return true;
        }
        return false;
    }

    void CheckAllStageClear()
    {
        if (IsAllStageClear())
        {
            endingText.SetText("Game Clear!" + sadEndingText);
            GameManager.isGameActive = false;
        }
        else
        {
            completeCanvas.SetActive(false);
        }
    }
    bool IsAllStageClear()
    {
        int count = 0;
        for (int i = 1; i <= stageCount; i++)
        {
            if (gameManager.GetStageClearance(i))
            {
                hearts[i-1].SetActive(true);
                count++;
            }
        }

        return count == stageCount;
    }
}
