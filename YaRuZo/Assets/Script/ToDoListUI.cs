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
[System.Serializable]
public class ToDoDataList
{
    [SerializeField]public  List<ToDoData> _list = new List<ToDoData>();

}

public class ToDoListUI : SingletonMonoBehaviour<ToDoListUI>,IMessageTransporter
{
    [SerializeField] VerticalButtonLayOut _buttonLayout;
    //[SerializeField]List<ToDoData> _toDoListData=new List<ToDoData>();
    ToDoDataList _toDoListData = new ToDoDataList();
    [SerializeField] OncliclUITrrigerTerm clickTrriger;
    UIDataController _dataCtrl;
    SaveActor<ToDoDataList> _toDoDataSave=new SaveActor<ToDoDataList>();
    string _toDoDataSavePath = "todoData";

    protected override void Awake()
    {
        base.Awake();
        _dataCtrl = UIDataController.Instance;

        SetTransportParent_privete();
        LoadData();
    }


    public void AddData(string data)
    {
        _toDoListData._list.Add(new ToDoData(data));
        SaveData();
    }

    public void RemoveData(int index)
    {
        _toDoListData._list.RemoveAt(index);
        SaveData();
    }

    void SyncTodoList()
    {
        _buttonLayout.ResetButton();
        for (int i=0;i<_toDoListData._list.Count;i++)
        {
            var createButton= _buttonLayout.AddButton(_toDoListData._list[i]._Text);
            clickTrriger.AddTargetButtons(createButton);

            UnityEvent onClick = new UnityEvent();
            //山椒が残ってしまう問題を解決
            int index = i;
            onClick.AddListener(()=>_dataCtrl._NowSelectTodoData=_toDoListData._list[index]);
            _buttonLayout.SetOnclick(onClick, i);
        }
    }

    void RemoveSelectData()
    {
        if (!_dataCtrl._removeDataFlag) return;
        if (_dataCtrl._NowSelectTodoData == null) return;
        for(int i=_toDoListData._list.Count-1;i>=0;i--)
        {
            if (_toDoListData._list[i].Equals(_dataCtrl._NowSelectTodoData))
            {
                _toDoListData._list.RemoveAt(i);
                _dataCtrl._removeDataFlag = false;
                return;
            }
        }
    }

    void SaveData()
    {
        _toDoDataSave.Save(_toDoListData, _toDoDataSavePath);
    }

    void LoadData()
    {
        _toDoListData = _toDoDataSave.Load(_toDoDataSavePath);
        if (_toDoListData == null) _toDoListData = new ToDoDataList();
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
