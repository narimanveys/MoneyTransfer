using AutoMapper;
using InternalMoneyTransfer.Core.DataModel;
using InternalMoneyTransfer.Core.Dtos;

namespace InternalMoneyTransfer.Core
{
    public class MappingProfile : Profile
    {
        #region Constructor

        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<UserAccount, UserAccountDto>();
            CreateMap<UserAccountDto, UserAccount>();

            CreateMap<Transaction, TransactionDto>()
                .ForMember(m => m.Creditor, opts => opts.MapFrom(src => src.Creditor.User.FullName))
                .ForMember(m => m.Debtor, opts => opts.MapFrom(src => src.Debtor.User.FullName));
            CreateMap<TransactionDto, Transaction>()
                .ForMember(m => m.Creditor, opts => opts.Ignore())
                .ForMember(m => m.Debtor, opts => opts.Ignore());
        }

        #endregion
    }
}