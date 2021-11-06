using UnityEngine.SceneManagement;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void OnPickStage(string stageName)
    {
        if (DataManager.Instance != null)
        {
            if (DataManager.Instance.stageDataTable.ContainsKey(stageName))
            {
                GameManager.Instance.currentStageSO = DataManager.Instance.stageDataTable[stageName];
                GameManager.Instance.currentLevelSO = DataManager.Instance.levelDataTable[stageName + "|0"];
                SceneManager.LoadSceneAsync(1);
            }
        }
    }

    public void OnExitApp()
    {
        Application.Quit();
    }
}
