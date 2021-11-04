using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public List<StageSO> stageList = new List<StageSO>();
    public List<LevelSO> levelList = new List<LevelSO>();
    public Dictionary<string, StageSO> stageDataTable;
    public Dictionary<string, LevelSO> levelDataTable;

    public List<string> stageDataName;
    public List<string> levelDataName;

    public int playerScore;
    public int basePoint = 25;
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

        GetResources();
    }

    private void GetResources()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        stageList = new List<StageSO>(Resources.LoadAll<StageSO>("Scriptable Object/Stages"));
        stageDataTable = new Dictionary<string, StageSO>();
        levelDataTable = new Dictionary<string, LevelSO>();

        Debug.Log($"stage count :: {stageList.Count}");
        for (int i = 0; i < stageList.Count; i++)
        {
            StageSO stageSO = stageList[i];
            stageDataTable.Add(stageSO.stageName, stageList[i]);
            Debug.Log($"Current stage name added :: {stageDataTable[stageSO.stageName].stageName}");

            if (stageSO.stageLevels.Count > 0)
            {
                levelList = new List<LevelSO>(stageSO.stageLevels);
                for (int j = 0; j < levelList.Count; j++)
                {
                    LevelSO levelSO = levelList[j];
                    string levelKey = stageSO.stageName + "|" + levelSO.levelID.ToString();
                    levelDataTable.Add(levelKey, levelSO);
                    Debug.Log($"Current level name added :: {levelDataTable[levelKey].levelID}");
                }
            }
        }
    }


#if UNITY_EDITOR
    void Update()
    {
        stageDataName = stageDataTable.Keys.ToList();
        levelDataName = levelDataTable.Keys.ToList();
    }
#endif


    public class PlayerData
    {
        private int playerScore;
        private int totalLevelUnlocked;

        public int _playerScore { get => playerScore; set => playerScore = value; }
        public int _totalLevelUnlocked { get => totalLevelUnlocked; set => totalLevelUnlocked = value; }


        public PlayerData() { }
        public PlayerData(int score, int levelUnlock)
        {
            playerScore = score;

        }
    }
}
