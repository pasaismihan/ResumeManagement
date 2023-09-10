using System;
using AutoMapper;
using backend.Core.Dtos.Candidate;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Job;
using backend.Core.Entities;

namespace backend.Core.AutoMapperConfig
{
	public class AutoMapperConfigProfile : Profile
	{
		public AutoMapperConfigProfile()
		{
			// Company
			CreateMap<CompanyCreateDto, Company>();
			CreateMap<Company, CompanyGetDto>();
			// Job
			CreateMap<JobCreateDto, Job>();
			CreateMap<Job, JobGetDto>()
				.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name)); // Job icerisinde bulunan Company Turu sayesinde Company entitisine giderek Name aldik
																													   // bunu da JobGetDto daki CompanyName ye donusturuyoruz
			// Canditate
			CreateMap<CandidateCreateDto, Candidate>();
			CreateMap<Candidate, CandidateGetDto>().
				ForMember(des => des.JobTitle, opt => opt.MapFrom(src => src.Job.Title));

		}
	}
}

