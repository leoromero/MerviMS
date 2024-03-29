﻿using Identity.Domain;
using Identity.DTOs;
using Mervi.Services.Interfaces;
using Mervi.Services.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services.Mappers
{
    public class UserMapper : Mapper<User,UserDto>
    {
        private readonly IMapper<IdentityRole, RoleDto> roleMapper;

        public UserMapper(IMapper<IdentityRole, RoleDto> mapper)
        {
            this.roleMapper = mapper;
        }
        public override UserDto ToDto(User entity)
        {
            return new UserDto
            {
                Email = entity.Email,
                FullName = entity.FullName,
                UserName = entity.UserName,
                Roles = roleMapper.ToDtos(entity.Roles)
            };
        }

        public override User ToEntity(UserDto dto)
        {
            return new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                UserName = dto.UserName,
                Roles = roleMapper.ToEntities(dto.Roles)
            };
        }
    }
}
