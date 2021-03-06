﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Scripting.Utils;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service.Datalayer;

namespace Northernrunners.ImportLibrary.Service
{
    public class UserService:IUserService
    {
        private readonly IDatalayerService _resultDataService;
        
        public UserService(IDatalayerService resultDataService)
        {
            _resultDataService = resultDataService;
        }

        public void AddUser(User user)
        {
            Console.WriteLine("Dato: " + user.DateOfBirth);
            var userDto = new UserDto
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Name = user.Name,
                Gender = user.Gender
            };
            _resultDataService.AddUser(userDto);
        }

        public User FindUser(string name)
        {
            return  GetAllUsers().FirstOrDefault(t => t.Name.Equals(name));
        }

        public User FindUser(int id)
        {
            return GetAllUsers().FirstOrDefault(t => t.Id.Equals(id));
        }

        public ICollection<User> GetAllUsers()
        {
            var users = _resultDataService.GetAllUsers();
            return users.Select(userDto => new User
            {
                DateOfBirth = userDto.DateOfBirth, Email = userDto.Email, Id = userDto.Id,Gender = userDto.Gender, Name = userDto.Name
            }).ToList();
            

        }

        public void AddUsers(ICollection<User> users)
        {
            foreach (var user in users)
            {
                AddUser(user);
            }
        }

        public ICollection<User> CreateAndGetUsers(ICollection<User> users, StreamWriter streamWriter)
        {
            CamelCaseUsers(users);
            var usersFromDb = _resultDataService.GetAllUsers();
            var usersToCreate = (from user in users let userFound = usersFromDb.FirstOrDefault(t => t.Name.Equals(user.Name)) where userFound == null select user).ToList();
            var usersToUpdate = usersFromDb.Where(user => string.IsNullOrEmpty(user.Gender)).ToList();
            if (usersToCreate.Count == 0 && usersToUpdate.Count == 0)
                return usersFromDb.Select(userDto => new User
                {
                    DateOfBirth = userDto.DateOfBirth,
                    Email = userDto.Email,
                    Id = userDto.Id,
                    Gender = userDto.Gender,
                    Name = userDto.Name
                }).ToList();

            foreach (var user in usersToUpdate)
            {
                var userInUserList = users.FirstOrDefault(t => t.Name.Equals(user.Name));
                if (userInUserList != null)
                {
                    userInUserList.Id = user.Id;
                    UpdateUser(userInUserList);
                }
                    
                
            }
            foreach (var user in usersToCreate)
            {
                AddUser(user);
                streamWriter.WriteLine("Added user: " + user.Name);
            }
            usersFromDb = _resultDataService.GetAllUsers();
            return usersFromDb.Select(userDto => new User
            {
                DateOfBirth = userDto.DateOfBirth,
                Email = userDto.Email,
                Id = userDto.Id,
                Gender = userDto.Gender,
                Name = userDto.Name
            }).ToList();
        }

        private static void CamelCaseUsers(IEnumerable<User> users)
        {
            var textInfo = new CultureInfo("no-NO", false).TextInfo;
            foreach (var user in users)
            {
                user.Name = textInfo.ToTitleCase(user.Name);
            }
        }

        public ICollection<User> GetAllUsersWithInvalidDate()
        {
            return GetAllUsers().Where(t => t.DateOfBirth < new DateTime(1900, 1, 1)).ToList();
        }

        public void UpdateUser(User user)
        {
            var userDto = new UserDto
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Gender = user.Gender,
                Id = user.Id
            };
            userDto.Name = userDto.Name;
            _resultDataService.UpdateUser(userDto);
        }
    }
}
