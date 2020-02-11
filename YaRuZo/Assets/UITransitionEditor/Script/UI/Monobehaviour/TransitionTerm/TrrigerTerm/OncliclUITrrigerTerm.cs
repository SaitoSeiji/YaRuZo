using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace aojiru_UI
{
    //指定のボタンを押すと条件を満たす
    public class OncliclUITrrigerTerm : AbstractUITrrigerTerm
    {
        [SerializeField] List<Button> targetButtons =new List<Button>();//指定のボタン
        public Button _HeadButton
        {
            get
            {
                if (targetButtons.Count == 0) targetButtons.Add(null);

                return targetButtons[0];
            }
            set { targetButtons[0] = value; }
        }

        protected override CoalTiming_StaisfyAction SetCoalTiming()
        {
            return CoalTiming_StaisfyAction.AWAKE;
        }

        protected override bool SetSatisfyAction()
        {
            //指定のボタンに「押されたら条件達成」を設定
            foreach (var bt in targetButtons)
            {
                if (bt == null) continue;
                bt.onClick.AddListener(() => SetSatisfyTrriger(true));
            }
            return true;
        }

        public override TrrigerType GetTrrigerType()
        {
            return TrrigerType.Onclick;
        }
        #region 後から追加
        public void AddTargetButtons(Button bt)
        {
            bt.onClick.AddListener(() => SetSatisfyTrriger(true));
        }
        
        #endregion
    }
}
