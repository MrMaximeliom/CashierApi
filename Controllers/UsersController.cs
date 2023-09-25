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
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IFileService _fileService;

        public UsersController(IUnitOfWork unitOfWork, IFileService fileService = null)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// 
        /// <returns>A list of all users</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /users
        ///
        /// </remarks>
        /// <response code="200">Returns all users</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync()
        {

            var result = await _unitOfWork.Users.GetAllAsync();
            var resultDto = result.Adapt<UserDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets user record by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested user record by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /users/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Returns user record with submitted id</response>
        /// <response code="404">Returns not found if there is no user with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {

            var result = await _unitOfWork.Users.GetByIdAsync(id);
            var resultDto = result.Adapt<UserDto>();

            return Ok(resultDto);


        }

        /// <summary>
        /// Update a user 
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="imagePath"></param>
        /// <param name="imageFile"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="gender"></param>
        /// <param name="userType"></param>
        /// <returns>an updated user record </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /users/{id:int}
        ///     {
        ///        "id": 1,
        ///        "firstName": "first_name",
        ///        "lastName": "last_name",
        ///        "imagePath": "image_path",
        ///        "phoneNumber "096565453",
        ///        "gender": "male",
        ///        "userType": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated user</response>
        /// <response code="404">if there is no user record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult UpdateUserAsync(int id, UserDto userDto)
        {

            var entity = _unitOfWork.Users.GetById(id);

            if (!ModelState.IsValid || userDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.FirstName = userDto.FirstName;
            entity.LastName = userDto.LastName;
            entity.Email = userDto.Email;
            entity.PhoneNumber = userDto.PhoneNumber;
            entity.ImagePath = userDto.ImagePath;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update user image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imageFile"></param>
        /// <returns>A message shows that user image uploaded successfully </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users/{id:int}/image
        ///     {
        ///        "id": 1
        ///        "imageFile": "image_file"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns user image uploaded successfully </response>
        /// <response code="400">If submitted data was wrong</response>
        /// <response code="404">If there is no user with submitted id</response>
        // limit uploaded images to 2mb size
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequestFormLimits(MultipartBodyLengthLimit = 2097152)]
        //[Authorize(Roles = $"{UsersRolesNames.AdminRole}, {UsersRolesNames.StaffRole}")]
        [HttpPut("{id:int}/image")]
        public async Task<IActionResult> UploadUserImage(int id, IFormFile imageFile)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user is null)
                return NotFound($"No user record found with this id: {id}");

            if (imageFile is null || imageFile.Length == 0)
                return BadRequest("No picture uploaded");

            var fileResult = await _fileService.SaveImageAsync(imageFile, "users");

            if (fileResult.Item1 == 0)
            {
                return BadRequest(fileResult.Item2);
            }

            user.ImagePath = "users\\" + fileResult.Item2;
            _unitOfWork.Complete();

            return Ok("User image uploaded successfully!");

        }

        /// <summary>
        /// Update partial data for user record
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
        ///     PATCH /api/users/{id:int}
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
        public IActionResult UpdateSubServicePartial(int id, JsonPatchDocument<User> users)

        {
            var entity = _unitOfWork.Users.GetById(id);
            if (!ModelState.IsValid || users is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            users.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Users.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete a user
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>message shows that record deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /users/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes a user</response>
        /// <response code="404">if there is no user record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult DeleteUserAsync(int id)
        {

            var entity = _unitOfWork.Users.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.Users.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
