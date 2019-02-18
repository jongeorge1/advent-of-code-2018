namespace AoC2018.Solutions.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LicenceFileExtensions
    {
        public static int MetadataChecksum(this LicenceFile file)
        {
            return file.RootNodes.MetadataChecksum();
        }

        private static int MetadataChecksum(this Node[] nodes)
        {
            return nodes.Sum(x => x.MetadataChecksum());
        }

        private static int MetadataChecksum(this Node node)
        {
            return node.Metadata.Sum() + node.Children.MetadataChecksum();
        }
    }
}
