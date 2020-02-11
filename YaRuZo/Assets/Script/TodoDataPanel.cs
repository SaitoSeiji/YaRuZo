using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TodoDataPanel : MonoBehaviour,IMessageTransporter
{
    [SerializeField] Text title;
    UIDataController dataCtrl;
    private void Awake()
    {
        dataCtrl = UIDataController.Instance;
        SetTransportParent_privete();
    }

    void InitData()
    {
        var data = dataCtrl._nowSelectTodoData;
        title.text = data._Text;
    }

    #region interfaceの実装
    /// <summary>
    /// メッセージ送信用の関数
    /// </summary>
    public void TranspotMessage_uiActive()
    {
        InitData();
    }
    /// <summary>
    /// メッセージを送ってもらうための登録をする
    /// 外部参照はされない
    /// </summary>
    public void SetTransportParent_privete()
    {
        var tran = MessageTransporter.FindParentTransporter(transform);
        tran.SetMessageTarget(gameObject);
    }
    #endregion
    #region buttonAction
    public void ButtonAction_DeleteData()
    {
        dataCtrl._removeDataFlag = true;
    }
    #endregion
}
