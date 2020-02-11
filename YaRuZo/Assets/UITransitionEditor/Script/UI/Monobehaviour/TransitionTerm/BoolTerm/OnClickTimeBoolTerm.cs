using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace aojiru_UI
{
    //ボタンを押した回数で条件達成
    public class OnClickTimeBoolTerm : AbstractUIBoolTerm
    {
        [SerializeField] public int _targetClickTime;//目標回数
        [SerializeField] int _nowClickTime;//現在の回数
        [SerializeField] Button[] _clickTargets = new Button[1];
        public Button _ClickTargetHead { get { return _clickTargets[0]; } set { _clickTargets[0] = value; } }

        protected override bool ConcreteTerm()
        {
            return _nowClickTime >= _targetClickTime;
        }

        protected override void InitAction()
        {
            base.InitAction();
            //ボタンに条件を設定
            foreach (var btn in _clickTargets)
            {
                btn.onClick.AddListener(() => _nowClickTime++);
            }
        }

        public override void TranspotMessage_uiActive()
        {
            _nowClickTime = 0;
        }

        public override BoolTermType GetTermType()
        {
            return BoolTermType.OnClickTime;
        }
    }
}
