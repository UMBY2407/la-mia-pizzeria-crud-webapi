using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Database;

namespace LaMiaPizzeria.Controllers.API
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class PizzaAPIController : Controller
	{
		public IActionResult GetPizzas()
		{
			using(PizzeriaContext db = new PizzeriaContext())
			{
				List<Pizza> pizzas = db.Pizzas.ToList();
				return Ok(pizzas);
			}
		}

		[HttpGet("{id}")]
		public IActionResult SearchById(int id)
		{
			using (PizzeriaContext db = new PizzeriaContext)
			{
				Pizza? pizzaToSearch = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

				if(pizzaToSearch != null)
				{
					return Ok(pizzaToSearch);
				}
				else
				{
					return NotFound();
				}
			}
		}

		[HttpGet]
		public IActionResult SearchByName (string name)
		{
			using(PizzeriaContext db = new PizzeriaContext())
			{
				Pizza? pizzaToSearch = db.Pizzas.Where(pizza => pizza.Name.Contains(name)).FirstOrDefault();

				if(pizzaToSearch != null)
				{
					return Ok(pizzaToSearch);
				} else
				{
					return NotFound();
				}
			}
		}

		[HttpPost]
		public IActionResult Create([FromBody] Pizza pizza)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(); 
			} else
			{
				using(PizzeriaContext db = new PizzeriaContext)
				{
					db.Pizzas.Add(pizza);
					db.SaveChanges();

					return Ok();
				}
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			using(PizzeriaContext db = new PizzeriaContext())
			{
				Pizza? pizzaToDelete = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

				if(pizzaToDelete != null)
				{
					db.Remove(pizzaToDelete);
					db.SaveChanges();
					return Ok();
				}else
				{
					return NotFound("Non ho trovato la pizza da eliminare");
				}
			}
		}
	}
}