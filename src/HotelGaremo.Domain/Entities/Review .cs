using HotelGaremo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Domain.Entities;

public class Review : BaseEntity
{
    public string Comment { get; private set; }
    public int Rating { get; private set; }
    public int UserId { get; private set; }
    public User? User { get; private set; }

    private Review() { }

    public Review(string comment, int rating, int userId)
    {
        Comment = comment;
        Rating = rating;
        UserId = userId;
    }

    public void Update(string comment, int rating)
    {
        Comment = comment;
        Rating = rating;
        SetUpdatedAt();
    }
}
