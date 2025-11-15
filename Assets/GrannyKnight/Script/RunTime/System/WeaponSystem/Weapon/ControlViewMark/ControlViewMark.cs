using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlViewMark : MonoBehaviour
{
    [SerializeField] private float _timeWait = 0.1f;
    [SerializeField] private float _timeWaitForDisable = 10f;
    [SerializeField] private int _startValueMark = 50;
    [SerializeField] private Mark markPref;
    [SerializeField] private GameObject _marksParent;
    private List<Mark> _list;
    private WaitForSeconds _wait;
    private int _valueMark;
    private event Action OnUpdateMark;

    private void OnDisable()
    {
        MainSystem.OnUpdate -= OnUpdate;
    }

    private void OnUpdate()
    {
        OnUpdateMark?.Invoke();
    }

    public void Initialization()
    {
        _valueMark = _startValueMark;
        _wait = new WaitForSeconds(_timeWait);
        _list = new List<Mark>();
        MainSystem.OnUpdate += OnUpdate;
        CreatePool();
    }

    public Mark GetMark()
    {
        Mark mark = FindFreeMark();
        if (mark == null)
        {
            mark = _list[0];
            mark.End();
        }
        OnUpdateMark += mark.OnUpdate;
        mark.OnEnd += MarkEnd;
        mark.OnDestroyMark += MarkDestroy;
        return mark;
    }

    private void MarkEnd(Mark mark)
    {
        OnUpdateMark -= mark.OnUpdate;
        mark.OnEnd -= MarkEnd;
        mark.OnDestroyMark -= MarkDestroy;
    }

    private void MarkDestroy(Mark mark)
    {
        MarkEnd(mark);
        if (_list.Contains(mark))
        {
            _list.Remove(mark);
        }
    }

    private void CreatePool()
    {
        if (_list.Count == 0)
        {
            StartCoroutine(Create(markPref, _valueMark));
        }
    }

    private Mark FindFreeMark()
    {
        foreach (Mark go in _list)
        {
            if (go.IsFree)
            {
                return go;
            }
        }
        WaitMark();
        return null;
    }

    private void WaitMark()
    {
        _valueMark *= 2;
        StartCoroutine(Create(markPref, _valueMark));
    }

    private IEnumerator Create(Mark markPref, int valueMark)
    {
        while (_list.Count < valueMark)
        {
            yield return _wait;
            GameObject tempGameObject = Instantiate(markPref.gameObject, Vector3.zero, Quaternion.identity, _marksParent.transform);
            Mark tempMark = tempGameObject.GetComponent<Mark>();
            tempMark.Initialization(_timeWaitForDisable);
            _list.Add(tempMark);
        }
    }
}