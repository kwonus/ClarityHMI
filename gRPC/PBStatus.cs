﻿using QuelleHMI.Controls;
using QuelleHMI.Verbs;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QuelleHMI
{
    [DataContract]
    public class PBStatusRequest : IQuelleStatusRequest
    {
        public PBStatusRequest(IQuelleStatusRequest irequest)
        {
            ;
        }
    }
    [DataContract]
    public class PBStatusResult: PBQuelleResult, IQuelleStatusResult
    {
        [DataMember(Order = 1)]
        public Guid[] sessions { get; set; }

        public PBStatusResult(IQuelleStatusResult iresult): base((IQuelleResult)iresult)
        {
            this.sessions = iresult.sessions;
        }
    }
}