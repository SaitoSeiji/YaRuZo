using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//MessageTransporterがメッセージを送る際のインターフェース
public interface IMessageTransporter:IEventSystemHandler
{
    /// <summary>
    /// メッセージ送信用の関数
    /// </summary>
    void TranspotMessage_uiActive();
    /// <summary>
    /// メッセージを送ってもらうための登録をする
    /// 外部参照はされない
    /// </summary>
    void SetTransportParent_privete();
}
