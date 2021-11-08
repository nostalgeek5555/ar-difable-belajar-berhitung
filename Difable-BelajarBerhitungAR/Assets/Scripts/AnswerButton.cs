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
    public List<Sprite> buttonImages;

    [Header("Answer Properties")]
    public int answer;
    public bool match = false;

    public void SetAnswerButton(int _answer, int imageID)
    {
        match = false;
        answer = _answer;
        answerText.text = answer.ToString();
        answerButton.GetComponent<Image>().sprite = buttonImages[imageID];
    }

    private void Start()
    {
        answerButton.onClick.RemoveAllListeners();
        answerButton.onClick.AddListener(() => { 
            if (answerButton.IsInteractable())
            {
                if (!AudioManager.Instance.sfxAudioSource.isPlaying)
                {
                    AudioManager.Instance.PlaySFX("button01");
                }
                GameManager.Instance.CheckAnswer(this);
            }
        });
    }
}
