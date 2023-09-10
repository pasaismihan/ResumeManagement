using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class CandidateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CandidateController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // CRUD

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCandidate([FromForm] CandidateCreateDto dto, IFormFile pdfFile)
        // FromForm different by FromBody . FromForm take file to request.
        {
            // First we saving pdf to server then saving url into our entity
            var fiveMegaByte = 5 * 1024 * 1024;
            var pdfMyType = "application/pdf";
            if(pdfFile.Length > fiveMegaByte || pdfFile.ContentType != pdfMyType)
            {
                return BadRequest("file is not valid");
            }

            var resumeUrl = Guid.NewGuid().ToString() + ".pdf"; // creating pdf 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"Documents","Pdfs",resumeUrl); // its combine with directoryPath(file direction like c/Documents/pdfs) , and resumeUrl)
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }
            Candidate newCandidate = _mapper.Map<Candidate>(dto);
            newCandidate.ResumeUrl = resumeUrl;
            await _context.Candidates.AddAsync(newCandidate);
            await _context.SaveChangesAsync();

            return Ok("Candidate Saved Succesfully");
        }
        // Read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidates()
        {
            var candidates = await _context.Candidates.Include(c=> c.Job).ToListAsync();
            var convertedCandidate = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);
            return Ok(convertedCandidate);
        }

        // Read (Download Pdf File)
        [HttpGet]
        [Route("download/{url}")]
        public IActionResult DownloadPdfFile(string url)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", "Pdfs", url);
            if (!System.IO.File.Exists(filePath)) {
                return NotFound("File Not Found");
            }
            var pdfBytes = System.IO.File.ReadAllBytes(filePath);
            var file = File(pdfBytes, "application/pdf",url);
            return file;
        }
    }
}

