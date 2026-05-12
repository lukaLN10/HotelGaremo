using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; protected set; } 
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    public void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
