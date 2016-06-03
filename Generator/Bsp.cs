using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class BSP : PCG
    {
        int _roomNum;
        int _roomSize;
        List<BspRoom> _roomList;
        

        void UpdateParam(int width, int height, int roomNumber, int roomSize)
        {
            base.UpdateParam(width, height);

            if (roomNumber > (PcGridWidth * PcGridHeight) / 50) roomNumber = (PcGridWidth * PcGridHeight) / 50;
            _roomNum = roomNumber;

            if (roomSize > (PcGridWidth * PcGridHeight)) roomSize = PcGridWidth * PcGridHeight;
            _roomSize = roomSize;
        }

        void GeneratePcgBsp(byte[][] g)
        {
            base.GeneratePcg(g);

            _roomList = new List<BspRoom>();
            var rootRoom = new BspRoom(0, 0, PcGridWidth, PcGridHeight, _roomSize);
            _roomList.Add(rootRoom);

            while (_roomList.Count - 1 < _roomNum)
            {
                var splitIndex = new Random().Next(_roomList.Count - 1);
                var splitRoom = _roomList[splitIndex];
                if (splitRoom.SplitBsp())
                {
                    _roomList.Add(splitRoom.LeftChild);
                    _roomList.Add(splitRoom.RightChiled);
                }
            }

            // go through each leaf and create room
            for (var i = 0; i < _roomList.Count - 1; i++)
            {
                var rm = _roomList[i];

                if(rm.LeftChild != null || rm.RightChiled != null) continue; // if has child leaf go to next
                
                rm.GenerateBspRoom();

                for (var j = rm.X; j < rm.X + rm.Width; j++)
                {
                    PcGrid[j][rm.Y] = 5;
                    PcGrid[j][rm.Y + rm.Height - 1] = 5;
                }
                for (var k = rm.Y; k < rm.Y + rm.Height; k++)
                {
                    PcGrid[rm.X][k] = 5;
                    PcGrid[rm.X + rm.Width - 1][k] = 5;
                }
            }
            
            // go through each leaf and create room
            for (var i = 0; i < _roomList.Count - 1; i++)
            {
                var rm = _roomList[i];

                if (rm.LeftChild != null || rm.RightChiled != null) continue; // if has child leaf go to next

                // Horizontal walls
                // fills grid with values represent where walls should be drawn
                for (var j = rm.RoomX1; j <= rm.RoomX2; j++)
                {
                    PcGrid[j][rm.RoomY1] = 2; // north wall
                    PcGrid[j][rm.RoomY2] = 2; // south wall
                }

                // vertical walls
                for (var k = rm.RoomY1; k <= rm.RoomY2; k++)
                {
                    PcGrid[rm.RoomX1][k] = 2; // West Wall
                    PcGrid[rm.RoomX2][k] = 2; // East Wall
                }

                // Create the Room
                for(var j = rm.RoomY1 + 1; j < rm.RoomY2; j++)
                    for (var k = rm.RoomX1 + 1; i < rm.RoomX2; i++)
                        PcGrid[k][j] = 1; // fill the room => 1 = traversable
            }
            ConnectBspRoom(rootRoom);
        }

        private void ConnectBspRoom(BspRoom room)
        {
            
        }
    }
}
