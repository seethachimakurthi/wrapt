namespace RecipeManagement.Domain.RolePermissions.Features;

using RecipeManagement.Domain.RolePermissions.Dtos;
using RecipeManagement.Domain.RolePermissions.Services;
using AutoMapper;
using MediatR;

public static class GetRolePermission
{
    public class RolePermissionQuery : IRequest<RolePermissionDto>
    {
        public readonly Guid Id;

        public RolePermissionQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<RolePermissionQuery, RolePermissionDto>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;

        public Handler(IRolePermissionRepository rolePermissionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<RolePermissionDto> Handle(RolePermissionQuery request, CancellationToken cancellationToken)
        {
            var result = await _rolePermissionRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<RolePermissionDto>(result);
        }
    }
}