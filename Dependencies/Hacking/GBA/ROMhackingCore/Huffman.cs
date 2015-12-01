using System;
using System.Collections.Generic;
using System.IO;
using Nintenlord.IO;
using Nintenlord.Collections.Trees;

namespace Nintenlord.ROMHacking
{
    public static class Huffman
    {
        public static BinaryTree<T> GetHuffmanTree<T>(IDictionary<T, int> items)
        {
            LinkedList<KeyValuePair<BinaryTree<T>, int>> trees = 
                new LinkedList<KeyValuePair<BinaryTree<T>, int>>();
            
            foreach (var item in items)
            {
                trees.AddLast(new KeyValuePair<BinaryTree<T>, int>(
                    new BinaryTree<T>(item.Key),
                    item.Value
                    ));
            }

            while (trees.Count > 1)
            {
                var last = trees.Last.Value;
                trees.RemoveLast();
                var notLast = trees.Last.Value;
                trees.RemoveLast();

                var newItem = new KeyValuePair<BinaryTree<T>, int>(
                    new BinaryTree<T>(last.Key, notLast.Key),
                    last.Value + notLast.Value
                    );

                if (trees.Count > 0)
                {
                    LinkedListNode<KeyValuePair<BinaryTree<T>, int>> node = trees.First;
                    while (node.Value.Value > newItem.Value && node.Next != null)
                    {
                        node = node.Next;
                    }
                    trees.AddAfter(node, newItem);
                }
                else
                {
                    trees.AddFirst(newItem);
                    break;
                }
            }

            return trees.First.Value.Key;
        }

        public static void DecompressData<T>(byte[] data, int index, int length, BinaryTree<T> tree, ICollection<T> toAddTo)
        {
            using (BitReader stream = new BitReader(new MemoryStream(data, index, length)))
            {
                while (stream.BaseStream.Position < stream.BaseStream.Length)
                {
                    toAddTo.Add(Read(stream, tree));
                }
            }
        }

        public static void DecompressDataUntil<T>(byte[] data, int index, Predicate<T> test, BinaryTree<T> tree, ICollection<T> toAddTo)
        {
            using (BitReader stream = new BitReader(new MemoryStream(data, index, data.Length - index)))
            {
                T last;
                do
                {
                    last = Read(stream, tree);
                    toAddTo.Add(last);
                }
                while (test(last));
            }
        }

        public static void DecompressDataUntil<T>(Stream stream, Predicate<T> test, BinaryTree<T> tree, ICollection<T> toAddTo)
        {
            BitReader reader = new BitReader(stream);
            
            T last;
            do
            {
                last = Read(reader, tree);
                toAddTo.Add(last);
            }
            while (!test(last));            
        }

        public static void DecompressDataUntil(Stream stream, Predicate<short> test, BinaryTree<short> tree, BinaryWriter output)
        {
            BitReader reader = new BitReader(stream);

            short last;
            do
            {
                last = Read(reader, tree);
                output.Write(last);
            }
            while (!test(last));            
        }

        public static void DecompressDataUntil(Stream stream, Predicate<ushort> test, BinaryTree<ushort> tree, BinaryWriter output)
        {
            BitReader reader = new BitReader(stream);

            ushort last;
            do
            {
                last = Read(reader, tree);
                output.Write(last);
            }
            while (!test(last));            
        }

        public static int GetCompDataLength<T>(Stream stream, Predicate<T> test, BinaryTree<T> tree)
        {
            long start = stream.Position;
            long length;
            BitReader reader = new BitReader(stream);

            T last;
            do
            {
                last = Read(reader, tree);
            }
            while (!test(last));
            length = stream.Position - start;

            return (int)length;
        }

        private static T Read<T>(BitReader reader, BinaryTree<T> tree)
        {
            BinaryTreeNode<T> currentNode = tree.Head;
            while (!currentNode.IsLeaf)
            {
                if (reader.ReadBit())
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    currentNode = currentNode.Left;
                }
            }
            return currentNode.Value;
        }

        public static void Compress<T>(IDictionary<T, bool[]> encoding, BitWriter writer, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                foreach (var boolean in encoding[item])
                {
                    writer.WriteBit(boolean);
                }
            }
        }

        public static bool IsOptimal<T>(IDictionary<T, bool[]> encoding, IDictionary<T, int> items)
        {
            return IsOptimal(encoding, items, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Can return false positives.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="encoding"></param>
        /// <param name="items"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool IsOptimal<T>(IDictionary<T, bool[]> encoding, IDictionary<T, int> items, IEqualityComparer<T> comp)
        {
            List<T> sortedItems = new List<T>(items.Keys);
            sortedItems.Sort((x, y) => items[x] - items[y]);

            if (encoding[sortedItems[sortedItems.Count - 1]].Length !=
                encoding[sortedItems[sortedItems.Count - 2]].Length
                )
            {
                return false;
            }

            int lastItem = items[sortedItems[0]];
            for (int i = 1; i < sortedItems.Count; i++)
            {                
                //(items[x] > items[y]) -> (encoding[x].Length <= encoding[y].Length)

                if (lastItem != items[sortedItems[i]] &&//Meaning items[x] > items[y]
                    encoding[sortedItems[i-1]].Length > encoding[sortedItems[i]].Length)
                {
                    return false;
                }
                lastItem = items[sortedItems[i]];
            }

            return true;
        }

    }
}
