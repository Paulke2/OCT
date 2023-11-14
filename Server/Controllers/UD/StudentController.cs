using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models; // Replace this with the actual namespace
using OCTOBER.Shared;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController, GenericRestController<StudentDTO>
    {
        public StudentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("Get/{StudentId}")]
        public async Task<IActionResult> Get(int StudentId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                StudentDTO? result = await _context
                    .Students
                    .Where(x => x.StudentId == StudentId)
                    .Select(s => new StudentDTO
                    {
                        StudentId = s.StudentId,
                        Salutation = s.Salutation,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        StreetAddress = s.StreetAddress,
                        Zip = s.Zip,
                        Phone = s.Phone,
                        Employer = s.Employer,
                        RegistrationDate = s.RegistrationDate,
                        CreatedBy = s.CreatedBy,
                        CreatedDate = s.CreatedDate,
                        ModifiedBy = s.ModifiedBy,
                        ModifiedDate = s.ModifiedDate
                    })
                    .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Students.Select(s => new StudentDTO
                {
                    StudentId = s.StudentId,
                    Salutation = s.Salutation,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    StreetAddress = s.StreetAddress,
                    Zip = s.Zip,
                    Phone = s.Phone,
                    Employer = s.Employer,
                    RegistrationDate = s.RegistrationDate,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate
                })
                .ToListAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                    .Where(x => x.StudentId == _StudentDTO.StudentId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.Student student = new EF.Models.Student
                    {
                        StudentId = _StudentDTO.StudentId,
                        Salutation = _StudentDTO.Salutation,
                        FirstName = _StudentDTO.FirstName,
                        LastName = _StudentDTO.LastName,
                        StreetAddress = _StudentDTO.StreetAddress,
                        Zip = _StudentDTO.Zip,
                        Phone = _StudentDTO.Phone,
                        Employer = _StudentDTO.Employer,
                        RegistrationDate = _StudentDTO.RegistrationDate,
                        CreatedBy = _StudentDTO.CreatedBy,
                        CreatedDate = _StudentDTO.CreatedDate,
                        ModifiedBy = _StudentDTO.ModifiedBy,
                        ModifiedDate = _StudentDTO.ModifiedDate
                    };

                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] StudentDTO _StudentDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                    .Where(x => x.StudentId == _StudentDTO.StudentId)
                    .FirstOrDefaultAsync();

                itm.Salutation = _StudentDTO.Salutation;
                itm.FirstName = _StudentDTO.FirstName;
                itm.LastName = _StudentDTO.LastName;
                itm.StreetAddress = _StudentDTO.StreetAddress;
                itm.Zip = _StudentDTO.Zip;
                itm.Phone = _StudentDTO.Phone;
                itm.Employer = _StudentDTO.Employer;
                itm.RegistrationDate = _StudentDTO.RegistrationDate;
                itm.CreatedBy = _StudentDTO.CreatedBy;
                itm.CreatedDate = _StudentDTO.CreatedDate;
                itm.ModifiedBy = _StudentDTO.ModifiedBy;
                itm.ModifiedDate = _StudentDTO.ModifiedDate;

                _context.Students.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpDelete]
        [Route("Delete/{StudentId}")]
        public async Task<IActionResult> Delete(int StudentId)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                    .Where(x => x.StudentId == StudentId)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Students.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }


    }
}
