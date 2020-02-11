using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VerticalButtonLayOut : MonoBehaviour
{
    [SerializeField] Button _buttonPrefab;
    [SerializeField] List<Button> _layoutList = new List<Button>();
    RectTransform _rectTr;
    RectTransform _RectTr
    {
        get
        {
            if (_rectTr == null)_rectTr=GetComponent<RectTransform>();
            return _rectTr;
        }
    }
    
    Button AddButton()
    {
        var inst = Instantiate(_buttonPrefab);
        _layoutList.Add(inst);
        inst.transform.SetParent(_RectTr);
        inst.transform.localScale = Vector3.one;
        return inst;
    }

    public Button AddButton(string text)
    {
        var bt=AddButton();
        SetText(_layoutList.Count-1,text);
        return bt;
    }

    void RemoveButton(int index)
    {
        Destroy(_layoutList[index].gameObject);
        _layoutList.RemoveAt(index);
    }

    public void ResetButton()
    {
        while (_layoutList.Count > 0)
        {
            RemoveButton(0);
        }
    }
    void SetText(int index,string text)
    {
        Text buttonText=_layoutList[index].GetComponentInChildren<Text>();
        buttonText.text = text;
    }

    public void SetOnclick(UnityEvent ue, int index)
    {
        _layoutList[index].onClick.AddListener(() => ue.Invoke());
    }
    public void SetOnclick(UnityAction ue, int index)
    {
        _layoutList[index].onClick.AddListener(() => ue.Invoke());
    }
}
