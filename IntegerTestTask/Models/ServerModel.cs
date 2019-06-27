using IntegerTestTask.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegerTestTask.Models
{
    //Класс дял описания серверных ошибок
    public class Error
    {
        public int ErrorCode { get; private set; }
        public string Value { get; private set; }

        public Error(int errorCode, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Получено пустое значение value", nameof(value));

            ErrorCode = errorCode;
            Value = value;
        }
    }

    //Класс для описания ограничений, установленных не сервере
    public class Settings
    {
        public int MaxBallsCount = Convert.ToInt32(Resources.MaxBallsCount);
        public string VersionProduct = Resources.VersionProduct;
    }
}
