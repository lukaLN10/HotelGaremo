using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelGaremo.Application.Features.CottageImage.UploadCottageImage;

public record UploadCottageImageCommand(
    IFormFile File,
    int CottageId
) : IRequest<UploadCottageImageResponse>;
