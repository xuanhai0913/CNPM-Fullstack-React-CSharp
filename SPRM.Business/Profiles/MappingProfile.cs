using AutoMapper;
using SPRM.Data.Entities;
using SPRM.Business.DTOs;
using SPRM.Data;
using System;

namespace SPRM.Business.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>().ReverseMap();
            
            // Project mappings
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<ProjectDto, Project>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<ProjectStatus>(src.Status)));
            
            // Proposal mappings
            CreateMap<Proposal, ProposalDto>().ReverseMap();
            
            // TaskItem mappings
            CreateMap<TaskItem, TaskItemDto>().ReverseMap();
            
            // Milestone mappings
            CreateMap<Milestone, MilestoneDto>().ReverseMap();
            
            // Evaluation mappings
            CreateMap<Evaluation, EvaluationDto>().ReverseMap();
            
            // Notification mappings
            CreateMap<Notification, NotificationDto>().ReverseMap();
            
            // Report mappings
            CreateMap<Report, ReportDto>().ReverseMap();
            
            // Transaction mappings
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            
            // ResearchTopic mappings
            CreateMap<ResearchTopic, ResearchTopicDto>().ReverseMap();
            
            // UserRole mappings
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
            
            // SystemSetting mappings
            CreateMap<SystemSetting, SystemSettingDto>().ReverseMap();
        }
    }
}
