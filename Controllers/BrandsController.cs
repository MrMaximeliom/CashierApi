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
    [Route("api/brands")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IFileService _fileService;

        public BrandsController(IUnitOfWork unitOfWork, IFileService fileService = null)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        /// <summary>
        /// Gets all brands.
        /// </summary>
        /// 
        /// <returns>A list of all brands</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /brands
        ///
        /// </remarks>
        /// <response code="200">Returns all brands</response>
     
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllBrandsAsync()
        {

            var result = await _unitOfWork.Brands.GetAllAsync();
            var resultDto = result.Adapt<BrandDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets brand record by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested brand record by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /brands/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Returns brand record with submitted id</response>
        /// <response code="404">Returns not found if there is no brand with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBrandByIdAsync(int id)
        {

            var result = await _unitOfWork.Brands.GetByIdAsync(id);
            var resultDto = result.Adapt<BrandDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Adds new brand 
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="logoPath"></param>
        /// <param name="logoFile"></param>
        /// <returns>a newly created brand</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /brands
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///        "description": "description",
        ///        "logoPath": "logo_path",
        ///        "logoFile": "logo_file"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns newly added brand</response>
        /// <response code="400">Returns bad request if submitted data was wrong</response>
    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddBrandAsync(Brand brandDto)
        {

           var result = await _unitOfWork.Brands.AddAsync(brandDto.Adapt<Brand>());
            var resultDto = result.Adapt<BrandDto>();
            _unitOfWork.Complete();
            return Ok(resultDto);


        }

        /// <summary>
        /// Update a brand
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="logoPath"></param>
        /// <param name="logoFile"></param>
        /// <returns>an updated brand record </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /brands/{id:int}
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///        "description": "description",
        ///        "logoPath": "logo_path",
        ///        "logoFile "logo_file"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated brand</response>
        /// <response code="404">if there is no brand record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>
 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateBrandAsync(int id, BrandDto brandDto)
        {

            var entity = _unitOfWork.Brands.GetById(id);

            if (!ModelState.IsValid || brandDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.Name = brandDto.Name;
            entity.Description = brandDto.Description;
            entity.LogoPath = brandDto.LogoPath;
            entity.LogoPath = brandDto.LogoPath;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update brand image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="logoFile"></param>
        /// <returns>A message shows that brand image uploaded successfully </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/brands/{id:int}/logo
        ///     {
        ///        "id": 1
        ///        "logoFile": "logo_file"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns brand image uploaded successfully </response>
        /// <response code="400">If submitted data was wrong</response>
        /// <response code="404">If there is no brand with submitted id</response>
        // limit uploaded images to 2mb size
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequestFormLimits(MultipartBodyLengthLimit = 2097152)]
        //[Authorize(Roles = $"{UsersRolesNames.AdminRole}, {UsersRolesNames.StaffRole}")]
        [HttpPut("{id:int}/image")]
        public async Task<IActionResult> UploadLogoImage(int id, IFormFile logoFile)
        {
            var user = await _unitOfWork.Brands.GetByIdAsync(id);
            if (user is null)
                return NotFound($"No logo record found with this id: {id}");

            if (logoFile is null || logoFile.Length == 0)
                return BadRequest("No picture uploaded");

            var fileResult = await _fileService.SaveImageAsync(logoFile, "brands");

            if (fileResult.Item1 == 0)
            {
                return BadRequest(fileResult.Item2);
            }

            user.LogoPath = "brands\\" + fileResult.Item2;
            _unitOfWork.Complete();

            return Ok("Brand logo uploaded successfully!");

        }

        /// <summary>
        /// Update partial data for brand record
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
        ///     PATCH /api/brands/{id:int}
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
        public IActionResult UpdateBrandPartial(int id, JsonPatchDocument<Brand> brand)

        {
            var entity = _unitOfWork.Brands.GetById(id);
            if (!ModelState.IsValid || brand is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            brand.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Brands.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete a brand
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>message shows that record deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /brands/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes a brand</response>
        /// <response code="404">if there is no brand record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBrandAsync(int id)
        {

            var entity = _unitOfWork.Brands.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.Brands.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
