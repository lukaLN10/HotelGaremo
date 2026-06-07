using MediatR;

namespace HotelGaremo.Application.Features.CottageImage.DeleteCottageImage;

public record DeleteCottageImageCommand(int ImageId) : IRequest<DeleteCottageImageResponse>;
