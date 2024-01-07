using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
        {
            Person person = new Person()
            {
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
                City = command.City,
                Deleted = command.Deleted
            };
            await _demoContext.Person.AddAsync(person, cancellationToken);
            await _demoContext.SaveChangesAsync(cancellationToken);
            return Ok("Created");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] int key, [FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var entity = await _demoContext.Person.FirstOrDefaultAsync(x => x.PersonId == command.PersonId, cancellationToken);
            if (entity == null) return BadRequest("Person not  exist");

            entity.PhoneNumber = command.PhoneNumber;
            entity.Name = command.Name;
            entity.City = command.City;

            await _demoContext.SaveChangesAsync(cancellationToken);
            return Ok(entity);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int key, CancellationToken cancellationToken)
        {
            var entity = await _demoContext.Person.FirstOrDefaultAsync(x => x.PersonId == key, cancellationToken);
            if (entity == null) return BadRequest("Person not  exist");
            entity.Deleted = true;
            await _demoContext.SaveChangesAsync(cancellationToken);
            return Ok(entity);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            return Ok("Patched");
        }
    }
}


public class CreatePersonCommand
{
    public int? PersonId { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? City { get; set; }

    public bool? Deleted { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }
}
