using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//メッセージの対象登録をするクラス
//IMessageTransporter以外も使えるようにしたい
public class MessageTransporter : MonoBehaviour
{
    //メッセージを送る対象
    List<GameObject> _messageTargets=new List<GameObject>();

    protected void SendMessage2Target()
    {

        foreach (var target in _messageTargets)
        {
            ExecuteEvents.Execute<IMessageTransporter>(
             target: target,
             eventData: null,
             functor: (reciever, eventData) => reciever.TranspotMessage_uiActive()
            );
        }
    }

    //メッセージを送る対象を設定
    public void SetMessageTarget(GameObject obj)
    {
        foreach(var target in _messageTargets)
        {
            if (target == obj) return;
        }
        _messageTargets.Add(obj);
    }

    //トランスポータを親から検索
    //受信側が親に自分を登録するために使う
    public static MessageTransporter FindParentTransporter(Transform self)
    {
        Transform target = self;
        while (true)
        {
            target = target.parent;
            if (target == null) return null;
            var trans=target.GetComponent<MessageTransporter>();
            if (trans != null) return trans;
        }
    }
}
