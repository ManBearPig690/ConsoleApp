using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Map
    {
        private const int MaxLeafSize = 20;
        private bool _didSplit = true;
        public int Widht;
        public int Height;

        public List<Leaf> Leafs = new List<Leaf>();
        public Leaf HelperLeaf; // healper leaf
        public Leaf RootLeaf;
        
        public Map(int w, int h)
        {
            Widht = w;
            Height = h;
            RootLeaf = new Leaf(0, 0, w, h);
            Leafs.Add(RootLeaf);
        }

        public void GenerateLeafs()
        {
            var random = new Random((int) DateTime.Now.Ticks);
            while (_didSplit)
            {
                _didSplit = false;
                foreach (var leaf in Leafs)
                {
                    if (leaf.LeftChild == null && leaf.RightChild == null)
                    {
                        if (leaf.Width > MaxLeafSize || leaf.Height > MaxLeafSize || random.Next(101) > 25) // 101 so the # is 1 - 100
                        {
                            if (leaf.Split())
                            {
                                Leafs.Add(leaf.LeftChild);
                                Leafs.Add(leaf.RightChild);
                                _didSplit = true;
                            }
                        }
                    }
                }
            }
            RootLeaf.CreateRooms();
        }
    }
}
