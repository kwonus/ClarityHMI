﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuelleHMI.XGeneration
{
    class XGenJava : XGen
	{
		//	Java code-generator:
		public XGenJava()
		{
			;
		}
		protected override string additionalImports()
		{
			return "";
		}
		protected override string QImport(String module)
		{
			if (module != null && module.Length > 0 && module.ToLower().StartsWith("hmi") == true)
			{
				string line;

				line = "import " + module + ";";
				return line + "\n";
			}
			return "";
		}
		protected override string constructor(Dictionary<string, string> accessible)
		{
			return "";
		}
		protected override string getterAndSetter(string name, string type)
		{
			string variable = "\t\tpublic " + type + "\t" + name + ";\n";
			return variable;
		}
		protected override string export(Type type)
		{
			string file = "";
			try
			{
				String parent = null; // QClass(c.BaseType);

				foreach (string k in accessible.Keys)
				{
					string t = accessible[k];
					if (t == null)
						continue;
					file += QImport(t);
				}
				if (parent != null)
					file += QImport(parent);

				string qname = QClass(type);
				string classname = QClass(type) != null ? qname : "UNKNOWN";
				file += ("\n\tclass " + classname);
				if (parent != null)
				{
					file += " extends ";
					file += parent;
				}
				file += " {\n";
				foreach (string p in accessible.Keys)
				{
					string t = accessible[p];
					file += getterAndSetter(p, t);
				}
				file += "\n\t}";
			}
			catch (Exception e)
			{
				file = "ERROR: " + e.Message;
			}
			return file;
		}
	}
}
