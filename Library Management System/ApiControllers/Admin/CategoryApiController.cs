﻿using Library_Management_System.DTOs.Category;
using Library_Management_System.Services.Admin.Category;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.ApiControllers.Admin;
[Route("api/category-api/[action]/{id?}")]
[ApiController]
public class CategoryApiController(ICategoryService service) : ControllerBase
{
    private readonly ICategoryService _service = service ?? throw new ArgumentNullException(nameof(service));
    public IActionResult? Index()
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategory dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Category_Name))
        {
            return BadRequest(new
            {
                status = "error",
                message = "Category name cannot be empty"

            });
        }

        if (await _service.GetCategoryByNameAsync(dto.Category_Name))
            return Conflict( new
            {
                status = "error",
                message = "Category already exists"
            });
        
        if (await _service.CreateCategoryAsync(dto))
        {
            return Ok(new
            {
                status = "success",
                message = "Created Successfully"
            });
        }

        return StatusCode(401, new
        {
            status = "error",
            message = "Internal Server Error just to check"
        });

    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        if ( await _service.GetCategoryByIdAsync(id)==null)
            return BadRequest(new
            {
                status = "error",
                message = "Category does not exist"
            });
        if (await _service.DeleteCategoryByIdAsync(id))
        {
            return Ok(new
            {
                status = "success",
                message = "Deleted Successfully"
            });
        }
        return StatusCode(500, new
        {
            status = "error",
            message = "Internal Server Error just to check"
        });
    }

    public async Task<IActionResult> Update(int id, UpdateCategory dto)
    {
        if (await _service.GetCategoryByNameAsync(dto.Category_Name))
        {
            return Conflict(new
            {
                status = "error",
                message = "Category already exists"
            });
        }

        if (await _service.UpdateCategoryByIdAsync(dto))
        {
            return Ok(new
            {
                status = "success",
                message = "Updated Successfully"
            });
        }
        return StatusCode(500, new
        {
            status = "error",
            message = "Internal Server Error"
        });

    }
        
    }
