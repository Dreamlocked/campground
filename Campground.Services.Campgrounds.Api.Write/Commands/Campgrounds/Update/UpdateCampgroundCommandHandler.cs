using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Delete;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using Campground.Services.Campgrounds.Infrastructure.Storage;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Update
{
    internal sealed class UpdateCampgroundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdateCampgroundCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IBlobStorageService _blobStorageService = blobStorageService;
        public async Task<Unit> Handle(UpdateCampgroundCommand command, CancellationToken cancellationToken)
        {
            var campground = _mapper.Map<Domain.Entities.Campground>(command);

            var imageTasks = command.Images.Select(async image =>
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var newImage = new Image()
                {
                    Id = Guid.NewGuid(),
                    CampgroundsId = campground.Id,
                    Filename = image.FileName,
                    Url = await _blobStorageService.UploadFileAsync(campground.Id.ToString().Split('-')[0], image.FileName, memoryStream.ToArray())
                };
                return newImage;
            });

            // TODO: This is a bug. The actualImages property is not being set in the command object.

            var imagesToDelete = campground.Images.Where(image => !command.actualImages.Contains(image.Id)).ToList();

            imagesToDelete.ForEach(async image => await _blobStorageService.DeleteFileAsync(campground.Id.ToString().Split('-')[0], image.Filename!));

            campground.Images = (await Task.WhenAll(imageTasks)).ToList();

            await _unitOfWork.ImageRepository.DeleteManyAsync(image => imagesToDelete.Contains(image));
            await _unitOfWork.CampgroundRepository.UpdateAsync(campground);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
