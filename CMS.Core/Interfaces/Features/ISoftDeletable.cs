using System;
using System.Collections.Generic;

namespace CMS.Core.Features
{
	public interface ISoftDeletable
	{
		bool Deleted { get; }
	}
}
