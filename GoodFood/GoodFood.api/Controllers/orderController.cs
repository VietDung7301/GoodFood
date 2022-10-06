using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace GoodFood.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  


    public class orderController : ControllerBase
       
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("{saler_id}")]
        public IActionResult OrderSaler(string saler_id)
        {
            try
            {
                //return order_id ten cua nguoi mua-mon an -so luong-ngay dat-(customer_name-dish_name-qunantity-date time)-total price-order_type
                string connectionString = "Server=localhost;Port=3306;Database=goodfood;Uid=root;Pwd=yasuo1234gg";
                var mySQLconnection = new MySqlConnection(connectionString);
                string GetOrder = "SELECT order_id,customer_name,order_time,order_type FROM orders,customers WHERE orders.customer_id=customers.customer_id AND saler_id='" + saler_id + "'";
                var ListOrderOfsaler = mySQLconnection.Query<OrderSalerFormat>(GetOrder);

                foreach (var tmpItem in ListOrderOfsaler)
                {
                    string getlistDish = "Select name,quantity,quantity*price As price from orderdetails,dishes where dishes.dish_id=orderdetails.dish_id AND order_id='" + tmpItem.order_id + "'";
                    tmpItem.Orderdish = mySQLconnection.Query<OrderDishes>(getlistDish);
                    tmpItem.totalPrice = 0;
                    foreach (var PriceOfdish in tmpItem.Orderdish)
                    {
                        tmpItem.totalPrice += PriceOfdish.price;
                    }
                }
                if (ListOrderOfsaler.First<OrderSalerFormat>() != null)
                {
                    return StatusCode(StatusCodes.Status200OK, ListOrderOfsaler);
                }
                else
                {
                    return BadRequest("Does't have any orders");
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
        [HttpPut]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("{order_id}")]
        public IActionResult acceptOrder(string order_id,int order_type)
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=goodfood;Uid=root;Pwd=yasuo1234gg";
                var mySQLconnection = new MySqlConnection(connectionString);
                string SetDeleteOrder = " Update orders set order_type = @num  WHERE order_id = '" + order_id + "'";
                var parameter = new DynamicParameters();
                parameter.Add("@num", order_type);
                int numberOfaffectedRows = mySQLconnection.Execute(SetDeleteOrder, parameter);
                if (numberOfaffectedRows > 0)
                {
                    return Ok("Took order");
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


