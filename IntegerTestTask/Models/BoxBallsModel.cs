using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegerTestTask.Models
{
    //Класс, преставляющий собой объект коробки с чёрными и белыми шарами
    public class BoxBalls
    {
        public int BallsCount { get; set; }
        public int Concentration { get; set; }
        public int BlackBalls { get; set; }
        public int WhiteBalls { get; set; }
        public List<int> BlackBallsNumbers { get; set; }
        public List<int> WhiteBallsNumbers { get; set; }
    }
}
