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
        }
    }
}
