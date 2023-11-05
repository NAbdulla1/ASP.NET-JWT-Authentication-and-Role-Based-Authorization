using Authentication_and_Authorization.Core.DTOs;
using Authentication_and_Authorization.Core.Extensions;
using Authentication_and_Authorization.Core.Models;
using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Core.QueryBuilders;

namespace Authentication_and_Authorization.Core.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateCustomer(UserRegisterDTO user);
        Task<UserDTO?> Get(long id);
        Task<PageDTO<UserDTO>> GetAll(IndexDTO indexData, string? searchTerm);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfOfWork;
        private readonly IUserQueryBuilder _queryBuilder;

        public UserService(IUnitOfWork unitOfOfWork, IUserQueryBuilder queryBuilder)
        {
            _unitOfOfWork = unitOfOfWork;
            _queryBuilder = queryBuilder;
        }

        public async Task<UserDTO> CreateCustomer(UserRegisterDTO user)
        {
            var newUser = user.GetUser(Data.Entities.UserType.Customer);

            _unitOfOfWork.Users.Add(newUser);
            await _unitOfOfWork.CommitAsync();

            return newUser.ToUserDTO();
        }

        public async Task<UserDTO?> Get(long id)
        {
            var user = await _unitOfOfWork.Users.GetOneAsync(id);

            return user?.ToUserDTO();
        }

        public async Task<PageDTO<UserDTO>> GetAll(IndexDTO indexData, string? searchTerm)
        {
            int pageIndex = indexData.Page ?? 1;
            int pageSize = indexData.PageSize ?? 10;

            var queryDetails = _queryBuilder.BuildGetAllQuery(indexData, searchTerm);
            var pageData = await _unitOfOfWork.Users.GetAllAsync(queryDetails, pageIndex, pageSize);

            return new PageDTO<UserDTO>
            (
                pageIndex,
                pageData.Total,
                pageData.Entities.Select(user => user.ToUserDTO())
            );
        }
    }
}
