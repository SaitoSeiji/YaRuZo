using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using aojiru_UI;
#if UNITY_EDITOR
using UnityEditor;

namespace aoji_EditorUI
{
    public class BoolTermNodeSet : NodeSet<BoolTermNodeData>
    {
        UITransitionTerm _tranData;

        public BoolTermNodeSet(Vector2 firstPos, Vector2 nodeSize
            , UITransitionTerm tranData, List<AbstractUIBoolTerm> boolTerms
            , int colorCode = 0)
            : base(firstPos, nodeSize, colorCode)
        {
            _tranData = tranData;
            RawAddNode(ConvertUIBoolTerm2NodeData(boolTerms));
        }

        List<BoolTermNodeData> ConvertUIBoolTerm2NodeData(List<AbstractUIBoolTerm> boolTerms)
        {
            var result = new List<BoolTermNodeData>();
            for (int i = 0; i < boolTerms.Count; i++)
            {
                var data = new BoolTermNodeData(this, _tranData, _tranData._BoolTerms[i]);
                result.Add(data);
            }
            return result;
        }

        public override void AddNode()
        {
            _tranData.AddBoolTerm(BoolTermType.AwakeTime);
            var NewData = new BoolTermNodeData(this, _tranData, _tranData._BoolTerms[_tranData._BoolTerms.Count - 1]);
            base.RawAddNode(NewData);
        }

        protected override void RawRemoveNode(BoolTermNodeData data)
        {
            _tranData.RemoveBoolTerm(data._boolTerm);
            base.RawRemoveNode(data);
        }
    }

    public class BoolTermNodeData : NodeData
    {
        BoolTermType _boolTermType;

        UITransitionTerm _tranTerm;
        public AbstractUIBoolTerm _boolTerm { get; private set; }
        BoolTermNodeSet _nodeSet;

        public BoolTermNodeData(BoolTermNodeSet nodeSet, UITransitionTerm tranTerms, AbstractUIBoolTerm myBoolTerm)
        {
            _nodeSet = nodeSet;
            _tranTerm = tranTerms;
            _boolTerm = myBoolTerm;
            _boolTermType = _boolTerm.GetTermType();
        }

        public override void AbstractCallBack()
        {
            EditorGUI.BeginChangeCheck();
            _boolTermType = (BoolTermType)EditorGUILayout.EnumPopup("Type", _boolTermType);
            if (EditorGUI.EndChangeCheck())
            {
                //SetBoolTermすると持っていた_boolTermが破棄されるので新しく代入しなおす
                //自動で代入されなおすほうがいいかもしれない
                _boolTerm = _tranTerm.SetBoolTerm(_boolTerm, _boolTermType);
            }

            switch (_boolTermType)
            {
                case BoolTermType.AwakeTime:
                    {
                        AwakeTimeBoolTerm term = (AwakeTimeBoolTerm)_boolTerm;
                        term.waitLength = EditorGUILayout.FloatField("待ち時間", term.waitLength);
                        break;
                    }
                case BoolTermType.OnClickTime:
                    {
                        OnClickTimeBoolTerm term = (OnClickTimeBoolTerm)_boolTerm;
                        term._targetClickTime = EditorGUILayout.IntField("押す回数", term._targetClickTime);
                        term._ClickTargetHead = EditorGUILayout.ObjectField("ボタン", term._ClickTargetHead, typeof(Button), true) as Button;
                        break;
                    }
            }

            if (GUILayout.Button("remove"))
            {
                _nodeSet.RemoveNode(this);
            }
        }

    }
}
#endif