using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    public class DefaultCanvasImpl : UICanvasOpenImplementor
    {
        public DefaultCanvasImpl(UICanvasBase firstCanvas) : base(firstCanvas)
        {

        }

        Stack<UICanvasBase> _openCanvasHirtory = new Stack<UICanvasBase>();
        public UICanvasBase _historyTop { get { return _openCanvasHirtory.Peek(); } }

        public override void AddCanvas(UICanvasBase target)
        {
            if (_openCanvasHirtory.Count > 0)
            {
                var head = _openCanvasHirtory.Peek();
                head.ChengeUIState(UICanvasBase.UISTATE.SLEEP);
            }

            target.gameObject.SetActive(true);
            _openCanvasHirtory.Push(target);
            target.ChengeUIState(UICanvasBase.UISTATE.ACTIVE);
        }

        public override void CloseCanvas(UICanvasBase nextCanvas, bool lastOpen)
        {
            if (_openCanvasHirtory.Contains(nextCanvas))
            {
                while (true)
                {
                    //nextCanvasより上の階層のものをすべて閉じる
                    //lastOpen=falseならnextCanvasも閉じる
                    var next = _openCanvasHirtory.Peek();
                    if (next != nextCanvas)
                    {
                        _openCanvasHirtory.Pop();
                        next.gameObject.SetActive(false);
                        next.ChengeUIState(UICanvasBase.UISTATE.CLOSE);
                    }
                    else if (next == nextCanvas && !lastOpen)
                    {
                        _openCanvasHirtory.Pop();
                        next.gameObject.SetActive(false);
                        next.ChengeUIState(UICanvasBase.UISTATE.CLOSE);
                        break;
                    }
                    else if (next == nextCanvas && lastOpen)
                    {
                        next.ChengeUIState(UICanvasBase.UISTATE.ACTIVE);
                        break;
                    }
                }
            }
            else
            {
                //閉じるものがスタックにないときのエラー
                //わかりやすいエラーコードにしたい
                Debug.Log("DefaultCanvasImpl error");
                return;
            }
        }
        public override int CaluculateNextSortOrder()
        {
            var head = _openCanvasHirtory.Peek();
            return head.SelfCanvas.sortingOrder + 1;
        }

        //履歴stackに含まれているかどうか
        public bool ContaintHitory(UICanvasBase target)
        {
            return _openCanvasHirtory.Contains(target);
        }

    }
}
