using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Object/Level")]
public class LevelSO : ScriptableObject
{
    public LevelType levelType;
    public int levelID;
    public GameObject levelPrefab;
    [ReorderableList] public List<int> totalBothSidesObject;
    [ReorderableList] public List<int> levelAnswers;
    public int matchAnswer;

    public enum LevelType
    {
        Menghitung = 0,
        Pengurangan = 1,
        Penjumlahan = 2
    }
}
