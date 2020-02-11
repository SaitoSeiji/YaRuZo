using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一度だけtrueを読み取れるboolの拡張版
public class Trriger
{
    bool _trriger=false;
    public bool _Trriger
    {
        get
        {
            bool flagCopy = _trriger;
            _trriger = false;
            return flagCopy;
        }
        set
        {
            _trriger = value;
        }
    }
}
