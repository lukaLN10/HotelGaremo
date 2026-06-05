using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.GetReviews;

public class GetReviewsResponse
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public string UserName { get; set; }
}