using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Apex.Domain.Models
{
	public class LoginForm
	{
		public string username { get; set; }
		public string password { get; set; }
	}
}
