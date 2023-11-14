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
    public class GradeConversionController : BaseController, GenericRestController<GradeConversionDTO>
    {
        public GradeConversionController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("Get/{SchoolID}/{LetterGrade}")]
        public async Task<IActionResult> Get(int SchoolID, string LetterGrade)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeConversionDTO? result = await _context
                    .GradeConversions
                    .Where(x => x.SchoolId == SchoolID && x.LetterGrade == LetterGrade)
                    .Select(gc => new GradeConversionDTO
                    {
                        SchoolId = gc.SchoolId,
                        LetterGrade = gc.LetterGrade,
                        GradePoint = gc.GradePoint,
                        MaxGrade = gc.MaxGrade,
                        MinGrade = gc.MinGrade,
                        CreatedBy = gc.CreatedBy,
                        CreatedDate = gc.CreatedDate,
                        ModifiedBy = gc.ModifiedBy,
                        ModifiedDate = gc.ModifiedDate
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

                var result = await _context.GradeConversions.Select(gc => new GradeConversionDTO
                {
                    SchoolId = gc.SchoolId,
                    LetterGrade = gc.LetterGrade,
                    GradePoint = gc.GradePoint,
                    MaxGrade = gc.MaxGrade,
                    MinGrade = gc.MinGrade,
                    CreatedBy = gc.CreatedBy,
                    CreatedDate = gc.CreatedDate,
                    ModifiedBy = gc.ModifiedBy,
                    ModifiedDate = gc.ModifiedDate
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
        public async Task<IActionResult> Post([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions
                    .Where(x => x.SchoolId == _GradeConversionDTO.SchoolId && x.LetterGrade == _GradeConversionDTO.LetterGrade)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeConversion gc = new GradeConversion
                    {
                        SchoolId = _GradeConversionDTO.SchoolId,
                        LetterGrade = _GradeConversionDTO.LetterGrade,
                        GradePoint = _GradeConversionDTO.GradePoint,
                        MaxGrade = _GradeConversionDTO.MaxGrade,
                        MinGrade = _GradeConversionDTO.MinGrade,
                        CreatedBy = _GradeConversionDTO.CreatedBy,
                        CreatedDate = _GradeConversionDTO.CreatedDate,
                        ModifiedBy = _GradeConversionDTO.ModifiedBy,
                        ModifiedDate = _GradeConversionDTO.ModifiedDate
                    };

                    _context.GradeConversions.Add(gc);
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
        public async Task<IActionResult> Put([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions
                    .Where(x => x.SchoolId == _GradeConversionDTO.SchoolId && x.LetterGrade == _GradeConversionDTO.LetterGrade)
                    .FirstOrDefaultAsync();

                itm.SchoolId = _GradeConversionDTO.SchoolId;
                itm.LetterGrade = _GradeConversionDTO.LetterGrade;
                itm.GradePoint = _GradeConversionDTO.GradePoint;
                itm.MaxGrade = _GradeConversionDTO.MaxGrade;
                itm.MinGrade = _GradeConversionDTO.MinGrade;
                itm.CreatedBy = _GradeConversionDTO.CreatedBy;
                itm.CreatedDate = _GradeConversionDTO.CreatedDate;
                itm.ModifiedBy = _GradeConversionDTO.ModifiedBy;
                itm.ModifiedDate = _GradeConversionDTO.ModifiedDate;

                _context.GradeConversions.Update(itm);
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
        [Route("Delete/{SchoolID}/{LetterGrade}")]
        public async Task<IActionResult> Delete(int SchoolID, string LetterGrade)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions
                    .Where(x => x.SchoolId == SchoolID && x.LetterGrade == LetterGrade)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeConversions.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
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
