using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILock : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Button _btnMix;
    [SerializeField] private Button _btnGenerate;
    [SerializeField] private InputField _inputWidth;
    [SerializeField] private InputField _inputHeight;


    private void OnEnable()
    {
        _btnMix.interactable = false;
        _btnGenerate.interactable = false;

        levelManager.mixedLetters.AddListener(MixBtnDelay);
        levelManager.generatedLetters.AddListener(ReadyToMix);
    }
    public void CheckForCorrectInput()
    {
        int _parsedWidth, _parsedHeight;
        int.TryParse(_inputWidth.text, out _parsedWidth);
        int.TryParse(_inputHeight.text, out _parsedHeight);
        if (
            _parsedWidth >= gameData.minValue &&
            _parsedWidth <= gameData.maxValue &&
            _parsedHeight >= gameData.minValue &&
            _parsedHeight <= gameData.maxValue
            )
            _btnGenerate.interactable = true;
        else
            _btnGenerate.interactable = false;
        
    }

    public void MixBtnDelay(float delay)
    {
        StartCoroutine(WaitForDelay(delay));
    }

    IEnumerator WaitForDelay(float _waitTime)
    {
        _btnMix.interactable = false;
        _btnGenerate.interactable = false;
        yield return new WaitForSeconds(_waitTime);
        _btnMix.interactable = true;
        _btnGenerate.interactable = true;
    }

    private void ReadyToMix()
    {
        _btnMix.interactable = true;
    }
}
