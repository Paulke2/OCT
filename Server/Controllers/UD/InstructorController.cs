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
    public class InstructorController : BaseController, GenericRestController<InstructorDTO>
    {
        public InstructorController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("Get/{SchoolId}/{InstructorId}")]
        public async Task<IActionResult> Get(int SchoolId, int InstructorId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                InstructorDTO? result = await _context
                    .Instructors
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.InstructorId == InstructorId)
                    .Select(i => new InstructorDTO
                    {
                        SchoolId = i.SchoolId,
                        InstructorId = i.InstructorId,
                        Salutation = i.Salutation,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        StreetAddress = i.StreetAddress,
                        Zip = i.Zip,
                        Phone = i.Phone,
                        CreatedBy = i.CreatedBy,
                        CreatedDate = i.CreatedDate,
                        ModifiedBy = i.ModifiedBy,
                        ModifiedDate = i.ModifiedDate
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

                var result = await _context.Instructors.Select(i => new InstructorDTO
                {
                    SchoolId = i.SchoolId,
                    InstructorId = i.InstructorId,
                    Salutation = i.Salutation,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    StreetAddress = i.StreetAddress,
                    Zip = i.Zip,
                    Phone = i.Phone,
                    CreatedBy = i.CreatedBy,
                    CreatedDate = i.CreatedDate,
                    ModifiedBy = i.ModifiedBy,
                    ModifiedDate = i.ModifiedDate
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
        public async Task<IActionResult> Post([FromBody] InstructorDTO _InstructorDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors
                    .Where(x => x.SchoolId == _InstructorDTO.SchoolId)
                    .Where(x => x.InstructorId == _InstructorDTO.InstructorId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.Instructor instructor = new EF.Models.Instructor
                    {
                        SchoolId = _InstructorDTO.SchoolId,
                        InstructorId = _InstructorDTO.InstructorId,
                        Salutation = _InstructorDTO.Salutation,
                        FirstName = _InstructorDTO.FirstName,
                        LastName = _InstructorDTO.LastName,
                        StreetAddress = _InstructorDTO.StreetAddress,
                        Zip = _InstructorDTO.Zip,
                        Phone = _InstructorDTO.Phone,
                        CreatedBy = _InstructorDTO.CreatedBy,
                        CreatedDate = _InstructorDTO.CreatedDate,
                        ModifiedBy = _InstructorDTO.ModifiedBy,
                        ModifiedDate = _InstructorDTO.ModifiedDate
                    };

                    _context.Instructors.Add(instructor);
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
        public async Task<IActionResult> Put([FromBody] InstructorDTO _InstructorDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors
                    .Where(x => x.SchoolId == _InstructorDTO.SchoolId)
                    .Where(x => x.InstructorId == _InstructorDTO.InstructorId)
                    .FirstOrDefaultAsync();

                itm.SchoolId = _InstructorDTO.SchoolId;
                itm.InstructorId = _InstructorDTO.InstructorId;
                itm.Salutation = _InstructorDTO.Salutation;
                itm.FirstName = _InstructorDTO.FirstName;
                itm.LastName = _InstructorDTO.LastName;
                itm.StreetAddress = _InstructorDTO.StreetAddress;
                itm.Zip = _InstructorDTO.Zip;
                itm.Phone = _InstructorDTO.Phone;
                itm.CreatedBy = _InstructorDTO.CreatedBy;
                itm.CreatedDate = _InstructorDTO.CreatedDate;
                itm.ModifiedBy = _InstructorDTO.ModifiedBy;
                itm.ModifiedDate = _InstructorDTO.ModifiedDate;

                _context.Instructors.Update(itm);
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
        [Route("Delete/{SchoolId}/{InstructorId}")]
        public async Task<IActionResult> Delete(int SchoolId, int InstructorId)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.InstructorId == InstructorId)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Instructors.Remove(itm);
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

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }
    }
}

