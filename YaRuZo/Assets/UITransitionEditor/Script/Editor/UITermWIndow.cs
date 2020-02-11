using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
#if UNITY_EDITOR
using UnityEditor;

namespace aoji_EditorUI
{
    public class UITermWIndow : DefaultWindow
    {
        //windowの情報にアクセスしやすくしたい

        UITransitionTerm _transitionData;

        BoolTermNodeSet _boolNodeSet;
        TrrigerTermNodeSet _trrigerNodeSet;

        public void OpenWindow(UITransitionTerm tranData)
        {
            _transitionData = tranData;
            //_boolNodeSet = new BoolTermNodeSet(new Vector2(400, 50), new Vector2(200, 150),tranData, colorCode: 5);
            _boolNodeSet = new BoolTermNodeSet(
                new Vector2(400, 50), new Vector2(200, 150), _transitionData, tranData._BoolTerms, colorCode: 5
                );
            _trrigerNodeSet = new TrrigerTermNodeSet(
                new Vector2(100, 50), new Vector2(200, 150), _transitionData, colorCode: 3
                );
            ShowWindow<UITermWIndow>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("add bool term"))
            {
                _boolNodeSet.AddNode();
            }

            BeginWindows();
            //DrawNode(_trrigerNode,"トリガー",0);
            _boolNodeSet.DrawNode("ブール", 10);
            _trrigerNodeSet.DrawNode("トリガー", 0);
            EndWindows();
        }


        protected override Vector2 GetWindowSize()
        {
            return new Vector2(1080, 450);
        }
    }
}
#endif
