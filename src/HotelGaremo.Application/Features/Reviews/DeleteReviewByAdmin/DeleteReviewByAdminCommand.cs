using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.DeleteReviewByAdmin;

public record DeleteReviewByAdminCommand(int ReviewId) : IRequest<DeleteReviewByAdminResponse>;
