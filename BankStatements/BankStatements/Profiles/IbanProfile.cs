using AutoMapper;
using BankStatements.Models.Domain;
using BankStatements.Models.DTO.Ibans;

namespace BankStatements.Profiles
{
    public class IbanProfile : Profile
    {
        public IbanProfile() 
        {
            CreateMap<Iban, IbanReadDTO>();
        }
    }
}
