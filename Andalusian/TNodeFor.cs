﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equus.Calabrese;
using Equus.Shire;
using Equus.Horse;

namespace Equus.Andalusian
{

    /// <summary>
    /// For loop class
    /// </summary>
    public sealed class TNodeFor : TNode
    {

        private int _Current;
        private int _Begin;
        private int _End;
        private int _ptrControll;

        public TNodeFor(TNode Parent, int Begin, int End, MemoryStruct Heap, int CellPointer)
            : base(Parent)
        {
            this._Begin = Begin;
            this._End = End;
            this._Heap = Heap;
            this._ptrControll = CellPointer;
        }
        
        public override long Writes
        {
            get
            {
                return this._Children.Sum<TNode>((x) => { return x.Writes; });
            }
            protected set
            {
                base.Writes = value;
            }
        }

        public override void BeginInvoke()
        {
            base.BeginInvoke();
            this.BeginInvokeChildren();
        }

        public override void EndInvoke()
        {
            base.EndInvoke();
            this.EndInvokeChildren();
        }

        public override void Invoke()
        {

            Cell c = new Cell((long)this._Begin);
            for (this._Current = this._Begin; this._Current <= this._End; this._Current++)
            {

                // Assign the controll variable //
                this._Heap.Scalars[this._ptrControll] = c;
                c++;

                // Invoke children //
                foreach (TNode node in this._Children)
                {

                    // Invoke //
                    node.Invoke();

                    // Check for the raise state == 1 or 2//
                    if (node.Raise == 1 || node.Raise == 2)
                        return;

                }

            }

        }

        public override string Message()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("For-Loop");
            for (int i = 0; i < this._Children.Count; i++)
            {

                if (i != this._Children.Count - 1)
                    sb.AppendLine('\t' + this._Children[i].Message());
                else
                    sb.Append('\t' + this._Children[i].Message());

            }
            return sb.ToString();

        }

        public override TNode CloneOfMe()
        {
            TNodeFor node = new TNodeFor(this.Parent, this._Begin, this._End, this._Heap, this._ptrControll);
            foreach (TNode t in this._Children)
                node.AddChild(t.CloneOfMe());
            return node;
        }

    }

}
