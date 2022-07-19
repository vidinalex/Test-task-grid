using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LetterInstance : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Text _text;

    private Vector2 _targetPos;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _text.text = ((char)Random.Range(gameData.charRange.x, gameData.charRange.y)).ToString();
    }

    public void SetPosition(Vector2 _position, float _size)
    {
        _rectTransform.anchoredPosition = _position;
        _rectTransform.sizeDelta = new Vector2(_size, _size);
    }

    public Vector2 GetAnchoredPos()
    {
        return _rectTransform.anchoredPosition;
    }

    public void SetTargetPos(Vector2 _target)
    {
        _targetPos = _target;
    }

    public void StartMove(float _waitTime)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(MoveToSpot(_waitTime,GetAnchoredPos()));
    }



    IEnumerator MoveToSpot(float _waitTime, Vector2 startPos)
    {
        float _elapsedTime = 0;
        while (_elapsedTime < _waitTime)
        {
            _elapsedTime += Time.deltaTime;
            _rectTransform.anchoredPosition = Vector2.Lerp(startPos, _targetPos, _elapsedTime/_waitTime);
            yield return null;
        }
        _rectTransform.anchoredPosition = _targetPos; // just in case :)
        yield return null;
    }
    }
