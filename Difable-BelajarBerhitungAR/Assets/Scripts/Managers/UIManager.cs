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
    public GameObject leftSideNumberBox;
    public GameObject rightSideNumberBox;
    public TextMeshProUGUI leftSideNumber;
    public TextMeshProUGUI rightSideNumber;

    [Header("Score Popup UI")]
    public Image scorePopupBG;
    public GameObject scorePopup;
    public TextMeshProUGUI scoreTMP;
    public Button closePopupButton;

    [Header("Navigations UI")]
    public Button homeButton;
    public Button nextLevelButton;


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

        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(() =>
        {
            NextLevel();
        });
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveAllListeners();
        closePopupButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.RemoveAllListeners();
    }

    public void SetLevelUI(LevelSO levelSO)
    {
        stageTitleTMP.text = GameManager.Instance.currentStageSO.stageDisplayName;

        switch (levelSO.levelType)
        {
            case LevelSO.LevelType.Menghitung:
                leftSideNumberBox.SetActive(false);
                rightSideNumberBox.SetActive(false);
                Debug.Log("menghitung");
                break;

            case LevelSO.LevelType.Pengurangan:
                leftSideNumberBox.SetActive(true);
                rightSideNumberBox.SetActive(true);
                leftSideNumber.text = levelSO.totalBothSidesObject[0].ToString();
                rightSideNumber.text = levelSO.totalBothSidesObject[1].ToString();
                Debug.Log($"current left side number :: {leftSideNumber.text}");
                Debug.Log($"current right side number :: {rightSideNumber.text}");
                break;

            case LevelSO.LevelType.Penjumlahan:
                leftSideNumberBox.SetActive(true);
                rightSideNumberBox.SetActive(true);
                leftSideNumber.text = levelSO.totalBothSidesObject[0].ToString();
                rightSideNumber.text = levelSO.totalBothSidesObject[1].ToString();
                Debug.Log($"current left side number :: {leftSideNumber.text}");
                Debug.Log($"current right side number :: {rightSideNumber.text}");
                break;

            default:
                break;
        }
    }


    public IEnumerator PopulateLevelUI(string levelID)
    {
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


    public void ShowPopupScore(AnswerButton answerButton)
    {
        if (!answerButton.match)
        {
            answerButton.match = true;
            DataManager.Instance.playerScore += DataManager.Instance.basePoint;
        }
        

        scorePopupBG.gameObject.SetActive(true);
        scorePopup.SetActive(true);

        if (GameManager.Instance.randomizedLevelList.Count > 0)
        {
            nextLevelButton.gameObject.SetActive(true);
        }

        else
        {
            nextLevelButton.gameObject.SetActive(false);
        }

        scoreTMP.text = DataManager.Instance.playerScore.ToString();

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(0.1f);
        sequence.Join(scorePopup.transform.DOPunchScale(0.25f * Vector3.one, 0.25f, 3, 1));
    }

    public void BackToMainMenu()
    {
        DataManager.Instance.playerScore = 0;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        if (GameManager.Instance.randomizedLevelList.Count > 0)
        {
            scorePopupBG.gameObject.SetActive(false);
            scorePopup.SetActive(false);

            GameManager.Instance.SetupNextLevel();
        }
    }

}
