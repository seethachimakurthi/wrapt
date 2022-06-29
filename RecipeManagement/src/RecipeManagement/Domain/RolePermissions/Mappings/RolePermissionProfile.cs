namespace RecipeManagement.Domain.RolePermissions.Mappings;

using RecipeManagement.Domain.RolePermissions.Dtos;
using AutoMapper;
using RecipeManagement.Domain.RolePermissions;

public class RolePermissionProfile : Profile
{
    public RolePermissionProfile()
    {
        //createmap<to this, from this>
        CreateMap<RolePermission, RolePermissionDto>()
            .ReverseMap();
        CreateMap<RolePermissionForCreationDto, RolePermission>();
        CreateMap<RolePermissionForUpdateDto, RolePermission>()
            .ReverseMap();
    }
}