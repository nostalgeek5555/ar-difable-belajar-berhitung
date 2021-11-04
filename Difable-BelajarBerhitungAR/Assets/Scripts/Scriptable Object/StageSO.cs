using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "StageSO", menuName = "Scriptable Object/Stage")]
public class StageSO : ScriptableObject
{
    public string stageName;
    [ReorderableList] public List<LevelSO> stageLevels;
}
