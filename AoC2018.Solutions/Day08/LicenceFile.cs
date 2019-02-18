namespace AoC2018.Solutions.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LicenceFile
    {
        public Node[] RootNodes { get; set; }

        public static LicenceFile Parse(string input)
        {
            int[] numbers = input.Split(' ').Select(int.Parse).ToArray();
            int pointer = 0;

            var nodes = new List<Node>();

            while (pointer < numbers.Length)
            {
                nodes.Add(ReadNode(numbers, ref pointer));
            }

            return new LicenceFile
            {
                RootNodes = nodes.ToArray(),
            };
        }

        public static Node ReadNode(int[] numbers, ref int pointer)
        {
            int childNodesCount = numbers[pointer++];
            int metadataCount = numbers[pointer++];

            var childNodes = new List<Node>();

            for (int i = 0; i < childNodesCount; i++)
            {
                childNodes.Add(ReadNode(numbers, ref pointer));
            }

            var result = new Node
            {
                Children = childNodes.ToArray(),
                Metadata = new int[metadataCount],
            };

            Array.Copy(numbers, pointer, result.Metadata, 0, metadataCount);

            pointer += metadataCount;

            return result;
        }
    }
}
