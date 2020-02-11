using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTodoData_inputpanel : MonoBehaviour,IMessageTransporter
{
    [SerializeField] InputField _inputField;

    private void Awake()
    {
        SetTransportParent_privete();   
    }

    void AddInput()
    {
        ToDoListUI.Instance.AddData(_inputField.textComponent.text);
    }

    void ResetInput()
    {
        _inputField.text = "";
    }
    #region button関数
    public void ButtonAction_addInput()
    {
        AddInput();
    }
    #endregion

    #region interfaceの実装
    /// <summary>
    /// メッセージ送信用の関数
    /// </summary>
    public void TranspotMessage_uiActive()
    {
        ResetInput();
    }
    /// <summary>
    /// メッセージを送ってもらうための登録をする
    /// 外部参照はされない
    /// </summary>
    public void SetTransportParent_privete()
    {
        var parent=MessageTransporter.FindParentTransporter(transform);
        parent.SetMessageTarget(gameObject);
    }
    #endregion
}
