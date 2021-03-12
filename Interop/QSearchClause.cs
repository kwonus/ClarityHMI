﻿using MessagePack;
using QuelleHMI.Fragments;
using QuelleHMI.Verbs;

namespace QuelleHMI
{
    [MessagePackObject]
    public class QSearchClause : IQuelleSearchClause
    {
        public QSearchClause() { /*for msgpack*/ }

        [Key(1)]
        public string syntax { get; set; }
        [IgnoreMember]
        public IQuelleSearchFragment[] fragments
        {
            get => this.qfragments;
            set
            {
                this.qfragments = new QSearchFragment[value.Length];
                int i = 0;
                foreach (var frag in value)
                    this.qfragments[i] = new QSearchFragment(value[i++]);
            }
        }
        [Key(2)]
        public QSearchFragment[] qfragments { get; set; }
        [Key(3)]
        public string segment { get; set; }
        [Key(4)]
        public HMIClause.HMIPolarity polarity { get; }

        public QSearchClause(IQuelleSearchClause iclause)
        {
            this.syntax = iclause.syntax;
            this.qfragments = iclause.fragments != null ? new QSearchFragment[iclause.fragments.Length] : null;
            this.segment = iclause.segment;
            this.polarity = iclause.polarity;

            if (this.qfragments != null)
                for (int i = 0; i < iclause.fragments.Length; i++)
                    this.qfragments[i] = new QSearchFragment(iclause.fragments[i]);
        }
    }
}
