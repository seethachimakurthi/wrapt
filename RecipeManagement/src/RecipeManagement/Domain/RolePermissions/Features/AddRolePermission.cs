namespace RecipeManagement.Domain.RolePermissions.Features;

using RecipeManagement.Domain.RolePermissions.Services;
using RecipeManagement.Domain.RolePermissions;
using RecipeManagement.Domain.RolePermissions.Dtos;
using RecipeManagement.Services;
using AutoMapper;
using MediatR;

public static class AddRolePermission
{
    public class AddRolePermissionCommand : IRequest<RolePermissionDto>
    {
        public readonly RolePermissionForCreationDto RolePermissionToAdd;

        public AddRolePermissionCommand(RolePermissionForCreationDto rolePermissionToAdd)
        {
            RolePermissionToAdd = rolePermissionToAdd;
        }
    }

    public class Handler : IRequestHandler<AddRolePermissionCommand, RolePermissionDto>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IRolePermissionRepository rolePermissionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RolePermissionDto> Handle(AddRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var rolePermission = RolePermission.Create(request.RolePermissionToAdd);
            await _rolePermissionRepository.Add(rolePermission, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var rolePermissionAdded = await _rolePermissionRepository.GetById(rolePermission.Id, cancellationToken: cancellationToken);
            return _mapper.Map<RolePermissionDto>(rolePermissionAdded);
        }
    }
}