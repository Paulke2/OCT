using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
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
    public class GradeController : BaseController, GenericRestController<GradeDTO>
    {
        public GradeController(OCTOBEROracleContext context,
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

                GradeDTO? result = await _context
                    .Grades
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.StudentId == StudentID)
                    .Select(sp => new GradeDTO
                    {
                        SchoolId = sp.SchoolId,
                        StudentId = sp.StudentId,
                        SectionId = sp.SectionId,
                        GradeTypeCode = sp.GradeTypeCode,
                        GradeCodeOccurrence = sp.GradeCodeOccurrence,
                        NumericGrade = sp.NumericGrade,
                        Comments = sp.Comments,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate
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

                var result = await _context.Grades.Select(sp => new GradeDTO
                {
                    SchoolId = sp.SchoolId,
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    GradeTypeCode = sp.GradeTypeCode,
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    NumericGrade = sp.NumericGrade,
                    Comments = sp.Comments,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
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
        public async Task<IActionResult> Post([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.SectionId == _GradeDTO.SectionId && x.StudentId == _GradeDTO.StudentId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.Grade grade = new EF.Models.Grade
                    {
                        SchoolId = _GradeDTO.SchoolId,
                        StudentId = _GradeDTO.StudentId,
                        SectionId = _GradeDTO.SectionId,
                        GradeTypeCode = _GradeDTO.GradeTypeCode,
                        GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence,
                        NumericGrade = _GradeDTO.NumericGrade,
                        Comments = _GradeDTO.Comments,
                        CreatedBy = _GradeDTO.CreatedBy,
                        CreatedDate = _GradeDTO.CreatedDate,
                        ModifiedBy = _GradeDTO.ModifiedBy,
                        ModifiedDate = _GradeDTO.ModifiedDate
                    };

                    _context.Grades.Add(grade);
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
        public async Task<IActionResult> Put([FromBody] GradeDTO _GradeDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.SectionId == _GradeDTO.SectionId && x.StudentId == _GradeDTO.StudentId)
                    .FirstOrDefaultAsync();

                itm.SchoolId = _GradeDTO.SchoolId;
                itm.StudentId = _GradeDTO.StudentId;
                itm.SectionId = _GradeDTO.SectionId;
                itm.GradeTypeCode = _GradeDTO.GradeTypeCode;
                itm.GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence;
                itm.NumericGrade = _GradeDTO.NumericGrade;
                itm.Comments = _GradeDTO.Comments;
                itm.CreatedBy = _GradeDTO.CreatedBy;
                itm.CreatedDate = _GradeDTO.CreatedDate;
                itm.ModifiedBy = _GradeDTO.ModifiedBy;
                itm.ModifiedDate = _GradeDTO.ModifiedDate;

                _context.Grades.Update(itm);
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
        [Route("Delete/{StudentID}")]
        public async Task<IActionResult> Delete(int StudentID)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.StudentId == StudentID)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Grades.Remove(itm);
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