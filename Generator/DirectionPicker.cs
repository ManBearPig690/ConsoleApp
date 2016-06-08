using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class DirectionPicker
    {
        private readonly List<DirectionType> _directionsPicked = new List<DirectionType>();

        public bool HasNextDirection
        {
            get { return _directionsPicked.Count < 4; }
        }

        public DirectionType GetNextDirection()
        {
            if(!HasNextDirection)throw new InvalidOperationException("No directions available.");

            DirectionType directionPicked;

            do
            {
                directionPicked = (DirectionType) new Random().Next(4);
            } while (_directionsPicked.Contains(directionPicked));

            _directionsPicked.Add(directionPicked);

            return directionPicked;
        }
    }
}
