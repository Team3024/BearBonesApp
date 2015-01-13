using System;
using System.Collections.Generic;

namespace BearBones
{
	public class neo4jData
	{
		public List<string> columns { get; set; }
		public List<List<Dictionary<string,dynamic>>> data { get; set; }
	}
}

