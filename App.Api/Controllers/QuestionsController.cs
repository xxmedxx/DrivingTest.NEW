using App.DataBaseAccess;
using App.IRepository;
using App.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;

namespace Api.Controllers
{
    public class QuestionsController : ApiController
    {
        private readonly IRepository<Question> repo;
        public QuestionsController(IRepository<Question> repo)
        {
            this.repo = repo;
        }

        // GET: api/Questions
        public IEnumerable<QuestionModel> Get()
        {
            var question = repo.GetAll();
            return Mapper.Map<IEnumerable<QuestionModel>>(question);
        }

        // GET: api/Questions/5
        public QuestionModel Get(int id)
        {
            var question = repo.GetById(id);
            return Mapper.Map<QuestionModel>(question);
        }

        // POST: api/Questions
        public QuestionModel Post([FromBody]QuestionModel value)
        {
            if (value == null)
                return null;
            var obj = Mapper.Map<Question>(value);
            var question = repo.Insert(obj);
            return Mapper.Map<QuestionModel>(question);
        }

        // PUT: api/Questions/5
        public QuestionModel Put(int id, [FromBody]QuestionModel value)
        {
            var obj = Mapper.Map<Question>(value);
            var user = repo.Update(obj);
            return Mapper.Map<QuestionModel>(user);
        }

        // DELETE: api/Questions/5
        public int Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
