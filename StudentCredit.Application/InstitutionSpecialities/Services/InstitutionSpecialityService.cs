using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Common.Extensions;
using StudentCredit.Application.InstitutionSpecialities.Dtos;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.InstitutionSpecialities.Services
{
	public class InstitutionSpecialityService : IScopedService
	{

		private readonly IAppDbContext context;

		public InstitutionSpecialityService(IAppDbContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<SpecialityInformationDto>> GetInstitutionSpecialities(SpecialityFilterDto filter)
		{
			var institutionSpecialities = this.context.Set<InstitutionSpeciality>()
				.Include(s => s.EducationalForm)
				.Include(s => s.ResearchArea)
				.Include(s => s.Speciality)
					.ThenInclude(q => q.EducationalQualification)
				.AsNoTracking();

			institutionSpecialities = filter.WhereBuilder(institutionSpecialities);
			institutionSpecialities = filter.OrderBuilder(institutionSpecialities);

			if (filter.Limit.HasValue && filter.Offset.HasValue)
			{
				institutionSpecialities = institutionSpecialities.ApplyPagination(filter.Offset.Value, filter.Limit.Value);
			}

			var result = await institutionSpecialities.Select(i =>
				new SpecialityInformationDto(i.Speciality.Id, i.Speciality.Name, i.Duration.Value, i.EducationalForm, i.Speciality.EducationalQualification, i.Institution, i.ResearchArea))
				.ToListAsync();

			return result;
		}

		public async Task SaveInstitution(InstitutionDto dto)
		{
			var model = await context.Set<Institution>()
				.Include(e => e.InstitutionSpecialities)
					.ThenInclude(s => s.Speciality)
				.SingleOrDefaultAsync(e => e.ExternalId == dto.Id);
			if (model == null)
			{
				model = new Institution(dto.Name, dto.IsActive, dto.InstitutionType, dto.Id, dto.RootId, dto.ParentId, dto.Level, dto.Uic);
				context.Entry(model).State = EntityState.Added;
			}
			else
			{
				model.Update(dto.Name, dto.IsActive, dto.InstitutionType, dto.Id, dto.RootId, dto.ParentId, dto.Level, dto.Uic);
				context.Entry(model).State = EntityState.Modified;
			}

			var specialities = await context.Set<Speciality>()
				.AsNoTracking()
				.ToListAsync();

			var forRemove = model.InstitutionSpecialities
				.Where(e => !dto.InstitutionSpecialities.Any(d => d.Id == e.ExternalId))
				.ToList();
			if (forRemove.Any())
			{
				forRemove.ForEach(fr =>
				{
					context.Entry(fr).State = EntityState.Deleted;
				});
			}

			foreach (var dtoInstitutionSpeciality in dto.InstitutionSpecialities)
			{
				var speciality = model.InstitutionSpecialities
					.Select(e => e.Speciality)
					.Distinct()
					.SingleOrDefault(e => e?.ExternalId == dtoInstitutionSpeciality.SpecialityId);

				if (speciality == null)
				{
					speciality = specialities.SingleOrDefault(e => e.ExternalId == dtoInstitutionSpeciality.SpecialityId);
				}

				if (speciality == null)
				{
					speciality = new Speciality(
						dtoInstitutionSpeciality.SpecialityId,
						dtoInstitutionSpeciality.Speciality.Name,
						dtoInstitutionSpeciality.SpecialityId,
						dtoInstitutionSpeciality.Speciality.EducationalQualificationId,
						dtoInstitutionSpeciality.Speciality.IsActive
					);

					context.Entry(speciality).State = EntityState.Added;
				}
				else
				{
					speciality.Update(
						dtoInstitutionSpeciality.Speciality.Name,
						dtoInstitutionSpeciality.SpecialityId,
						dtoInstitutionSpeciality.Speciality.EducationalQualificationId,
						dtoInstitutionSpeciality.Speciality.IsActive
					);
				}

				var institutionSpeciality = model.InstitutionSpecialities
					.SingleOrDefault(e => e.ExternalId == dtoInstitutionSpeciality.Id);
				if (institutionSpeciality != null)
				{
					institutionSpeciality.SpecialityId = speciality.Id;
					institutionSpeciality.Speciality = speciality;
					institutionSpeciality.EducationalFormId = dtoInstitutionSpeciality.EducationalFormId;
					institutionSpeciality.Duration = dtoInstitutionSpeciality.Duration;
					institutionSpeciality.IsActive = dtoInstitutionSpeciality.IsActive;
				}
				else
				{
					var spec = new InstitutionSpeciality
					{
						Id = dtoInstitutionSpeciality.Id,
						ExternalId = dtoInstitutionSpeciality.Id,
						InstitutionId = dtoInstitutionSpeciality.InstitutionId,
						SpecialityId = speciality.Id,
						EducationalFormId = dtoInstitutionSpeciality.EducationalFormId,
						Duration = dtoInstitutionSpeciality.Duration,
						IsActive = dtoInstitutionSpeciality.IsActive
					};

					context.Entry(spec).State = EntityState.Added;
				}
			}

			await context.SaveChangesAsync();
		}
	}
}
