using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Product;

namespace Task5EpamCourse.Models.Purchase
{
    public class CreatePurchaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, ввыберите дату")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите имя клиента")]
        [DataType(DataType.Text)]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите номер телефона")]
        [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$", ErrorMessage = "Неверно введет номер")]
        public string ClientTelephone { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название продукта")]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите цену продукта")]
        [DataType(DataType.Currency,ErrorMessage = "Неверный ввод цены")]
        public double Price { get; set; }
    }
}