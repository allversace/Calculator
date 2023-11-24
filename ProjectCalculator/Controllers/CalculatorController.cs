using Microsoft.AspNetCore.Mvc;

namespace ProjectCalculator.Controllers
{
    /// <summary>
    /// Контроллер служащий для выполнения различных функций калькулятора.
    /// Функция суммы чисел
    /// Функция вычитания чисел
    /// Функция умножения чисел
    /// Функция деления чисел
    /// Функция вычисления числа в степени
    /// Функция вычислении числа в корне
    /// Функция суммы чисел
    /// </summary>
    [Route("api/CalculatorController")]
    [ApiController]
    public class CalculatorController : Controller
    {
        private const string ErrorMessageOur = "Возможно, вы ввели что-то неправильно.";
        private const string ErrorMessageDenominator = "Знаменатель не может иметь значение 0";
        private const string ErrorMessageSqrt = "Занчение не может быть отрицательным";

        /// <summary>
        /// Метод вычисления суммы чисел А и В
        /// </summary>
        /// <param name="numberOne"></param>
        /// <param name="numberTwo"></param>
        /// <returns></returns>
        [HttpGet("add")]
        public ActionResult<double> Add(string numberOne, string numberTwo)
        {
            if (numberOne != null && numberTwo != null && numberOne.All(char.IsDigit) && numberTwo.All(char.IsDigit))
            {
                return Ok(double.Parse(numberOne) + double.Parse(numberTwo));
            }

            return BadRequest(ErrorMessageOur);
        }

        /// <summary>
        /// Метод вычитания числа А из числа В
        /// </summary>
        /// <param name="numberOne"></param>
        /// <param name="numberTwo"></param>
        /// <returns></returns>
        [HttpGet("subtract")]
        public ActionResult<double> Subtract(string numberOne, string numberTwo)
        {
            if (numberOne != null && numberTwo != null && numberOne.All(char.IsDigit) && numberTwo.All(char.IsDigit))
            {
                return Ok(double.Parse(numberOne) - double.Parse(numberTwo));
            }

            return BadRequest(ErrorMessageOur);
        }

        /// <summary>
        /// Метод умножения числа А на число В
        /// </summary>
        /// <param name="numberOne"></param>
        /// <param name="numberTwo"></param>
        /// <returns></returns>
        [HttpGet("multiply")]
        public ActionResult<double> Multiply(string numberOne, string numberTwo)
        {
            if (numberOne != null && numberTwo != null && numberOne.All(char.IsDigit) && numberTwo.All(char.IsDigit))
            {
                return Ok(double.Parse(numberOne) * double.Parse(numberTwo));
            }

            return BadRequest(ErrorMessageOur);
        }

        /// <summary>
        /// Метод деления числа А на число В
        /// </summary>
        /// <param name="numberOne"></param>
        /// <param name="numberTwo"></param>
        /// <returns></returns>
        [HttpGet("divide")]
        public ActionResult<double> Divide(string numberOne, string numberTwo)
        {
            if (numberOne != null && numberTwo != null && numberOne.All(char.IsDigit) && numberTwo.All(char.IsDigit) && numberTwo != "0")
            {
                return Ok(double.Parse(numberOne) / double.Parse(numberTwo));
            }

            return BadRequest(ErrorMessageDenominator + " - " + ErrorMessageOur);
        }

        /// <summary>
        /// Метод вычисления числа в степени
        /// </summary>
        /// <param name="numberOne"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet("pow")]
        public ActionResult<double> Power(string numberOne, string numberTwo)
        {
            return Ok(Math.Pow(double.Parse(numberOne), double.Parse(numberTwo)));
        }

        /// <summary>
        /// Метод вычисления числа в корне
        /// </summary>
        /// <param name="numberOne"></param>
        /// <returns></returns>
        [HttpGet("sqrt")]
        public ActionResult<double> Sqrt(string numberOne)
        {
            if (double.Parse(numberOne) < 0 && numberOne.All(char.IsDigit))
            {
                return BadRequest(ErrorMessageSqrt + " - " + ErrorMessageOur);
            }

            return Ok(Math.Sqrt(double.Parse(numberOne)));
        }

        /// <summary>
        /// Метод для получения вводимого значения пользователя
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        [HttpPost("evaluate")]
        public ActionResult<double> Evaluate([FromBody] string expression)
        {
            try
            {
                NCalc.Expression expr = new NCalc.Expression(expression);
                double result = Convert.ToDouble(expr.Evaluate());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorMessageOur + ex.Message);
            }
        }
    }
}