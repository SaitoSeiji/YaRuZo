using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace aoji_EditorUI
{
    public class SimpleNodeSet : NodeSet<SimpleNodeData>
    {
        public SimpleNodeSet(Vector2 firstPos, Vector2 nodeSize, int colorCode = 0)
            : base(firstPos, nodeSize, colorCode)
        {
        }
        public SimpleNodeSet(Vector2 firstPos, Vector2 nodeSize
            , bool arrangeX, int arrangeCount, int colorCode = 0)
            : base(firstPos, nodeSize, arrangeX, arrangeCount, colorCode)
        {
        }

        public override void AddNode()
        {
        }
    }

    public class SimpleNodeData : NodeData
    {
        public override void AbstractCallBack()
        {
        }
    }
}