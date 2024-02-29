﻿using FileStorageNetCore;
using FileStorageNetCore.Api;
using Microsoft.AspNetCore.Mvc;

namespace StudentCredit.Hosting.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FilesStorageController : FileStorageController
	{

		public FilesStorageController(BlobStorageService service)
			: base(service)
		{

		}
	}
}
