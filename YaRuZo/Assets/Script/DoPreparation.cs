using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoPreparation : MonoBehaviour,IMessageTransporter
{
    UIDataController dataCtrl;
    [SerializeField] Text _titleText;

    private void Awake()
    {
        dataCtrl = UIDataController.Instance;
        SetTransportParent_privete();
    }

    void SetTitle()
    {
        string titleString = _titleText.text;
        var replaceSt=titleString.Replace("title", dataCtrl._nowSelectTodoData._Text);
        _titleText.text = replaceSt;
    }

    #region interfaceの実装
    /// <summary>
    /// メッセージ送信用の関数
    /// 名前がちょっとわかりにくい
    /// </summary>
    public void TranspotMessage_uiActive()
    {
        SetTitle();
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
}
