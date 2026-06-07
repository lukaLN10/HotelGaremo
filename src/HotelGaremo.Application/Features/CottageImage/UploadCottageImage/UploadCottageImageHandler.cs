using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageImage.UploadCottageImage;

public class UploadCottageImageHandler : IRequestHandler<UploadCottageImageCommand, UploadCottageImageResponse>
{
    private readonly IDataContext _context;
    private readonly IImageService _imageService;

    public UploadCottageImageHandler(IDataContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    public async Task<UploadCottageImageResponse> Handle(UploadCottageImageCommand request, CancellationToken cancellationToken)
    {
        var cottage = await _context.Cottages
            .FirstOrDefaultAsync(x => x.Id == request.CottageId, cancellationToken);

        if (cottage == null)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        var imageUrl = await _imageService.UploadImageAsync(request.File, "cottages");

        var cottageImage = new HotelGaremo.Domain.Entities.CottageImage
        {
            ImageURL = imageUrl,
            CottageId = request.CottageId,
            CottageRoomId = null
        };

        _context.CottageImages.Add(cottageImage);
        await _context.SaveChangesAsync(cancellationToken);

        return new UploadCottageImageResponse
        {
            Id = cottageImage.Id,
            ImageURL = cottageImage.ImageURL,
            CottageId = request.CottageId
        };
    }
}
