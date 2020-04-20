using System.Collections.Generic;

namespace CMS.Core.SharedKernel
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// Transient objects are not associated with an item already in storage. For instance,
        /// a Product entity is transient if its Id is 0.
        /// </summary>
        public virtual bool IsTransientRecord()
        {
            return Id == 0;
        }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}