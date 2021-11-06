using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public StageSO currentStageSO;
    public LevelSO currentLevelSO;

    public List<LevelSO> randomizedLevelList;

    [Header("Level Properties")]
    private int stageID;
    private int levelID;
    private int matchAnswer;

    //level properties setter getter
    public int _stageID { get => stageID; set => stageID = value; }
    public int _levelID { get => levelID; set => levelID = value; }
    public int _matchAnswer { get => matchAnswer; set => matchAnswer = value; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != null && Instance == this)
        {
            Debug.Log("instance this");
            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
        {
            Debug.Log("destroy duplicate instance");
            Destroy(gameObject);
            return;
        }

        

    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
    }


    public void OnSceneChange(Scene currentScene, Scene nextScene)
    {
        
        Debug.Log("on scene change");
        if (nextScene.buildIndex == 1)
        {
            if (currentStageSO != null && currentLevelSO != null)
            {
                Debug.Log($"Next scene name == {nextScene.name}");
                randomizedLevelList = new List<LevelSO>(DataManager.Instance.levelDataTable.Values.ToList().Where(x => x.levelType == currentLevelSO.levelType));

                SetupNextLevel();
                
            }
        }

        if (nextScene.buildIndex == 0)
        {
            randomizedLevelList.Clear();
        }
    }

    public void SetupNextLevel()
    {
        LevelSO randomLevelSO = PickRandomLevel();
        UIManager.Instance.SetLevelUI(randomLevelSO.levelType);
        SetupLevelData(randomLevelSO);
    }

    public LevelSO PickRandomLevel()
    {
        int randomLevelPick = Random.Range(0, randomizedLevelList.Count - 1);

        LevelSO pickedLevelSO = randomizedLevelList[randomLevelPick];
        randomizedLevelList.Remove(pickedLevelSO);

        Debug.Log($"random picked level :: {pickedLevelSO.levelID}");
        return pickedLevelSO;
    }

    public void SetupLevelData(LevelSO levelSO)
    {
        if (Instance != null)
        {
           currentLevelSO = levelSO;
           _levelID = levelSO.levelID;
           _matchAnswer = levelSO.matchAnswer;
        }

        ARManager.Instance.SpawnARObject(currentLevelSO);
        string levelKey = DataManager.Instance.levelDataTable.FirstOrDefault(x => x.Value == currentLevelSO).Key;
        StartCoroutine(UIManager.Instance.PopulateLevelUI(levelKey));
    }


    public void CheckAnswer(int _answer)
    {
        if (_answer == _matchAnswer)
        {
            UIManager.Instance.ShowPopupScore();
        }
    }


}
