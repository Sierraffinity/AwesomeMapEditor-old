using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.ROMHacking.Collections;

namespace BinaryTreeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int>[] list = new BinaryTree<int>[6];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = new BinaryTree<int>(i);
            }
            BinaryTree<int> tree = new BinaryTree<int>(
                new BinaryTree<int>(list[0],list[1]),
                new BinaryTree<int>(
                    new BinaryTree<int>(list[2],list[3]),
                    new BinaryTree<int>(list[4],list[5])
                    )
                );

            string message = "Result:";
            foreach (var item in tree)
            {
                message += " " + item.ToString();
            }
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
