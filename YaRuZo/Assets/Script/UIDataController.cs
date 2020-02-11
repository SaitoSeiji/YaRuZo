using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataController : SingletonMonoBehaviour<UIDataController>
{
    public ToDoData _nowSelectTodoData { get; set; }
    public bool _removeDataFlag { get; set; }//trueなら_nowSelectTodoDataに一致するデータを削除
}
