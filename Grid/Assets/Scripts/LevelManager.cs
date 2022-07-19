using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LetterInstance _letterPrefab;
    [SerializeField] private RectTransform _gameField;
    [SerializeField] private Text _inputHeight;
    [SerializeField] private Text _inputWidth;        
    [SerializeField] private float _flyTime = 2f;

    private List<LetterInstance> letterInstances = new List<LetterInstance>();
    private float _parsedWidth, _parsedHeight;
    private float _gameFieldWidth, _gameFieldHeight;

    [HideInInspector] public UnityEvent generatedLetters;
    [HideInInspector] public UnityEvent<float> mixedLetters;

    [ContextMenu("Generate Letters")]
    public void GenerateLetters()
    {
        PreCalc();
        ClearLastInstances();

        float _size = CalcLettersSize();

        for (int i = 0; i < _parsedHeight; i++)
        {
            for (int j = 0; j < _parsedWidth; j++)
            {
                LetterInstance _created = Instantiate(_letterPrefab);
                letterInstances.Add(_created);
                _created.transform.SetParent(_gameField);
                _created.SetPosition(CalcLetterPosition(j, i), _size);
            }
        }
        generatedLetters?.Invoke();
    }

    public void MixLetters()
    {
        List<LetterInstance> tempLetterList = new List<LetterInstance>(letterInstances);
        while (tempLetterList.Count > 1)
        {
            int randomIndex = UnityEngine.Random.Range(1, tempLetterList.Count);

            SwapPositions(tempLetterList[0], tempLetterList[randomIndex]);

        tempLetterList.RemoveAt(randomIndex);
        tempLetterList.RemoveAt(0);
            
        }

        if (tempLetterList.Count>0)
            tempLetterList[0].SetTargetPos(tempLetterList[0].GetAnchoredPos());

        foreach (LetterInstance letter in letterInstances)
        {
            letter.StartMove(_flyTime);
        }

        mixedLetters?.Invoke(_flyTime);
        
    }

    private void SwapPositions(LetterInstance letter_1, LetterInstance letter_2)
    {
        letter_1.SetTargetPos(letter_2.GetAnchoredPos());
        letter_2.SetTargetPos(letter_1.GetAnchoredPos());
    }

    private void ClearLastInstances()
    {
        foreach (LetterInstance letter in letterInstances)
        {
            Destroy(letter.gameObject);
        }
        letterInstances.Clear();
    }

    private void PreCalc()
    {
        _parsedWidth = float.Parse(_inputWidth.text);
        _parsedHeight = float.Parse(_inputHeight.text);

        _gameFieldWidth = Screen.width + _gameField.sizeDelta.x;
        _gameFieldHeight = Screen.height + _gameField.sizeDelta.y;
    }

    private float CalcLettersSize()
    {
        float _size = Mathf.Min(
            _gameFieldWidth / _parsedWidth,
            _gameFieldHeight / _parsedHeight
            );
        return _size;
    }

    private Vector2 CalcLetterPosition(int posX, int posY)
    {
        return new Vector2(
            (_gameFieldWidth / _parsedWidth) / 2 + (_gameFieldWidth / _parsedWidth) * posX,
            -((_gameFieldHeight / _parsedHeight) / 2 + (_gameFieldHeight / _parsedHeight) * posY)
            );
    }
}
