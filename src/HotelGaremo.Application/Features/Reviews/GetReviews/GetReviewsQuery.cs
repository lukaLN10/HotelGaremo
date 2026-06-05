using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.GetReviews;

public record GetReviewsQuery : IRequest<List<GetReviewsResponse>>;
