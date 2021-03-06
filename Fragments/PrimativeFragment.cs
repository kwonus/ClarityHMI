﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuelleHMI.Fragments
{
    public class PrimativeFragment: Fragment
    {
		public PrimativeFragment(Actions.Action segment, string fragment, byte adjacency, byte group)
			 : base(segment, fragment, adjacency, group)
		{
			if (this.segment == null)
			{
				segment.Notify("error", "Major design/implementation error: aborting!");
				return;
			}
		}
	}
}
