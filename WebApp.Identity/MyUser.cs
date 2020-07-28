using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Identity
{
    public class MyUser : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public string Member { get; set; } = "Member";
        public string OrgId { get; set; }
    }

    public class Organization 
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}


/*
 * CREATE TABLE [dbo].[Users](
	[Id] [nvarchar] (450) NOT NULL,
	[UserName] [nvarchar] (256) NULL,
	[NormalizedUserName] [nvarchar] (256) NULL,
	[PasswordHash] [nvarchar] (max) NULL,
CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED(
	[Id]  ASC

))
*/