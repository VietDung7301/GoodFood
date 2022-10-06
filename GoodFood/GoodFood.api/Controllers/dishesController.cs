using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace GoodFood.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class dishesController : Controller
    {
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("{saler_id}")]
        public IActionResult Dishes(string saler_id,[FromBody] Dish dish)
        {
            try
            {
                string dish_id = System.Guid.NewGuid().ToString();

                string connectionString = "Server=localhost;Port=3306;Database=goodfood;Uid=root;Pwd=yasuo1234gg";
                var mySQLconnection = new MySqlConnection(connectionString);
                string InsertDish = "Insert into dishes (dish_id,saler_id,name,price) values (@dish_id,@saler_id,@name,@price)";
                var parameters = new DynamicParameters();
                parameters.Add("@dish_id", dish_id);
                parameters.Add("@saler_id", saler_id);
                parameters.Add("@name", dish.name);
                parameters.Add("@price", dish.price);
                int numberOfaffectedRows = mySQLconnection.Execute(InsertDish, parameters);
                if (numberOfaffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, dish_id);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch (MySqlException mysqlexception)
            {
                if (mysqlexception.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e003");
                }
                StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
            return StatusCode(StatusCodes.Status400BadRequest, "e001");
        }
    }
}
