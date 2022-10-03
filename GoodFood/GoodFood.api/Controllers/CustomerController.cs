using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MySqlConnector;
using Dapper;
using GoodFood.api.Models;

namespace GoodFood.api.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [Route("api/dishes")]
        [HttpGet]
        public IActionResult getAllDishes()
        {
            try
            {
                var sqlConnection = new MySqlConnection(consts.dbConnection);

                string getAllDishesCommand = "select * from dishes";

                var dishes = sqlConnection.Query<Dish>(getAllDishesCommand);

                if (dishes != null)
                {
                    return StatusCode(StatusCodes.Status200OK, dishes);
                }

                return StatusCode(StatusCodes.Status400BadRequest, "null");
            } 
            catch (MySqlException)
            {
                throw;
            }
        }
    }
}
