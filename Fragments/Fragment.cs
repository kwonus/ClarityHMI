﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QuelleHMI
{
	public abstract class Fragment
	{
		protected Actions.Action segment;

		public string text { get; protected set; }
		protected UInt32 sequence;  // Sequence number of fragment

		protected Fragment(Actions.Action segment, string fragment, uint sequence)
		{
			this.text = fragment != null ? fragment.Trim() : "";
			this.segment = segment;
			this.sequence = sequence;
		}
	}
}