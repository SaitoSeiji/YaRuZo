using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using aojiru_UI;

[System.Serializable]
public class ToDoData
{
    [SerializeField]string _text;
    public string _Text { get { return _text; } }
    public ToDoData(string text)
    {
        _text = text;
    }
}

public class ToDoListUI : SingletonMonoBehaviour<ToDoListUI>,IMessageTransporter
{
    [SerializeField] VerticalButtonLayOut _buttonLayout;
    [SerializeField]List<ToDoData> _toDoListData=new List<ToDoData>();
    [SerializeField] OncliclUITrrigerTerm clickTrriger;
    UIDataController _dataCtrl;
    protected override void Awake()
    {
        base.Awake();
        _dataCtrl = UIDataController.Instance;

        SetTransportParent_privete();
    }

    public void AddData(string data)
    {
        _toDoListData.Add(new ToDoData(data));
    }

    public void RemoveData(int index)
    {
        _toDoListData.RemoveAt(index);
    }

    void SyncTodoList()
    {
        _buttonLayout.ResetButton();
        for (int i=0;i<_toDoListData.Count;i++)
        {
            var createButton= _buttonLayout.AddButton(_toDoListData[i]._Text);
            clickTrriger.AddTargetButtons(createButton);

            UnityEvent onClick = new UnityEvent();
            //山椒が残ってしまう問題を解決
            int index = i;
            onClick.AddListener(()=>_dataCtrl._nowSelectTodoData=_toDoListData[index]);
            _buttonLayout.SetOnclick(onClick, i);
        }
    }

    void RemoveSelectData()
    {
        if (!_dataCtrl._removeDataFlag) return;
        if (_dataCtrl._nowSelectTodoData == null) return;
        for(int i=_toDoListData.Count-1;i>=0;i--)
        {
            if (_toDoListData[i].Equals(_dataCtrl._nowSelectTodoData))
            {
                _toDoListData.RemoveAt(i);
                _dataCtrl._removeDataFlag = false;
                return;
            }
        }
    }
    #region interfaceの実装
    /// <summary>
    /// メッセージ送信用の関数
    /// </summary>
    public void TranspotMessage_uiActive()
    {
        RemoveSelectData();
        SyncTodoList();
    }
    /// <summary>
    /// メッセージを送ってもらうための登録をする
    /// 外部参照はされない
    /// </summary>
    public void SetTransportParent_privete()
    {
        var tran=MessageTransporter.FindParentTransporter(transform);
        tran.SetMessageTarget(gameObject);
    }
    #endregion

}
