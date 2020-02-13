using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDoDoing : MonoBehaviour,IMessageTransporter
{
    public enum DoingState
    {
        BEFORE,
        DOING,
        BREAKE
    }
    [SerializeField] DoingState _doingState = DoingState.BEFORE;

    [SerializeField] Text _titleText;
    string _firstTitleText;
    [SerializeField] Text _doingTimetext;
    [SerializeField] Text _breakTimeText;
    [SerializeField] GameObject _doingPanel;
    [SerializeField] GameObject _breakPanel;
    bool _doingNow { get { return _doingPanel.activeInHierarchy; } }

    UIDataController dataCtrl;

    //float _doingTime;
    //float _breakTime;
    RealTimeTimer _doingTimer=new RealTimeTimer();
    RealTimeTimer _breakTimer=new RealTimeTimer();


    private void Awake()
    {
        dataCtrl = UIDataController.Instance;
        SetTransportParent_privete();
        _firstTitleText = _titleText.text;
    }

    private void Update()
    {

        float _doingTime=0;
        float _breakTime=0;
        if (_doingNow)
        {
            if (!_doingTimer._IsActive) _doingTimer.StartTimer();
            if (_breakTimer._IsActive) _breakTimer.StopTimer();
        }else if (!_doingNow && _doingState == DoingState.BREAKE)
        {
            if (_doingTimer._IsActive) _doingTimer.StopTimer();
            if (!_breakTimer._IsActive) _breakTimer.StartTimer();
        }
        _doingTime = _doingTimer.GetTimer();
        _breakTime = _breakTimer.GetTimer();

        DisplayTime(_doingTime, _doingTimetext);
        DisplayTime(_breakTime, _breakTimeText);
    }

    static void SwichActive(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }

    void DisplayTime(float time,Text displayText)
    {
        int minute = 0;
        int hour = 0;
        while (time >= 60)
        {
            minute++;
            time -= 60;
        }
        while (minute >= 60)
        {
            hour++;
            minute -= 60;
        }
        displayText.text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + time.ToString("00");
    }

    void ResetDoingTime()
    {
        //_doingTime = 0;
        _doingTimer.ResetTimer();
    }

    void ResetBreakTime()
    {
        //_breakTime = 0;
        _breakTimer.ResetTimer();
    }

    void StartDoingPanel()
    {
        _doingPanel.SetActive(true);
        _breakPanel.SetActive(false);
    }

    void BreakDoingPanel()
    {
        _doingPanel.SetActive(false);
        _breakPanel.SetActive(true);
    }

    void SetTitleText()
    {
        var replaceSt=_firstTitleText.Replace("title", dataCtrl._NowSelectTodoData._Text);
        _titleText.text = replaceSt;
    }
    

    #region interfaceの実装
    /// <summary>
    /// メッセージ送信用の関数
    /// 名前がちょっとわかりにくい
    /// </summary>
    public void TranspotMessage_uiActive()
    {
        SetTitleText();
        BreakDoingPanel();
        _doingState = DoingState.BEFORE;
        ResetBreakTime();
        ResetDoingTime();
        _doingTimetext.gameObject.SetActive(false);
        _breakTimeText.gameObject.SetActive(false);
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

    public void ButtonAction_RemoveData()
    {
        dataCtrl._removeDataFlag = true;
    }

    public void ButtonAction_StartDoing()
    {
        StartDoingPanel();
        _doingState = DoingState.DOING;
    }

    public void ButtonAction_BreakDoing()
    {
        BreakDoingPanel();
        _doingState = DoingState.BREAKE;
        ResetBreakTime();
    }

    public void ButtonAction_ChengeActiveBreakTime()
    {
        SwichActive(_breakTimeText.gameObject);
    }
    public void ButtonAction_ChengeActiveDoingTime()
    {
        SwichActive(_doingTimetext.gameObject);
    }
}
