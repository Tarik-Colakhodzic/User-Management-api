using System;

namespace Application.Common.Models
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime? DateModified { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}