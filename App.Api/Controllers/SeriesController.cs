using App.DataBaseAccess;
using App.IRepository;
using App.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class SeriesController : ApiController
    {
        private readonly IRepository<Serie> repo;
        public SeriesController(IRepository<Serie> repo)
        {
            this.repo = repo;
        }

        // GET: api/Series
        public IEnumerable<SeriesModel> Get()
        {
            var sereis = repo.GetAll();
            return Mapper.Map<IEnumerable<SeriesModel>>(sereis);
        }

        // GET: api/Series/5
        public SeriesModel Get(int id)
        {
            var sereis = repo.GetById(id);
            return Mapper.Map<SeriesModel>(sereis);
        }

        // POST: api/Series
        public SeriesModel Post([FromBody]SeriesModel value)
        {
            if (value == null)
                return null;
            var obj = Mapper.Map<Serie>(value);
            var series = repo.Insert(obj);
            return Mapper.Map<SeriesModel>(series);
        }

        // PUT: api/Series/5
        public SeriesModel Put(int id, [FromBody]SeriesModel value)
        {
            var obj = Mapper.Map<Serie>(value);
            var user = repo.Update(obj);
            return Mapper.Map<SeriesModel>(user);
        }

        // DELETE: api/Series/5
        public int Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
