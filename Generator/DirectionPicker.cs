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
        private readonly DirectionType _previousDirection;
        private readonly int _changeDirectionmodifier;

        
        public bool HasNextDirection
        {
            get { return _directionsPicked.Count < 4; }
        }

        public bool MustChangeDirection
        {
            get
            {
                // 100 will always change
                // 0 will never change
                return ((_directionsPicked.Count > 0)||(_changeDirectionmodifier > new Random().Next(0, 100)));
            }
        }

        public DirectionPicker(DirectionType previousDirection, int changeDirectionModifier)
        {
            _previousDirection = previousDirection;
            _changeDirectionmodifier = changeDirectionModifier;
        }

        public DirectionType GetNextDirection()
        {
            if(!HasNextDirection)throw new InvalidOperationException("No directions available.");

            DirectionType directionPicked;

            do
            {
                directionPicked = MustChangeDirection ? (DirectionType) new Random().Next(4) : _previousDirection;
            } while (_directionsPicked.Contains(directionPicked));

            _directionsPicked.Add(directionPicked);

            return directionPicked;
        }
    }
}
