using UnityEngine;
using Vuforia;
using Lean.Pool;

public class ARManager : MonoBehaviour
{
    public static ARManager Instance;
    public Transform calculationBoard;
    public MidAirPositionerBehaviour positionerBehaviour;

    private void Awake()
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

    public void SpawnARObject(LevelSO levelSO)
    {
        if (calculationBoard.childCount > 0)
        {
            for (int i = calculationBoard.childCount - 1; i >= 0; i--)
            {
                LeanPool.Despawn(calculationBoard.GetChild(i).gameObject);
            }
        }

        LeanPool.Spawn(levelSO.levelPrefab, calculationBoard);
    }

    public void OnTrackableFound()
    {
        positionerBehaviour.MidAirIndicator.SetActive(false);
    }

    public void OnTrackableLost()
    {
        positionerBehaviour.MidAirIndicator.SetActive(true);
    }
}
