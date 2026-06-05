using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.CreateReview;

public record CreateReviewCommand(
    string Comment,
    int Rating
) : IRequest<CreateReviewResponse>;