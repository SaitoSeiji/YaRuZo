using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace aoji_EditorUI
{
    public abstract class NodeSet<T>
        where T : NodeData
    {
        Vector2 _firstPos;
        Vector2 _nodeSize;
        bool _arrangeX = true;//trueならx方向に並べる
        int _arrangeCount = 5;//並べる最大個数
        int _colorCode = 0;

        protected List<T> _nodeList = new List<T>();

        #region コンストラクタ
        public NodeSet(Vector2 firstPos, Vector2 nodeSize, int colorCode = 0)
        {
            _firstPos = firstPos;
            _nodeSize = nodeSize;
            _arrangeX = true;
            _arrangeCount = 5;
            _colorCode = colorCode;
        }
        public NodeSet(Vector2 firstPos, Vector2 nodeSize
            , bool arrangeX, int arrangeCount, int colorCode = 0)
        {
            _firstPos = firstPos;
            _nodeSize = nodeSize;
            _arrangeX = arrangeX;
            _arrangeCount = arrangeCount;
            _colorCode = colorCode;
        }
        #endregion
        #region rect関係


        //public void AddRect()
        //{
        //    Rect result = new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
        //    result = CaliculatePositionRect(result, _arrangeX, _nodeList.Count);
        //    _rectList.Add(result);
        //}

        //public void RemoveRect(int index)
        //{
        //    _rectList.RemoveAt(index);
        //}

        Rect GetRect(int index)
        {
            return _nodeList[index].GetRect();
        }

        void SetRect(int index, Rect rect)
        {
            //_rectList[index] = rect;
            _nodeList[index].SetRect(rect);
        }

        void ResetRect()
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                //_rectList[i] = new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
                //_rectList[i] = CaliculatePositionRect(_rectList[i], _arrangeX, i);
                Rect newRect = CaliculateRect(GetInitRect(), _arrangeX, i);
                _nodeList[i].SetRect(newRect);
            }
        }
        Rect GetInitRect()
        {
            return new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
        }

        Rect CaliculateRect(Rect firstPos, bool arrangeX, int nodeCount)
        {
            Rect result = firstPos;
            float nodeInterval = 5;
            int stepCount = nodeCount / _arrangeCount;//段の数
            int pieceCount = nodeCount % _arrangeCount;//個数の位置
            result.x += (_nodeSize.x + nodeInterval) * ((arrangeX) ? pieceCount : stepCount);
            result.y += (_nodeSize.y + nodeInterval) * ((arrangeX) ? stepCount : pieceCount);
            return result;
        }

        #endregion
        string GetColorCodeString()
        {
            return "flow node " + _colorCode;
        }
        #region node関連

        protected void RawAddNode(List<T> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                RawAddNode(data[i]);
            }
        }

        protected void RawAddNode(T nodeData)
        {
            //ノードのrectの計算
            Rect makeRect = CaliculateRect(GetInitRect(), _arrangeX, _nodeList.Count);
            //ノードの登録
            nodeData.SetRect(makeRect);
            _nodeList.Add(nodeData);
        }

        protected void RawRemoveNode(int index)
        {
            RawRemoveNode(_nodeList[index]);
        }
        protected virtual void RawRemoveNode(T data)
        {
            _nodeList.Remove(data);
        }


        #endregion


        #region public関数

        public abstract void AddNode();

        public void RemoveNode(int index)
        {
            RemoveNode(_nodeList[index]);
        }

        public void RemoveNode(T data)
        {
            RawRemoveNode(data);
            ResetRect();
        }

        public void DrawNode(string name, int numberSet)
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                Rect newRect = GUI.Window(i + numberSet, GetRect(i), _nodeList[i].CallBack, name + i, GetColorCodeString());
                SetRect(i, newRect);
            }
        }
        #endregion
    }

    public abstract class NodeData
    {
        Rect _myRect;

        public NodeData()
        {
        }

        public void SetRect(Rect rect)
        {
            _myRect = rect;
        }
        public Rect GetRect()
        {
            return _myRect;
        }

        public void CallBack(int id)
        {
            AbstractCallBack();
        }
        public abstract void AbstractCallBack();

    }
}