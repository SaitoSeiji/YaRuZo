using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using aojiru_UI;
#if UNITY_EDITOR
using UnityEditor;

namespace aoji_EditorUI
{
    public class TrrigerTermNodeSet : NodeSet<TrrigerTermNodeData>
    {

        public TrrigerTermNodeSet(Vector2 firstPos, Vector2 nodeSize
            , UITransitionTerm term
            , int colorCode = 0)
            : base(firstPos, nodeSize, colorCode)
        {
            RawAddNode(new TrrigerTermNodeData(this, term));
        }

        public override void AddNode()
        {
            throw new System.NotImplementedException();
        }

        protected override void RawRemoveNode(TrrigerTermNodeData data)
        {
            base.RawRemoveNode(data);
        }
    }

    public class TrrigerTermNodeData : NodeData
    {
        TrrigerType _trrigerType;


        UITransitionTerm _tranTerm;
        AbstractUITrrigerTerm _trrigerTerm { get { return _tranTerm._TrrigerTerm; } }
        TrrigerTermNodeSet _nodeSet;

        public TrrigerTermNodeData(TrrigerTermNodeSet nodeSet, UITransitionTerm tranTerm)
        {
            _nodeSet = nodeSet;
            _tranTerm = tranTerm;
            if (_trrigerTerm == null)
            {
                _tranTerm.AddTrrigerTerm(TrrigerType.None);
            }

            _trrigerType = _trrigerTerm.GetTrrigerType();

        }

        public override void AbstractCallBack()
        {
            EditorGUI.BeginChangeCheck();
            _trrigerType = (TrrigerType)EditorGUILayout.EnumPopup("Type", _trrigerType);
            if (EditorGUI.EndChangeCheck())
            {
                _tranTerm.SetTrrigerTerm(_trrigerTerm, _trrigerType);
            }

            switch (_trrigerType)
            {
                case TrrigerType.None:
                    break;
                case TrrigerType.Onclick:
                    {
                        OncliclUITrrigerTerm term = (OncliclUITrrigerTerm)_trrigerTerm;
                        term._HeadButton = EditorGUILayout.ObjectField("ボタン", term._HeadButton, typeof(Button), true) as Button;
                        break;
                    }
            }
        }
    }
}
#endif
