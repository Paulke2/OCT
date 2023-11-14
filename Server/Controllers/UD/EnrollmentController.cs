using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using static System.Collections.Specialized.BitVector32;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : BaseController, GenericRestController<EnrollmentDTO>
    {
        public EnrollmentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("Get/{StudentID}/{SectionID}")]
        public async Task<IActionResult> Get(int StudentID, int SectionID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                EnrollmentDTO? result = await _context
                    .Enrollments
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.StudentId == StudentID)
                    .Select(sp => new EnrollmentDTO
                    {
                        StudentId = sp.StudentId,
                        SectionId = sp.SectionId,
                        EnrollDate = sp.EnrollDate,
                        FinalGrade = sp.FinalGrade,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
                        SchoolId = sp.SchoolId,
                        
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

                var result = await _context.Enrollments.Select(sp => new EnrollmentDTO
                {
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    EnrollDate = sp.EnrollDate,
                    FinalGrade = sp.FinalGrade,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                   
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
        public async Task<IActionResult> Post([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId && x.StudentId == _EnrollmentDTO.StudentId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.Enrollment enrollment = new EF.Models.Enrollment
                    {
                        StudentId = _EnrollmentDTO.StudentId,
                        SectionId = _EnrollmentDTO.SectionId,
                        EnrollDate = _EnrollmentDTO.EnrollDate,
                        FinalGrade = _EnrollmentDTO.FinalGrade,
                        CreatedBy = _EnrollmentDTO.CreatedBy,
                        CreatedDate = _EnrollmentDTO.CreatedDate,
                        ModifiedBy = _EnrollmentDTO.ModifiedBy,
                        ModifiedDate = _EnrollmentDTO.ModifiedDate,
                        SchoolId = _EnrollmentDTO.SchoolId,
                        
                    };
                    _context.Enrollments.Add(enrollment);
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
        public async Task<IActionResult> Put([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId && x.StudentId == _EnrollmentDTO.StudentId)
                    .FirstOrDefaultAsync();
                itm.StudentId = _EnrollmentDTO.StudentId;
                        itm.SectionId = _EnrollmentDTO.SectionId;
                        itm.EnrollDate = _EnrollmentDTO.EnrollDate;
                        itm.FinalGrade = _EnrollmentDTO.FinalGrade;
                        itm.CreatedBy = _EnrollmentDTO.CreatedBy;
                        itm.CreatedDate = _EnrollmentDTO.CreatedDate;
                        itm.ModifiedBy = _EnrollmentDTO.ModifiedBy;
                        itm.ModifiedDate = _EnrollmentDTO.ModifiedDate;
                        itm.SchoolId = _EnrollmentDTO.SchoolId;
                        
               

                _context.Enrollments.Update(itm);
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
        public async Task<IActionResult> Delete(int StudentID)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.StudentId == StudentID)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Enrollments.Remove(itm);
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
    }
}
