using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageImage.DeleteCottageImage;

public class DeleteCottageImageHandler : IRequestHandler<DeleteCottageImageCommand, DeleteCottageImageResponse>
{
    private readonly IDataContext _context;
    private readonly IImageService _imageService;

    public DeleteCottageImageHandler(IDataContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    public async Task<DeleteCottageImageResponse> Handle(DeleteCottageImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _context.CottageImages
            .FirstOrDefaultAsync(x => x.Id == request.ImageId, cancellationToken);

        if (image == null)
            throw new BadRequestException("სურათი ვერ მოიძებნა.");

        _imageService.DeleteImage(image.ImageURL);

        _context.CottageImages.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteCottageImageResponse { Message = "სურათი წარმატებით წაიშალა." };
    }
}
