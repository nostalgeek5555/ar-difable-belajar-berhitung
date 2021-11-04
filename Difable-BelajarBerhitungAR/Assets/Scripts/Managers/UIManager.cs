using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Level UI")]
    public TextMeshProUGUI stageTitleTMP;
    public Transform answerPanel;
    public GameObject answerButtonPrefab;
    public TextMeshProUGUI leftSideNumber;
    public TextMeshProUGUI rightSideNumber;

    [Header("Score Popup UI")]
    public Image scorePopupBG;
    public GameObject scorePopup;
    public TextMeshProUGUI scoreTMP;
    public Button closePopupButton;

    [Header("Navigations UI")]
    public Button homeButton;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(() =>
        {
            BackToMainMenu();
        });


        closePopupButton.onClick.RemoveAllListeners();
        closePopupButton.onClick.AddListener(() =>
        {
            ClosePopupScore();
        });
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveAllListeners();
        closePopupButton.onClick.RemoveAllListeners();
    }


    public IEnumerator PopulateLevelUI(string levelID)
    {
        if(GameManager.Instance.currentLevelSO.levelType == LevelSO.LevelType.Menghitung)
        {
            leftSideNumber.gameObject.SetActive(false);
            rightSideNumber.gameObject.SetActive(false);
        }

        else
        {
            leftSideNumber.gameObject.SetActive(true);
            rightSideNumber.gameObject.SetActive(true);
            leftSideNumber.text = GameManager.Instance.currentLevelSO.totalBothSidesObject[0].ToString();
            rightSideNumber.text = GameManager.Instance.currentLevelSO.totalBothSidesObject[1].ToString();
        }


        if (answerPanel.childCount > 0)
        {
            for (int i = answerPanel.childCount - 1; i >= 0; i--)
            {
                LeanPool.Despawn(answerPanel.GetChild(i).gameObject);
            }
        }

        yield return 0;

        if (DataManager.Instance.levelDataTable.Count > 0)
        {
            if (DataManager.Instance.levelDataTable.ContainsKey(levelID))
            {
                AnswerButton answerButton;
                LevelSO levelSO = DataManager.Instance.levelDataTable[levelID];

                for (int j = 0; j < levelSO.levelAnswers.Count; j++)
                {
                    answerButton = LeanPool.Spawn(answerButtonPrefab, answerPanel).GetComponent<AnswerButton>();
                    answerButton.SetAnswerButton(levelSO.levelAnswers[j]);
                    Debug.Log($"current option number {j} :: {levelSO.levelAnswers[j]}");
                }
            }
        }
    }

    public void ClosePopupScore()
    {
        scorePopupBG.gameObject.SetActive(false);
        scorePopup.SetActive(false);
    }


    public void ShowPopupScore()
    {
        DataManager.Instance.playerScore = 0;
        DataManager.Instance.playerScore += DataManager.Instance.basePoint;

        scorePopupBG.gameObject.SetActive(true);
        scorePopup.SetActive(true);
        scoreTMP.text = DataManager.Instance.playerScore.ToString();

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(0.1f);
        sequence.Join(scorePopup.transform.DOPunchScale(0.25f * Vector3.one, 0.25f, 3, 1));
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
