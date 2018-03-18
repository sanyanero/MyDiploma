using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using Test.DAL.Abstract;
using AutoMapper;
using Test.MODELS.Entities;
using Test.MODELS.DTO;
using System.Collections.Generic;
using Test.MODELS.Helpers;

namespace Test.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null !");
            }

            if (!await _unitOfWork.ProductsRepository.Exist(id))
            {
                return NotFound($"Item {id} doesn't exist!");
            }

            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);

            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int page, [FromQuery] int count, [FromQuery] string type, [FromQuery] string name, [FromQuery] bool isFullMatch)
        {
            var filters = new List<Expression<Func<Product, bool>>>();
            if (type != null)
            {
                if (isFullMatch) // if true = search as a full match
                {
                    filters.Add(p => p.Type == type);
                }
                else
                {
                    filters.Add(p => p.Type.Contains(type));
                }
            }

            if (name != null)
            {
                if (isFullMatch) // if true = search as a full match
                {
                    filters.Add(p => p.Name == name);
                }
                else
                {
                    filters.Add(p => p.Name.Contains(name));
                }
            }


            var products = await _unitOfWork.ProductsRepository.GetPagedAsync(filters: filters,orderBy:p=>p.Date, count: count, page: page);
            var productsCount = await _unitOfWork.ProductsRepository.CountAsync(filters: filters);
            var pageReturnModel = new PageReturnModel<ProductDTO>
            {
                Items = _mapper.Map<IEnumerable<ProductDTO>>(products),
                TotalCount = productsCount,
                CurrentPage = page
            };

            return Ok(pageReturnModel);
        }

        [HttpGet]
        [Route("grouped")]
        public IActionResult GetProductsGroupedBy([FromQuery] int page, [FromQuery] int count)
        {

            var productsGroup = _unitOfWork.ProductsRepository.GroupBy(p => p.Type, page, count);
            var productsGroupCount = _unitOfWork.ProductsRepository.GroupCount(p => p.Type);
            var pageReturnModel = new PageReturnModel<GroupModel<string>>
            {
                Items = productsGroup,
                TotalCount = productsGroupCount,
                CurrentPage = page
            };

            return Ok(pageReturnModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }

            model.Id = null; //ensure that the object will be created as a new object
            //var location = new Location { City = model.City, Country = model.Country};
            var product = _mapper.Map<Product>(model);// the same behaviour as commented above

            product = await _unitOfWork.ProductsRepository.AddAsync(product);

            await _unitOfWork.Save();
            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO model, Guid id)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }
            if (id == null && model.Id != id)
            {
                return BadRequest("ID can't be null !");
            }

            if (!await _unitOfWork.ProductsRepository.Exist(id))
            {
                return NotFound($"Item {id} doesn't exist!");
            }
            //var product = new Product { Name = model.Name, Type = model.Type};
            var product = _mapper.Map<Product>(model);// the same behaviour as commented above

            product = _unitOfWork.ProductsRepository.Update(product);
            await _unitOfWork.Save();
            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null !");
            }

            if (!await _unitOfWork.ProductsRepository.Exist(id))
            {
                return NotFound($"Item {id} doesn't exist!");
            }

            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);

            _unitOfWork.ProductsRepository.Delete(product);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}