namespace RecipeManagement.Domain.RolePermissions.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class RolePermissionForManipulationDto 
    {
        public string Role { get; set; }
        public string Permission { get; set; }
    }
}