using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageImage.GetCottageImages;

public class GetCottageImagesHandler : IRequestHandler<GetCottageImagesQuery, List<GetCottageImagesResponse>>
{
    private readonly IDataContext _context;

    public GetCottageImagesHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<List<GetCottageImagesResponse>> Handle(GetCottageImagesQuery request, CancellationToken cancellationToken)
    {
        var cottageExists = await _context.Cottages
            .AnyAsync(x => x.Id == request.CottageId, cancellationToken);

        if (!cottageExists)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        return await _context.CottageImages
            .Where(x => x.CottageId == request.CottageId)
            .Select(x => new GetCottageImagesResponse
            {
                Id = x.Id,
                ImageURL = x.ImageURL
            })
            .ToListAsync(cancellationToken);
    }
}
