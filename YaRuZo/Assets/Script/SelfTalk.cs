﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfTalk : MonoBehaviour,IMessageTransporter
{
    UIDataController _dataCtrl;
    [SerializeField] Text selfTalkText;

    private void Awake()
    {
        _dataCtrl = UIDataController.Instance;
        SetTransportParent_privete();
    }

    void SetSelfTalk()
    {
        var replaceSt=selfTalkText.text.Replace("title", _dataCtrl._nowSelectTodoData._Text);
        selfTalkText.text = replaceSt;
    }

    #region interfaceの実装
    /// <summary>
    /// メッセージ送信用の関数
    /// 名前がちょっとわかりにくい
    /// </summary>
    public void TranspotMessage_uiActive()
    {
        SetSelfTalk();
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
