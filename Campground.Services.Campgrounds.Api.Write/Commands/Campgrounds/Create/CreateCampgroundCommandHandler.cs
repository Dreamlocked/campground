﻿using AutoMapper;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Storage;
using MediatR;
using System.Security.Claims;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Create
{
    internal sealed class CreateCampgroundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBlobStorageService blobStorageService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCampgroundCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IBlobStorageService _blobStorageService = blobStorageService; 
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Unit> Handle(CreateCampgroundCommand command, CancellationToken cancellationToken)
        {
            var campground = _mapper.Map<Domain.Entities.Campground>(command);
            campground.HostId = Guid.Parse(_httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value);

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

            campground.Images = (await Task.WhenAll(imageTasks)).ToList();

            await _unitOfWork.CampgroundRepository.AddAsync(campground);
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}
