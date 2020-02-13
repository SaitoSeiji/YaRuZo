using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataController : SingletonMonoBehaviour<UIDataController>
{
    [SerializeField] ToDoData _nowSelectedToDoData;
    public ToDoData _NowSelectTodoData { get { return _nowSelectedToDoData; } set { _nowSelectedToDoData = value; } }
    public bool _removeDataFlag { get; set; }//trueなら_nowSelectTodoDataに一致するデータを削除
}
