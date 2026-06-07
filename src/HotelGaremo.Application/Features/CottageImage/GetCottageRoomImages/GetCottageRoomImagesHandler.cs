using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageImage.GetCottageRoomImages;

public class GetCottageRoomImagesHandler : IRequestHandler<GetCottageRoomImagesQuery, List<GetCottageRoomImagesResponse>>
{
    private readonly IDataContext _context;

    public GetCottageRoomImagesHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<List<GetCottageRoomImagesResponse>> Handle(GetCottageRoomImagesQuery request, CancellationToken cancellationToken)
    {
        var roomExists = await _context.CottageRooms
            .AnyAsync(x => x.Id == request.CottageRoomId, cancellationToken);

        if (!roomExists)
            throw new BadRequestException("კოტეჯის ოთახი ვერ მოიძებნა.");

        return await _context.CottageImages
            .Where(x => x.CottageRoomId == request.CottageRoomId)
            .Select(x => new GetCottageRoomImagesResponse
            {
                Id = x.Id,
                ImageURL = x.ImageURL
            })
            .ToListAsync(cancellationToken);
    }
}
