using AutoMapper;
using BankStatements.Models.Domain;
using BankStatements.Models.DTO.BankStatements;

namespace BankStatements.Profiles
{
    public class BankStatementProfile : Profile
    {
        public BankStatementProfile() 
        {
            CreateMap<BankStatement, BankStatementReadDTO>();
            CreateMap<BankStatementCreateDTO, BankStatement>();
        }
    }
}
