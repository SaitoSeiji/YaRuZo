using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//リアルタイムの経過時間を計測する
public class RealTimeTimer 
{
    float _nowStartTime;
    float _elapsedTime_bofore;

    public bool _IsActive { get; private set; }
    
    public void StartTimer()
    {
        _IsActive = true;
        _nowStartTime = Time.realtimeSinceStartup;
    }

    public void StopTimer()
    {
        _elapsedTime_bofore = GetTimer();
        _IsActive = false;
    }

    public void ResetTimer()
    {
        _nowStartTime = Time.realtimeSinceStartup;
        _elapsedTime_bofore = 0;
    }

    public float GetTimer()
    {
        if (_IsActive)
        {
            float nowElapsedTime = Time.realtimeSinceStartup - _nowStartTime;
            return nowElapsedTime + _elapsedTime_bofore;
        }
        else
        {
            return _elapsedTime_bofore;
        }
    }
}
