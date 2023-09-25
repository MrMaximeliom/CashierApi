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
    [Route("api/invoice-items")]
    [Produces("application/json")]
    [ApiController]
    public class InvoiceItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public InvoiceItemsController(IUnitOfWork unitOfWork, IFileService fileService = null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all invoice items.
        /// </summary>
        /// 
        /// <returns>A list of all invoice items</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /invoice-items
        ///
        /// </remarks>
        /// <response code="200">Returns all invoice items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetAllInvoiceItemsAsync()
        {

            var result = await _unitOfWork.InvoiceItems.GetAllAsync();
            var resultDto = result.Adapt<InvoiceItemDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets invoice item record by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested invoice item record by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /invoice-items/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Returns invoice item record with submitted id</response>
        /// <response code="404">Returns not found if there is no invoice item with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvoiceItemByIdAsync(int id)
        {

            var result = await _unitOfWork.InvoiceItems.GetByIdAsync(id);
            var resultDto = result.Adapt<InvoiceItemDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Adds new invoice item
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="discount"></param>
        /// <param name="totalPrice"></param>
        /// <param name="invoiceId"></param>
        /// <param name="productId"></param>
        /// <returns>a newly created invoice item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /invoice-items
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///        "count": 2,
        ///        "discount": 10.2,
        ///        "totalPrice": 15,
        ///        "invoiceId": 1,
        ///        "productId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns newly added invoice item</response>
        /// <response code="400">Returns bad request if submitted data was wrong</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> AddInvoiceItemAsync(InvoiceItemDto invoiceItemDto)
        {

            var result = await _unitOfWork.InvoiceItems.AddAsync(invoiceItemDto.Adapt<InvoiceItem>());
            var resultDto = result.Adapt<InvoiceItemDto>();

            return Ok(resultDto);


        }

        /// <summary>
        /// Update a invoice
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="discount"></param>
        /// <param name="totalPrice"></param>
        /// <param name="invoiceId"></param>
        /// <param name="productId"></param>
        /// <returns>an updated invoice item record </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /invoice-items/{id:int}
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///        "count": 2,
        ///        "discount": 10.2,
        ///        "totalPrice": 15,
        ///        "invoiceId": 1,
        ///        "productId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated invoice item</response>
        /// <response code="404">if there is no invoice item record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult UpdateInvoiceItemAsync(int id, InvoiceItemDto invoiceItemDto)
        {

            var entity = _unitOfWork.InvoiceItems.GetById(id);

            if (!ModelState.IsValid || invoiceItemDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.Name = invoiceItemDto.Name;
            entity.Count = invoiceItemDto.Count;
            entity.Discount = invoiceItemDto.Discount;
            entity.TotalPrice = invoiceItemDto.TotalPrice;
            entity.InvoiceId = invoiceItemDto.InvoiceId;
            entity.ProductId = invoiceItemDto.ProductId;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update partial data for invoice item record
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
        ///     PATCH /api/invoice-items/{id:int}
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
        [Authorize]
        [HttpPatch("{id:int}")]
        public IActionResult UpdateInvoiceItemPartial(int id, JsonPatchDocument<InvoiceItem> invoiceItem)

        {
            var entity = _unitOfWork.InvoiceItems.GetById(id);
            if (!ModelState.IsValid || invoiceItem is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            invoiceItem.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.InvoiceItems.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete an invoice item
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>message shows that record deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /invoice-items/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes an invoice item</response>
        /// <response code="404">if there is no invoice item record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult DeleteInvoiceItemAsync(int id)
        {

            var entity = _unitOfWork.InvoiceItems.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.InvoiceItems.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
