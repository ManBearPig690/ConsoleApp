using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            // if there are children, connect them first
            if (room.LeftChild != null)
            {
                ConnectBspRoom(room.LeftChild);
                ConnectBspRoom(room.RightChiled);
            }

            
            if (room.LeftChild == null) return; // if leaf then stop

            // connect based on split direction
            if (room.Horizontal) // if split horizontally
            {
                var midpointX = room.X + room.Width/2; // the midpoint of node
                if (room.LeftChild.IsFilled && room.RightChiled.IsFilled) // if both are not split create corridor connection from south of left to north of right
                {
                    var offset = CheckCorner(midpointX, -1, room.LeftChild.RoomY2, room.RoomY1, room.Horizontal);
                    for (var i = room.LeftChild.RoomY2; i <= room.RightChiled.RoomY1; i++)
                    {
                        if (PcGrid[midpointX + offset][i] == 1 || PcGrid[midpointX + offset][i] == 4) break;
                        PcGrid[midpointX + offset][i] = 4;
                    }
                }
                else if (room.LeftChild.IsFilled && !room.RightChiled.IsFilled) // if left child is not split, create corridor from south wall of left tomidpoint of right
                {
                    var offset = CheckCorner(midpointX, -1, room.LeftChild.RoomY2, room.Y + room.Height - 1,
                        room.Horizontal);
                    for (var i = room.RightChiled.RoomY1; i >= room.Y; i--)
                    {
                        if (PcGrid[midpointX + offset][i] == 1 || PcGrid[midpointX + offset][i] == 4) break;
                        PcGrid[midpointX + offset][i] = 4;
                    }
                }
                else if (!room.LeftChild.IsFilled && room.RightChiled.IsFilled) // if right is not split create corridor from north of right to mid of left
                {
                    var offset = CheckCorner(midpointX, -1, room.RightChiled.RoomY1, room.Y, room.Horizontal);
                    for (var i = room.LeftChild.Height/2; i >= room.Y; i--)
                    {
                        if (PcGrid[midpointX + offset][i] == 1 || PcGrid[midpointX + offset][i] == 4) break;
                        PcGrid[midpointX + offset][i] = 4;
                    }
                }
                else // if both children split create corridor from mid left to mid right (connecting nodes of same level i believe)
                {
                    var offset = CheckCorner(midpointX, -1, room.LeftChild.Height/2,
                        room.LeftChild.Height + room.RightChiled.Height - 1, room.Horizontal);
                    for (var i = room.LeftChild.Height/2; i < room.LeftChild.Height + room.RightChiled.Height; i++)
                    {
                        // stop if we're in the second child and have reached a room tile or corridor tile
                        if ((PcGrid[midpointX + offset][i] == 1 || PcGrid[midpointX + offset][i] == 4) &&
                            i > room.RightChiled.Y) break;
                        PcGrid[midpointX + offset][i] = 4;
                    }
                }
            }
            else // if split vertically
            {
                var midPointY = room.Y + room.Height/2;
                if (room.LeftChild.IsFilled && room.RightChiled.IsFilled) // if bth are not split create from right of left to left of right
                {
                    var offset = CheckCorner(room.LeftChild.RoomX2, room.RightChiled.RoomX1, midPointY, -1,
                        room.Horizontal);
                    for (var i = room.LeftChild.RoomX2; i <= room.RightChiled.RoomX1; i++)
                    {
                        if (PcGrid[i][midPointY + offset] == 1 || PcGrid[i][midPointY + offset] == 4) break;
                        PcGrid[i][midPointY + offset] = 4;
                    }
                }
                else if (room.LeftChild.IsFilled && !room.RightChiled.IsFilled) // if left is not split create from right wall of left to midpint of right
                {
                    var offset = CheckCorner(room.LeftChild.RoomX2, room.X + room.Width - 1, midPointY, -1,
                        room.Horizontal);
                    for (var i = room.LeftChild.RoomX2; i < room.X + room.Width; i++)
                    {
                        if (PcGrid[i][midPointY + offset] == 1 || PcGrid[i][midPointY + offset] == 4) break;
                        PcGrid[i][midPointY + offset] = 4;
                    }
                }
                else if(!room.LeftChild.IsFilled && room.RightChiled.IsFilled) // if right is not split create corridor from left wall of right to midpoint of left
                {
                    var offset = CheckCorner(room.RightChiled.RoomX1, room.LeftChild.X, midPointY, -1, room.Horizontal);
                    for (var i = room.RightChiled.RoomX1; i >= room.LeftChild.X; i--)
                    {
                        if (PcGrid[i][midPointY + offset] == 1 || PcGrid[i][midPointY + offset] == 4) break;
                        PcGrid[i][midPointY + offset] = 4;
                    }
                }
                else // if both are split  create corridor from mid of left to mid of right (connecting nodes for same level i believe)
                {
                    var offset = CheckCorner(room.LeftChild.Width/2, room.LeftChild.Width + room.RightChiled.Width - 1,
                        midPointY, -1, room.Horizontal);
                    for (var i = room.LeftChild.Width/2; i < room.LeftChild.Width + room.RightChiled.Width; i++)
                    {
                        // stop if we're in the second child and have reached a room tile or corridor tile
                        if ((PcGrid[i][midPointY + offset] == 1 || PcGrid[i][midPointY + offset] == 4) && i > room.RightChiled.X) break;
                        PcGrid[i][midPointY + offset] = 4;
                    }
                }
            }
        }

        private int CheckCorner(int x1, int x2, int y1, int y2, bool horizontal)
        {
            if (horizontal)
            {
                if (PcGrid[x1][y1] == 2 && PcGrid[x1 + 1][y1] == 1) return 1; // top right
                if (PcGrid[x1][y2] == 2 && PcGrid[x1 + 1][y2] == 1) return 1; // bottom right
                if (PcGrid[x1][y1] == 2 && PcGrid[x1 - 1][y1] == 1) return -1; // top left
                if (PcGrid[x1][y2] == 2 && PcGrid[x1 - 1][y2] == 1) return -1; // bottom left
            } 
            else
            {
                if (PcGrid[x1][y1] == 2 && PcGrid[x1][y1 + 1] == 1) return 1; // bottom left
                if (PcGrid[x2][y2] == 2 && PcGrid[x2][y2 + 1] == 1) return 1; // bottom right
                if (PcGrid[x1][y1] == 2 && PcGrid[x1][y1 - 1] == 1) return -1; // top left
                if (PcGrid[x2][y2] == 2 && PcGrid[x2][y2 - 1] == 1) return -1; // top right
            }

            return 0;
        }
    }
}
