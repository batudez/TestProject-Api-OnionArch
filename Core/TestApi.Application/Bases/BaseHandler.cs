﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Interfaces.Automapper;
using TestApi.Application.UnitOfWorks;

namespace TestApi.Application.Bases
{
	public class BaseHandler
	{
		public readonly IMapper _mapper;
		public readonly IUnitOfWork _unitOfWork;
		public readonly IHttpContextAccessor _httpContextAccessor;
		public readonly string _userId;

		public BaseHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
			_userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}
