using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPooler : MonoBehaviour
{
    [SerializeField] private LetterInstance _letterPrefab;
    [SerializeField] private GameData _gameData;

    [SerializeField] private List<LetterInstance> _letters = new List<LetterInstance>();
    private int _lastLetter = 0;

    [ContextMenu("GeneratePool")]
    private void GeneratePool()
    {
        for (int i = 0; i < Mathf.Pow(_gameData.maxValue,2)+1; i++)
        {
            LetterInstance _letter;
            _letter = Instantiate(_letterPrefab);
            _letter.transform.SetParent(transform);            
            _letters.Add(_letter);
        }
    }

    public LetterInstance NextLetter()
    {
        if(_lastLetter == _letters.Count-1) _lastLetter = 0;
        _letters[_lastLetter].gameObject.SetActive(true);
        return _letters[_lastLetter++];
    }
}
