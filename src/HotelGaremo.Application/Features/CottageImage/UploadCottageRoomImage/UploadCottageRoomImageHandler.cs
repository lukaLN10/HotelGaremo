using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageImage.UploadCottageRoomImage;

public class UploadCottageRoomImageHandler : IRequestHandler<UploadCottageRoomImageCommand, UploadCottageRoomImageResponse>
{
    private readonly IDataContext _context;
    private readonly IImageService _imageService;

    public UploadCottageRoomImageHandler(IDataContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    public async Task<UploadCottageRoomImageResponse> Handle(UploadCottageRoomImageCommand request, CancellationToken cancellationToken)
    {
        var room = await _context.CottageRooms
            .FirstOrDefaultAsync(x => x.Id == request.CottageRoomId, cancellationToken);

        if (room == null)
            throw new BadRequestException("ოთახი ვერ მოიძებნა.");

        var imageUrl = await _imageService.UploadImageAsync(request.File, "cottage-rooms");

        var cottageImage = new HotelGaremo.Domain.Entities.CottageImage
        {
            ImageURL = imageUrl,
            CottageRoomId = request.CottageRoomId,
            CottageId = null
        };

        _context.CottageImages.Add(cottageImage);
        await _context.SaveChangesAsync(cancellationToken);

        return new UploadCottageRoomImageResponse
        {
            Id = cottageImage.Id,
            ImageURL = cottageImage.ImageURL,
            CottageRoomId = request.CottageRoomId
        };
    }
}
