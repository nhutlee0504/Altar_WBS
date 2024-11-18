﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
	public class Student
	{
		[Key]
		[ForeignKey("User")]
		public int UserID { get; set; } // UserID là khóa chính
		public User User { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
		public string ParentPhone { get; set; }
		public string ProfileImage { get; set; }
	}
}