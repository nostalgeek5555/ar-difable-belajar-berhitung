using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Lean.Pool;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    [Header("Button Properties")]
    public Button answerButton;
    public TextMeshProUGUI answerText;

    [Header("Answer Properties")]
    public int answer;

    public void SetAnswerButton(int _answer)
    {
        answer = _answer;
        answerText.text = answer.ToString();
    }

    private void Start()
    {
        answerButton.onClick.RemoveAllListeners();
        answerButton.onClick.AddListener(() => { 
            if (answerButton.IsInteractable())
            {
                GameManager.Instance.CheckAnswer(answer);
            }
        });
    }
}
