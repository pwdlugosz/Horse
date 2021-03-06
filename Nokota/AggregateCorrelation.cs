﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equus.Horse;
using Equus.Calabrese;

namespace Equus.Nokota
{
    
    public sealed class AggregateCorrelation : AggregateStatCo
    {

        public AggregateCorrelation(FNode X, FNode Y, FNode W, Predicate F)
            : base(X, Y, W, F)
        {
        }

        public AggregateCorrelation(FNode X, FNode Y, Predicate F)
            : base(X, Y, F)
        {
        }

        public AggregateCorrelation(FNode X, FNode Y, FNode W)
            : base(X, Y, W)
        {
        }

        public AggregateCorrelation(FNode X, FNode Y)
            : base(X, Y)
        {
        }

        public override Cell Evaluate(Record WorkData)
        {

            if (WorkData[0].IsZero == true) return new Cell(this.ReturnAffinity);
            Cell avgx = WorkData[1] / WorkData[0];
            Cell stdx = Cell.Sqrt(WorkData[2] / WorkData[0] - avgx * avgx);
            Cell avgy = WorkData[3] / WorkData[0];
            Cell stdy = Cell.Sqrt(WorkData[4] / WorkData[0] - avgy * avgy);
            Cell covxy = WorkData[5] / WorkData[0] - avgx * avgy;
            if (stdx.IsZero || stdy.IsZero) return new Cell(this.ReturnAffinity);
            return covxy / (stdx * stdy);

        }

        public override Aggregate CloneOfMe()
        {
            return new AggregateCorrelation(this._MapX.CloneOfMe(), this._MapY.CloneOfMe(), this._MapW.CloneOfMe(), this._F.CloneOfMe());
        }

    }

}
