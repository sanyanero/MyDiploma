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
    public class QuestionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/{id:guid}")]
        public async Task<IActionResult> GetQuestion(Guid id)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null !");
            }

            if (!await _unitOfWork.QuestionsRepository.Exist(id))
            {
                return NotFound($"Item {id} doesn't exist!");
            }

            var question = await _unitOfWork.QuestionsRepository.GetByIdAsync(id);

            return Ok(_mapper.Map<QuestionDto>(question));
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions([FromQuery] int page, [FromQuery] int count, [FromQuery] string text, [FromQuery] bool isFullMatch)
        {
            var filters = new List<Expression<Func<Question, bool>>>();
            
            if (text != null)
            {
                if (isFullMatch) // if true = search as a full match
                {
                    filters.Add(p => p.QuestionText == text);
                }
                else
                {
                    filters.Add(p => p.QuestionText.Contains(text));
                }
            }


            var questions = await _unitOfWork.QuestionsRepository.GetPagedAsync(filters: filters, count: count, page: page);
            var questionsCount = await _unitOfWork.QuestionsRepository.CountAsync(filters: filters);
            var pageReturnModel = new PageReturnModel<QuestionDto>
            {
                Items = _mapper.Map<IEnumerable<QuestionDto>>(questions),
                TotalCount = questionsCount,
                CurrentPage = page
            };

            return Ok(pageReturnModel);
        }

        [HttpGet]
        [Route("grouped")]
        public IActionResult GetQuestionsGroupedBy([FromQuery] int page, [FromQuery] int count)
        {

            var questionsGroup = _unitOfWork.QuestionsRepository.GroupBy(p => p.QuestionText, page, count);
            var questionsGroupCount = _unitOfWork.QuestionsRepository.GroupCount(p => p.QuestionText);
            var pageReturnModel = new PageReturnModel<GroupModel<string>>
            {
                Items = questionsGroup,
                TotalCount = questionsGroupCount,
                CurrentPage = page
            };

            return Ok(pageReturnModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestions([FromBody] QuestionDto model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }

            model.Id = null; //ensure that the object will be created as a new object
            var question = _mapper.Map<Question>(model);// the same behaviour as commented above

            question = await _unitOfWork.QuestionsRepository.AddAsync(question);

            await _unitOfWork.Save();
            return Ok(_mapper.Map<QuestionDto>(question));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] QuestionDto model, Guid id)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }
            if (id == null && model.Id != id)
            {
                return BadRequest("ID can't be null !");
            }

            if (!await _unitOfWork.QuestionsRepository.Exist(id))
            {
                return NotFound($"Item {id} doesn't exist!");
            }
            //var product = new Product { Name = model.Name, Type = model.Type};
            var question = _mapper.Map<Question>(model);// the same behaviour as commented above

            question = _unitOfWork.QuestionsRepository.Update(question);
            await _unitOfWork.Save();
            return Ok(_mapper.Map<QuestionDto>(question));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null !");
            }

            if (!await _unitOfWork.QuestionsRepository.Exist(id))
            {
                return NotFound($"Item {id} doesn't exist!");
            }

            var question = await _unitOfWork.QuestionsRepository.GetByIdAsync(id);

            _unitOfWork.QuestionsRepository.Delete(question);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}