using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //キャンバスの開閉動作の実装を行う
    //デザインパターン:ブリッジのImplementorを表現
    public abstract class UICanvasOpenImplementor
    {
        //コンストラクタなくてもいいかも
        public UICanvasOpenImplementor(UICanvasBase firstCanvas)
        {
            AddCanvas(firstCanvas);
        }

        //現在のキャンバスを閉じて別のものを開く　ができない

        //キャンバスを開く処理
        public abstract void AddCanvas(UICanvasBase target);


        //履歴にあるキャンバスまで閉じる
        //lastOpen=trueならnextCanvasは閉じない
        public abstract void CloseCanvas(UICanvasBase nextCanvas, bool lastOpen);

        //次のsortOrderを計算
        public abstract int CaluculateNextSortOrder();
    }
}
