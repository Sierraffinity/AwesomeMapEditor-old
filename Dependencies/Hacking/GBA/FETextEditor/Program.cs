using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Nintenlord.Collections.Trees;
using System.Collections.Generic;
using System.Xml.Serialization;
using FETextEditor.DefaultFreeSpace;

namespace FETextEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IApplication app = new App();
            Form1 ui = new Form1(app);
            app.CurrentUI = ui;
            Application.Run(ui);

            //ROM rom = new ROM();
            //rom.CRC32 = 0xFFFF;
            //rom.GameTitle = "dfh";
            //rom.MakerCode = "gadjf";
            //rom.GameCode = "test";
            //var space = new Space();
            //space.Usage = "Free";
            //space.SpaceArea.Add(new OffsetSizePair(1, 1));
            //space.SpaceArea.Add(new OffsetSizePair(15, 10));
            //rom.ROM_space.Add(space);
            //rom.SaveToFile("test.xml");

            //ROM rom2 = ROM.LoadFromFile("test.xml");
        }

        public static BinaryTree<ushort> GetHuffmanTree(byte[] data, int index, int count)
        {
            IntNode[] rawTree = new IntNode[count];
            unsafe 
            {
                fixed(IntNode* ptr = rawTree)
                {
                    Marshal.Copy(data, index, (IntPtr)ptr, count * sizeof(IntNode));
                }
            }
            int topIndex = count - 1;//Make into a parameter?
            return MakeTree(topIndex, rawTree);
        }

        private static BinaryTree<ushort> MakeTree(int headIndex, IntNode[] nodes)
        {
            IntNode head = nodes[headIndex];
            if (head.IsLeaf)
            {
                return new BinaryTree<ushort>(head.Value);
            }
            else
            {
                return new BinaryTree<ushort>(
                    MakeTree(head.Left, nodes),
                    MakeTree(head.Right, nodes)
                    );
            }
        }

        public static ushort[] RawCopy(this byte[] array)
        {
            var result = new ushort[array.Length / 2 + ((array.Length & 1) == 1 ? 1 : 0)];
            unsafe
            {
                fixed (ushort* ptr = result)
                {
                    Marshal.Copy(array, 0, new IntPtr(ptr), array.Length);
                }
            }
            return result;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
        [Serializable]
        private struct IntNode
        {
            [FieldOffset(0)]
            public ushort Left;
            [FieldOffset(2)]
            public ushort Right;

            public bool IsLeaf
            {
                get { return Right == ushort.MaxValue; }
            }
            public ushort Value
            {
                get { return Left; }
            }

            public override string ToString()
            {
                if (IsLeaf)
                {
                    return string.Format("Leaf: {0}", Value);
                }
                else
                {
                    return string.Format("Branch: {0} {1}", Left, Right);
                }
            }
        }        
        
        /// <summary>
        /// Node representation invented by Zahlman.
        /// </summary>
        /// <remarks> 
        /// If the value is even, this node is a leaf, and the value (divided by 2)
        /// is a symbol. Otherwise, the value is the number of nodes in the left
        /// subtree. (In a Huffman tree, there are no nodes with a single child,
        /// because that would correspond to a completely useless bit in the
        /// encoding.)
        /// The root of the left subtree is at index x+1, and of the right subtree
        /// is x + left + 1, where x is the index of the current node.
        /// This works because the structure of the tree is such that every 
        /// subtree always contains an odd number of nodes. Proof by induction:
        /// each subtree is either a leaf (1 node), or else it contains 1 + an odd
        /// number (left subtree, assumed in the inductive step) + an odd number
        /// (right subtree, similarly) of nodes, which is odd.</remarks>
        [StructLayout(LayoutKind.Explicit, Pack = 2, Size = 2)]
        [Serializable]
        private struct ShortNode
        {
            [FieldOffset(0)]
            private short value;

            public bool IsLeaf
            {
                get { return (value & 1) == 0; }
            }
            public short Value
            {
                get { return (short)(value >> 1); }
            }

            public override string ToString()
            {
                if (IsLeaf)
                {
                    return string.Format("Leaf: {0}", Value);
                }
                else
                {
                    return string.Format("Branch: {0} {1}", 1, value + 1);
                }
            }
        }

        //public IntNode GetLeft(IntNode node, IList<IntNode> nodes)
        //{
        //    return nodes[node.Left];
        //}

        //public IntNode GetLeft(IntNode node, IList<IntNode> nodes)
        //{
        //    return nodes[node.Right];
        //}
    }
}
