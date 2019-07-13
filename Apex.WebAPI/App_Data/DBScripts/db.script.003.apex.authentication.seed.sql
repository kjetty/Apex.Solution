--users
insert into [Users] ([FirstName],[LastName],[LoginId],[LoginPassword],[Email],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Test','Admin','testadmin','Password','ketan@crossthreadsolutions.com','True',0,GetDate())
GO

insert into [Users] ([FirstName],[LastName],[LoginId],[LoginPassword],[Email],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Test','Member','testmember','Password','ketan@crossthreadsolutions.com','True',0,GetDate())
GO

insert into [Users] ([FirstName],[LastName],[LoginId],[LoginPassword],[Email],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Test','Developer','testdevelopr','Password','ketan@crossthreadsolutions.com','True',0,GetDate())
GO

insert into [Users] ([FirstName],[LastName],[LoginId],[LoginPassword],[Email],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Ketan','Jetty','kjetty','Password','kjetty@yahoo.com','True',0,GetDate())
GO

insert into [Users] ([FirstName],[LastName],[LoginId],[LoginPassword],[Email],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Sunil','Narahari','snarahari','Password','snarahari@crossthreadsolutions.com','True',0,GetDate())
GO

insert into [Users] ([FirstName],[LastName],[LoginId],[LoginPassword],[Email],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Parijatha','Narahari','pnarahari','Password','pnarahari@crossthreadsolutions.com','True',0,GetDate())
GO

--roles
insert into [Roles] ([Name],[Description],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Admin','Administrator','True',0,GetDate())
GO

insert into [Roles] ([Name],[Description],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Manager','Manager','True',0,GetDate())
GO

insert into [Roles] ([Name],[Description],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Member','Member','True',0,GetDate())
GO

insert into [Roles] ([Name],[Description],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('Developer','Developer','True',0,GetDate())
GO

--user roles
insert into [UserRoles] ([UserId],[RoleId],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('4','1','True',0,GetDate())
GO

insert into [UserRoles] ([UserId],[RoleId],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('4','2','True',0,GetDate())
GO

insert into [UserRoles] ([UserId],[RoleId],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('1','1','True',0,GetDate())
GO

insert into [UserRoles] ([UserId],[RoleId],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('2','2','True',0,GetDate())
GO

insert into [UserRoles] ([UserId],[RoleId],[ActiveFlag],[ModifiedBy],[ModifiedDate])
values ('3','3','True',0,GetDate())
GO

/*
--	Category	Kee				Valu				orderNum		Description
 
	language	hindi								0
	language	english								0
	language	telugu								0
 
	status		job				start
	status		job				in progress
	status		job				close

	status		invoice			start
	status		invoice			sent to client
	status		invoice			close
 
	rate		basic			3.25								3.25 per hour
	rate		aethna hosp		9.25								9.25 per hour 
 
	travel		basic			0.63								0.63 cens per mile
	travel		premium			0.75
	travel		care hosp		0.90
	
	InvoiceType	single
	InvoiceType	double
          
*/