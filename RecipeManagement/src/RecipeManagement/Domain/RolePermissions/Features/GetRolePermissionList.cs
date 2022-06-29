namespace RecipeManagement.Domain.RolePermissions.Features;

using RecipeManagement.Domain.RolePermissions.Dtos;
using RecipeManagement.Domain.RolePermissions.Services;
using RecipeManagement.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetRolePermissionList
{
    public class RolePermissionListQuery : IRequest<PagedList<RolePermissionDto>>
    {
        public readonly RolePermissionParametersDto QueryParameters;

        public RolePermissionListQuery(RolePermissionParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<RolePermissionListQuery, PagedList<RolePermissionDto>>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IRolePermissionRepository rolePermissionRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<RolePermissionDto>> Handle(RolePermissionListQuery request, CancellationToken cancellationToken)
        {
            var collection = _rolePermissionRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<RolePermissionDto>(_mapper.ConfigurationProvider);

            return await PagedList<RolePermissionDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}