using Domain.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ODataRestApi.Controllers
{
    public class PersonController : ODataController
    {
        private readonly DemoContext _demoContext;

        public PersonController(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }

        [EnableQuery]
        public IQueryable<Person> Get()
        {
            return _demoContext.Person.AsQueryable();
        }
    }
}
