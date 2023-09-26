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
    [Route("api/products")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IFileService _fileService;

        public ProductsController(IUnitOfWork unitOfWork, IFileService fileService = null)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// 
        /// <returns>A list of all products</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /products
        ///
        /// </remarks>
        /// <response code="200">Returns all products</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {

            var result = await _unitOfWork.Products.GetAllAsync();
            var resultDto = result.Adapt<ProductDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets prodcut record by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested product record by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /products/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Returns product record with submitted id</response>
        /// <response code="404">Returns not found if there is no prodcut with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {

            var result = await _unitOfWork.Products.GetByIdAsync(id);
            var resultDto = result.Adapt<ProductDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Adds new prodcut
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="barcode"></param>
        /// <param name="description"></param>
        /// <param name="imagePath"></param>
        /// <param name="imageFile"></param>
        /// <returns>a newly created brand</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /products
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///        "barcode": "varcode",
        ///        "description": "description",
        ///        "imagePath": "logo_path",
        ///        "imageFile": "logo_file"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns newly added product</response>
        /// <response code="400">Returns bad request if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(ProductDto productDto)
        {

            var result = await _unitOfWork.Products.AddAsync(productDto.Adapt<Product>());
            var resultDto = result.Adapt<ProductDto>();

            return Ok(resultDto);


        }

        /// <summary>
        /// Update a product
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="barcode"></param>
        /// <param name="description"></param>
        /// <param name="imagePath"></param>
        /// <param name="imageFile"></param>
        /// <returns>an updated product record </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /products/{id:int}
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///        "barcode": "varcode",
        ///        "description": "description",
        ///        "imagePath": "logo_path",
        ///        "imageFile": "logo_file"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated product</response>
        /// <response code="404">if there is no product record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateProductAsync(int id, ProductDto productDto)
        {

            var entity = _unitOfWork.Products.GetById(id);

            if (!ModelState.IsValid || productDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.Name = productDto.Name;
            entity.Description = productDto.Description;
            entity.Barcode = productDto.Barcode;
            entity.ImagePath = productDto.ImagePath;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update product image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imageFile"></param>
        /// <returns>A message shows that product image uploaded successfully </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/products/{id:int}/image
        ///     {
        ///        "id": 1
        ///        "imageFile": "image_file"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns product image uploaded successfully </response>
        /// <response code="400">If submitted data was wrong</response>
        /// <response code="404">If there is no product with submitted id</response>
        // limit uploaded images to 2mb size
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequestFormLimits(MultipartBodyLengthLimit = 2097152)]
        //[Authorize(Roles = $"{UsersRolesNames.AdminRole}, {UsersRolesNames.StaffRole}")]
        [HttpPut("{id:int}/image")]
        public async Task<IActionResult> UploadProductImage(int id, IFormFile imageFile)
        {
            var user = await _unitOfWork.Products.GetByIdAsync(id);
            if (user is null)
                return NotFound($"No logo record found with this id: {id}");

            if (imageFile is null || imageFile.Length == 0)
                return BadRequest("No picture uploaded");

            var fileResult = await _fileService.SaveImageAsync(imageFile, "products");

            if (fileResult.Item1 == 0)
            {
                return BadRequest(fileResult.Item2);
            }

            user.ImagePath = "products\\" + fileResult.Item2;
            _unitOfWork.Complete();

            return Ok("product image uploaded successfully!");

        }

        /// <summary>
        /// Update partial data for product record
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
        ///     PATCH /api/products/{id:int}
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
        public IActionResult UpdateProductPartial(int id, JsonPatchDocument<Product> product)

        {
            var entity = _unitOfWork.Products.GetById(id);
            if (!ModelState.IsValid || product is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            product.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Products.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete a product
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>message shows that record deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /products/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes a product</response>
        /// <response code="404">if there is no product record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProductAsync(int id)
        {

            var entity = _unitOfWork.Products.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.Products.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
