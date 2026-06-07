using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelGaremo.Application.Features.CottageImage.UploadCottageRoomImage;

public record UploadCottageRoomImageCommand(
    IFormFile File,
    int CottageRoomId
) : IRequest<UploadCottageRoomImageResponse>;
