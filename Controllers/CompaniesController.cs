using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using CashierApi.DataTransferObjects;
using CashierApi.Interfaces;
using CashierApi.Models;
using CashierApi.Services;

namespace CashierApi.Controllers
{
    [Route("api/companies")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public CompaniesController(IUnitOfWork unitOfWork, IFileService fileService = null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// 
        /// <returns>A list of all companies</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /companies
        ///
        /// </remarks>
        /// <response code="200">Returns all companies</response>
   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {

            var result = await _unitOfWork.Companies.GetAllAsync();
            var resultDto = result.Adapt<CompanyDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets company record by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested company record by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /companies/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Returns company record with submitted id</response>
        /// <response code="404">Returns not found if there is no company with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompanyByIdAsync(int id)
        {

            var result = await _unitOfWork.Companies.GetByIdAsync(id);
            var resultDto = result.Adapt<CompanyDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Adds new company
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>a newly created company</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /companies
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///        "description": "description",
        ///        "email": "email",
        ///        "phoneNumber": "phone_number"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns newly added company</response>
        /// <response code="400">Returns bad request if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddCompanyAsync(CompanyDto companyDto)
        {

            var result = await _unitOfWork.Companies.AddAsync(companyDto.Adapt<Company>());
            var resultDto = result.Adapt<CompanyDto>();

            return Ok(resultDto);


        }

        /// <summary>
        /// Update a company
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>an updated company record </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /companies/{id:int}
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///        "description": "description",
        ///        "email": "email",
        ///        "phoneNumber": "phone_number"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated company</response>
        /// <response code="404">if there is no company record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateCompanyAsync(int id, CompanyDto companyDto)
        {

            var entity = _unitOfWork.Companies.GetById(id);

            if (!ModelState.IsValid || companyDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.Name = companyDto.Name;
            entity.Description = companyDto.Description;
            entity.Email = companyDto.Email;
            entity.PhoneNumber = companyDto.PhoneNumber;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update partial data for company record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operationType"></param>
        /// <param name="path"></param>
        /// <param name="op"></param>
        /// <param name="from"></param>
        /// <param name="value"></param>
        /// <returns> no content </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/companies/{id:int}
        ///     [ 
        ///       {
        ///       "operationType": 0,
        ///       "path": "/propertyName",
        ///       "op": "replace",
        ///       "from": "string",
        ///       "value": "string"
        ///       }
        ///     ]
        ///        
        ///     
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="400">If submitted data was wrong</response>
        /// <response code="404">If there is no record with submitted id</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id:int}")]
        public IActionResult UpdateCompanyPartial(int id, JsonPatchDocument<Company> company)

        {
            var entity = _unitOfWork.Companies.GetById(id);
            if (!ModelState.IsValid || company is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            company.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Companies.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete a company
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>message shows that record deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /companies/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes a company</response>
        /// <response code="404">if there is no company record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCompanyAsync(int id)
        {

            var entity = _unitOfWork.Companies.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.Companies.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
