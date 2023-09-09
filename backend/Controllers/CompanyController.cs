using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }

        public CompanyController(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // CRUD ISLEMLERI 

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto dto)
        {
            Company newCompany = _mapper.Map<Company>(dto);
            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
            return Ok("Company Created Successfully");
        }

        // Read

        // Update

        // Delete 




    }
}

