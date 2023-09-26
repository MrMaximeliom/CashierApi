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
    [Route("api/invoices")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public InvoicesController(IUnitOfWork unitOfWork, IFileService fileService = null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// 
        /// <returns>A list of all invoices</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /invoices
        ///
        /// </remarks>
        /// <response code="200">Returns all invoices</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllInvoicesAsync()
        {

            var result = await _unitOfWork.Invoices.GetAllAsync();
            var resultDto = result.Adapt<InvoiceDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets invoice record by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested invoice record by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /invoices/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Returns invoice record with submitted id</response>
        /// <response code="404">Returns not found if there is no invoice with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvoiceByIdAsync(int id)
        {

            var result = await _unitOfWork.Invoices.GetByIdAsync(id);
            var resultDto = result.Adapt<InvoiceDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Adds new invoice
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="number"></param>
        /// <param name="VAT"></param>
        /// <param name="deliveryPrice"></param>
        /// <param name="userId"></param>
        /// <returns>a newly created invoice</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /invoices
        ///     {
        ///        "id": 1,
        ///        "number": "234RT",
        ///        "VAT": 0.15,
        ///        "deliveryPrice": 0,
        ///        "userId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns newly added invoice</response>
        /// <response code="400">Returns bad request if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddInvoiceAsync(InvoiceDto invoiceDto)
        {

            var result = await _unitOfWork.Invoices.AddAsync(invoiceDto.Adapt<Invoice>());
            var resultDto = result.Adapt<InvoiceDto>();
            _unitOfWork.Complete();
            return Ok(resultDto);


        }

        /// <summary>
        /// Update a invoice
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="number"></param>
        /// <param name="VAT"></param>
        /// <param name="deliveryPrice"></param>
        /// <param name="userId"></param>
        /// <returns>an updated invoice record </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /invoices/{id:int}
        ///     {
        ///        "id": 1,
        ///        "number": "234RT",
        ///        "VAT": 0.15,
        ///        "deliveryPrice": 0,
        ///        "userId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated invoice</response>
        /// <response code="404">if there is no invoice record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateInvoiceAsync(int id, InvoiceDto invoiceDto)
        {

            var entity = _unitOfWork.Invoices.GetById(id);

            if (!ModelState.IsValid || invoiceDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.Number = invoiceDto.Number;
            entity.VAT = invoiceDto.VAT;
            entity.DeliveryPrice = invoiceDto.DeliveryPrice;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update partial data for invoice record
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
        ///     PATCH /api/invoices/{id:int}
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
        public IActionResult UpdateInvoicePartial(int id, JsonPatchDocument<Invoice> invoice)

        {
            var entity = _unitOfWork.Invoices.GetById(id);
            if (!ModelState.IsValid || invoice is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            invoice.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Invoices.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete an invoice
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>message shows that record deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /invoices/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes an invoice</response>
        /// <response code="404">if there is no invoice record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteInvoiceAsync(int id)
        {

            var entity = _unitOfWork.Invoices.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.Invoices.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
