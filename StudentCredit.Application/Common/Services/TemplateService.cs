﻿using FileStorageNetCore.Api;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Common.Interfaces;
using StudentCredit.Data.Common.Models;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Application.Common.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IAppDbContext context;
        private readonly BlobStorageService fileStorageRepository;

        public TemplateService(
            IAppDbContext context,
            BlobStorageService fileStorageRepository
            )
        {
            this.context = context;
            this.fileStorageRepository = fileStorageRepository;
        }

        public async Task<byte[]> GetTemplateAsync(string alias)
        {
            var file = await this.context.Set<FileTemplate>()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Alias == alias);

            if (file == null)
            {
                throw new ArgumentNullException($"Missing template");
            }

            var template = await this.fileStorageRepository.GetBytes(file.Key, file.DbId);
            return template;
        }
    }
}
