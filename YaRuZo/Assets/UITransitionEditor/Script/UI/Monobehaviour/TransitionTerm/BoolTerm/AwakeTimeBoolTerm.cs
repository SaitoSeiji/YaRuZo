using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //アクティブになってからの経過時間が条件
    //指定時間を待たずに条件を満たすことがある
    public class AwakeTimeBoolTerm : AbstractUIBoolTerm
    {
        //editorで使用するためにpublic　ほんとはパブリックにしたくない
        public float waitLength;

        TimeFlag flag;

        public override void TranspotMessage_uiActive()
        {
            flag = new TimeFlag();
            flag.StartWait(waitLength);
        }

        protected override bool ConcreteTerm()
        {
            if (flag == null) TranspotMessage_uiActive();

            return !flag.WaitNow;
        }

        public override BoolTermType GetTermType()
        {
            return BoolTermType.AwakeTime;
        }
    }
}
