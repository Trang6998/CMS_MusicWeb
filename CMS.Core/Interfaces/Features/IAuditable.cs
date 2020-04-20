using System;

namespace CMS.Core.Features
{
	public interface IAuditable
	{
		/// <summary>
		/// Gets or sets the date and time of entity creation
		/// </summary>
		DateTime CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the date and time of entity update
		/// </summary>
		DateTime UpdatedOn { get; set; }
	}
}
