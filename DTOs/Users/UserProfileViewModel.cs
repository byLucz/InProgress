using System;
using System.ComponentModel.DataAnnotations;

namespace XKANBAN.DTOs.Users
{
    public class UserProfileViewModel
    {
        public string Email { get; set; }

        [Display(Name = "ФИО")]
        [MaxLength(250)]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Логин пользователя обязателен")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
