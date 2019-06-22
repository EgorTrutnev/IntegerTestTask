using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegerTestTask.Models;
using IntegerTestTask.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace IntegerTestTask.Controllers
{
    //Контроллер, отвечающиё за формирование коробки с шарами
    [Route("api/[controller]")]
    [ApiController]
    public class BoxBallsController : Controller
    {
        Random rnd = new Random();
        int maxBallsCount = Convert.ToInt32(Resources.MaxBallsCount);

        // GET: api/BoxBalls/action
        [HttpGet("[action]")]
        public IActionResult GetBoxBalls(int ballscount = -1, int concentration = -1, int blackballs = -1, int whiteballs = -1)
        {
            //Проверка входных данных на валидность
            //Если в коробке меньше 1 но большое maxBallsCount шаров, сообщаем об ошибке
            if ((ballscount < 1 && ballscount != -1) || ballscount > maxBallsCount)
                return Ok(new Error(2000, $"Указанное количество шаров в коробке ('ballscount' = {ballscount} шт.) должно находится в границах от 1 до {maxBallsCount} или быть равно '-1' (для случайного значения) исходя из серверных ограничений."));

            //Если концентарция меньше -1 и больше 100, сообщаем об ошибке
            if (concentration < -1 || concentration > 100)
                return Ok(new Error(2001, $"Соотношение чёрных шаров к белым ('concentration' = {concentration} %) должно находится в границах от 0 до 100% или быть равно '-1' (для случайного значения)."));

            //Если в коробке чёрных шаров меньше -1, но большое maxBallsCount шаров, сообщаем об ошибке
            if (blackballs < -1 || blackballs > maxBallsCount)
                return Ok(new Error(2002, $"Указанное количество чёрных ('blackballs' = {blackballs} шт.) шаров в коробке должно находится в границах от 0 до {maxBallsCount} или быть равно '-1' (для случайного значения) исходя из серверных ограничений."));

            //Если в коробке белых шаров меньше -1, но большое максимально допустимой суммы всех шаров, сообщаем об ошибке
            if (whiteballs < -1 || whiteballs > maxBallsCount)
                return Ok(new Error(2003, $"Указанное количество белых ('whiteballs' = {whiteballs} шт.) шаров в коробке должно находится в границах от 0 до {maxBallsCount} или быть равно '-1' (для случайного значения) исходя из серверных ограничений."));

            //Если указанное количество чёрных шаров превышает общее количество, сообщаем об ошибке
            if (blackballs > ballscount && ballscount > 0)
                return Ok(new Error(2004, $"Указанное количество чёрных шаров ('blackballs' = {blackballs} шт.) не может превышать общее ('ballscount' = {ballscount} шт.) количество шаров в коробке."));

            //Если указанное количество белых шаров превышает общее количество, сообщаем об ошибке
            if (whiteballs > ballscount && ballscount > 0)
                return Ok(new Error(2005, $"Указанное количество белых шаров ('whiteballs' = {whiteballs} шт.) не может превышать общее ('ballscount' = {ballscount} шт.) количество шаров в коробке."));

            //Если концентрация чёрных шаров 0%, но их количество было указано больше 0, сообщаем об ошибке
            if (concentration == 0 && blackballs > 0)
                return Ok(new Error(2006, $"При процентном соотношении чёрных шаров к белым ('concentration' = {concentration} %), в корзине невозможно нахождение чёрных шаров."));

            //Если концентрация белых шаров 0%, но их количество было указано больше 0, сообщаем об ошибке
            if (concentration == 100 && whiteballs > 0)
                return Ok(new Error(2007, $"При процентном соотношении чёрных шаров к белым ('concentration' = {concentration} %), в корзине невозможно нахождение белых шаров."));

            //Если сумма чёрных и белов шаров равна 0, и хотя бы одна группа не задаётся случайным образом, сообщаем об ошибке
            if (blackballs + whiteballs == 0 && (blackballs != -1 || whiteballs != -1))
                return Ok(new Error(2008, $"Сумма чёрных ('blackballs' = {blackballs} шт.) и белых ('whiteballs' = {whiteballs} шт.) шаров в коробке не должна быть равна 0."));

            //Если сумма чёрных и белых шаров не равна общему количеству шаров в коробке, сообщаем об ошибке
            if (blackballs + whiteballs != ballscount && blackballs != -1 && whiteballs != -1 && ballscount != -1)
                return Ok(new Error(2009, $"Сумма чёрных ('blackballs' = {blackballs} шт.) и белых ('whiteballs' = {whiteballs} шт.) шаров должна быть равна общему ('ballscount' = {ballscount} шт.) количеству шаров в коробке."));

            //Если сумма чёрных и белых шаров больше максимально допустимой суммы всех шаров, сообщаем об ошибке
            if (blackballs + whiteballs > maxBallsCount && blackballs != -1 && whiteballs != -1)
                return Ok(new Error(2010, $"Сумма чёрных ('blackballs' = {blackballs} шт.) и белых ('whiteballs' = {whiteballs} шт.) шаров в коробке не должна быть больше {maxBallsCount} исходя из серверных ограничений."));

            //Если указанное количество всех шаров не равно сумме белых и чёрных (исходя из количества чёрных и их концентрации)
            if (ballscount > 0 && concentration >= 0 && blackballs >= 0)
                if ((int)Math.Round((double)blackballs * (100 - concentration) / concentration + blackballs) != ballscount)
                    return Ok(new Error(2011, $"Указанное количество всех шаров ('ballscount' = {ballscount}) не соответствует соотношению концентрации ('concentration' = {concentration}) и количеству чёрных шаров ('blackballs' = {blackballs})."));

            //Если указанное количество всех шаров не равно сумме белых и чёрных (исходя из количества белых и их концентрации чёрных)
            if (ballscount > 0 && concentration >= 0 && whiteballs >= 0)
                if ((int)Math.Round((double)whiteballs / (100 - concentration) * concentration + whiteballs) != ballscount)
                    return Ok(new Error(2012, $"Указанное количество всех шаров ('ballscount' = {ballscount}) не соответствует соотношению концентрации ('concentration' = {concentration}) и количеству белых шаров ('whiteballs' = {whiteballs})."));

            //Если указанное количество чёрных и белых шаров не соответствует указанной концентрации чёрных шаров к белым
            if (concentration >= 0 && blackballs >= 0 && whiteballs >= 0)
                if (100 - (int)Math.Round((double)whiteballs * concentration / blackballs) != concentration)
                    return Ok(new Error(2013, $"Указанная концентрация ('concentration' = {concentration}) не соответствует реальной концентрации при количестве чёрных ('blackballs' = {blackballs}) и белых ('whiteballs' = {whiteballs}) шаров."));

            //Если указанное количество чёрных шаров и концентрация при нахождении белых шаров выводят значение последнего за пределы максимально допустимого порога
            if (concentration > 0 && blackballs > 0)
                if ((int)Math.Round((double)(100 - concentration) * blackballs / concentration) + blackballs > maxBallsCount)
                    return Ok(new Error(2014, $"Количество всех шаров ('ballscount' = {(int)Math.Round((double)(100 - concentration) * blackballs / concentration) + blackballs}), полученное при указанном количестве чёрных шаров ('blackballs' = {blackballs}) и концентрации ('concentration' = {concentration}) превышает максимально допустимое количество ('maxBallsCount' = {maxBallsCount})."));

            //Если указанное количество белых шаров и концентрация при нахождении чёрных шаров выводят значение последнего за пределы максимально допустимого порога
            if (concentration > 0 && whiteballs > 0)
                if ((int)Math.Round((double)concentration / (100 - concentration) * whiteballs) + whiteballs > maxBallsCount)
                    return Ok(new Error(2015, $"Количество всех шаров ('ballscount' = {(int)Math.Round((double)concentration / (100 - concentration) * whiteballs) + whiteballs}), полученное при указанном количестве белых шаров ('whiteballs' = {whiteballs}) и концентрации ('concentration' = {concentration}) превышает максимально допустимое количество ('maxBallsCount' = {maxBallsCount})."));
           
            //Если хотя бы у одной переменной указано неизвестное значение
            if (ballscount == -1 || concentration == -1 || blackballs == -1 || whiteballs == -1)
            {
                //Если у всех переменных неизвестные значения
                if (ballscount == -1 && concentration == -1 && blackballs == -1 && whiteballs == -1)
                {
                    ballscount = rnd.Next(1, maxBallsCount);
                    blackballs = rnd.Next(ballscount);
                    whiteballs = ballscount - blackballs;
                    concentration = (int)Math.Round((double)blackballs / ballscount * 100);
                }
                //Если общее количество шаров не указано
                else if (ballscount == -1)
                {
                    //Если концентрация  не указана
                    if (concentration == -1)
                    {
                        //Если количество чёрных шаров не указано
                        if (blackballs == -1)
                            blackballs = RandomBallsCount(maxBallsCount, whiteballs);

                        //Иначе не указно, сколько белых шаров в коробке
                        else if (whiteballs == -1)
                            whiteballs = RandomBallsCount(maxBallsCount, blackballs);

                        //Имея количество белых и чёрных шаров можем получить концентрацию
                        concentration = (int)Math.Round((double)blackballs / (blackballs + whiteballs) * 100);
                    }
                    //Иначе концентрация указана, но неизвестно количество белых или чёрных гаров
                    //Если неизвестно количество чёрных и белых шаров
                    else if (blackballs == -1 && whiteballs == -1)
                    {
                        if (concentration == 0)
                            blackballs = 0;
                        else
                            blackballs = (int)Math.Round((double)rnd.Next(1, maxBallsCount / 100 * concentration / concentration) * concentration);

                        if (concentration == 100)
                            whiteballs = 0;
                        else
                            whiteballs = (int)Math.Round((double)blackballs * (100 - concentration) / concentration);
                    }
                    //Если неизвестно количество чёрных шаров, значит все остальные парраметры уже заданы. Используем кол-во белых шаров и концентрацию для нахождения чёрных шаров
                    else if (blackballs == -1)
                        blackballs = (int)Math.Round((double)concentration / (100 - concentration) * whiteballs);

                    //Если неизвестно количество белых шаров, значит все остальные парраметры уже заданы. Используем кол-во чёрных шаров и концентрацию для нахождения белых шаров
                    else if (whiteballs == -1)
                        whiteballs = (int)Math.Round((double)(100 - concentration) * blackballs / concentration);

                    ballscount = blackballs + whiteballs;
                }
                //Иначе общее количество шаров указано
                //Если не указана концентрация
                else if (concentration == -1)
                {
                    //Если количество белых и чёрных шаров не указано, задаём количество чёрных случайно, а количество белых разницой между всеми и чёрными шарами в коробке 
                    if (blackballs == -1 && whiteballs == -1)
                    {
                        blackballs = RandomBallsCount(ballscount, whiteballs);
                        whiteballs = ballscount - blackballs;
                    }
                    //Если количество чёрных шаров не указано
                    else if (blackballs == -1)
                        blackballs = RandomBallsCount(ballscount, whiteballs);

                    //Иначе не указно, сколько белых шаров в коробке
                    else if (whiteballs == -1)
                    {
                        if (blackballs == -1)
                            whiteballs = RandomBallsCount(ballscount, blackballs);
                        else
                            whiteballs = ballscount - blackballs;
                    }

                    concentration = (int)Math.Round((double)blackballs / ballscount * 100);
                }
                //Иначе общее количество шаров и концентрация указаны
                //Если не указано колличество чёрных шаров
                else if (blackballs == -1)
                {
                    if (whiteballs == -1)
                        whiteballs = ballscount - (int)Math.Round((double)ballscount / 100 * concentration);

                    blackballs = ballscount - whiteballs;
                }
                //Иначе вычисляем, что не указано количество белых шаров
                else
                    whiteballs = ballscount - blackballs;
            }

            //Создаём массив шаров
            int[] ballsArray = Enumerable.Range(1, ballscount).ToArray();
            //Используя цикл по количеству всех шаров, начинаем идти с первого в коробке
            for (int i = 0; i < ballscount; i++)
            {
                //Случайным образом выбираем номер шара из коробки
                int number = rnd.Next(ballscount - 1);

                //Достаём шар с выбранным выше номером
                int temp = ballsArray[number];

                //Вместо вынутого шара кладём тот, что занимает позицию i в коробке
                ballsArray[number] = ballsArray[i];
                //Вместо поседнего шара кладём то, что вынули случайно, т.е. меняем оба шара местами
                ballsArray[i] = temp;
            }

            //Создаём коробку со всеми шарами и по-умолчанию делаем их белыми (значение '0')
            int[] ballsBox = new int[ballscount];
            //Создаём цикл размерность которого равна количетву чёрных шаров в коробке
            for (int i = 0; i < blackballs; i++)
                //Получая значение i-го элемента из массива ballsArray, меняем у элемента массива ballsBox, соответствующего полученному значению, цвет на чёрный (значение '1')
                ballsBox[ballsArray[i] - 1] = 1;

            //Массивы переменных для записи в них номера чёрных и белых шаров
            List<int> blackBallsNumbers = new List<int>(blackballs);
            List<int> whiteBallsNumbers = new List<int>(whiteballs);

            //Извлекам каждый шар в коробке
            for (int i = 0; i < ballsBox.Length; i++)
            {
                //Если достаём шар чёрного цвета
                if (ballsBox[i] == 1)
                {
                    blackBallsNumbers.Add(i + 1);
                }
                //Иначе цвет шара - белый
                else
                    whiteBallsNumbers.Add(i + 1);
            }

            BoxBalls boxBalls = new BoxBalls
            {
                BallsCount = ballscount,
                Concentration = concentration,
                BlackBalls = blackballs,
                WhiteBalls = whiteballs,
                BlackBallsNumbers = blackBallsNumbers,
                WhiteBallsNumbers = whiteBallsNumbers
            };

            return Ok(boxBalls);
        }

        //Перменная для случайного определения количества белых или чёрных шаров в коробке
        private int RandomBallsCount(int ballscount, int colorballs)
        {
            //Если количество белых шаров равно 0, то определяем случайное количество чёрных шаров от 1 до максимально разрешённого количества
            if (colorballs == 0)
                return rnd.Next(1, ballscount);
            //Если белых шаров не 0, тогда задаём случайное количество чёрных шаров от 0 до разницы максимального и кол-ва белых
            else
                return rnd.Next(ballscount - colorballs);
        }
    }
}
