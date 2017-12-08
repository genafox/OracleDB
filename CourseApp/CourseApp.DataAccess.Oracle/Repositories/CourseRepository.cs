﻿using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace CourseApp.DataAccess.Oracle.Repositories
{
	public class CourseRepository : ICourseRepository
	{
		private readonly OracleDbContext context;

		public CourseRepository(OracleDbContext dbContext)
		{
			this.context = dbContext;
		}

		public Task<IEnumerable<Course>> GetAsync()
		{
			return this.context.ExecuteQueryAsync(
				"SELECT * FROM Course_GF",
				OracleDataMapper.FromReader);
		}

		public Course GetById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<int> Create(Course entity)
		{
			string procName = "CourseAppPackage_GF.CreateCourseProc";

			var newCourseIdParam = new OracleParameter("newCourseId", OracleDbType.Int32, ParameterDirection.Output);

			await this.context.ExecuteProcedureAsync(
				procName,
				new OracleParameter("courseName", OracleDbType.Varchar2, ParameterDirection.Input),
				new OracleParameter("coursePrice", OracleDbType.Double, ParameterDirection.Input),
				newCourseIdParam);

			return (int)newCourseIdParam.Value;
		}

		public void Update(Course entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}
