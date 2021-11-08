using UnityEngine.SceneManagement;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void OnPickStage(string stageName)
    {
        if (!AudioManager.Instance.sfxAudioSource.isPlaying)
        {
            AudioManager.Instance.PlaySFX("button01");
        }

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
        if (!AudioManager.Instance.sfxAudioSource.isPlaying)
        {
            AudioManager.Instance.PlaySFX("button01");
        }

        Application.Quit();
    }
}
