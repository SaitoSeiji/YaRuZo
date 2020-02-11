using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//指定時間待つを達成するクラス
//待っている間trueを返す
public class TimeFlag
{
    float startTime=0;
    float waitLength=0;
    
    //待ち時間中であることを示すフラグ
    public bool WaitNow
    {
        get
        {
            return ! (Time.fixedTime > startTime + waitLength);
        }
    }

    //待ち時間を開始
    //指定した時間からwait待つ　つまり足されるわけではない（仕様としての吟味はしてない）
    public void StartWait(float wait)
    {
        startTime = Time.fixedTime;
        waitLength = wait;
    }
}
