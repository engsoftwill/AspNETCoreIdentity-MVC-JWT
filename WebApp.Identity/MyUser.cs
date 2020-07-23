using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Identity
{
    public class MyUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
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