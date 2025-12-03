using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CosmicLexicon.Foundation.Structures;
using Xunit;

namespace CosmicLexicon.Foundation.Structures
{
    public class LinkedListHelpersTests
    {
        private class Node
        {
            public int Value { get; set; }
            public Node? Next { get; set; }
        }

        [Fact]
        public void ToListWithValidLinkedListReturnsList()
        {
            // Arrange
            var node1 = new Node { Value = 1 };
            var node2 = new Node { Value = 2 };
            var node3 = new Node { Value = 3 };
            node1.Next = node2;
            node2.Next = node3;

            // Act
            var result = LinkedListHelpers.ToList(node1, n => n.Next);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result[0].Value);
            Assert.Equal(2, result[1].Value);
            Assert.Equal(3, result[2].Value);
        }

        [Fact]
        public void ToListWithCircularReferenceHandlesLoop()
        {
            // Arrange
            var node1 = new Node { Value = 1 };
            var node2 = new Node { Value = 2 };
            var node3 = new Node { Value = 3 };
            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node1; // Create circular reference

            // Act
            var result = LinkedListHelpers.ToList(node1, n => n.Next);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result[0].Value);
            Assert.Equal(2, result[1].Value);
            Assert.Equal(3, result[2].Value);
        }

        [Fact]
        public void ToListWithNullNextSelectorThrowsArgumentNullException()
        {
            // Arrange
            var node = new Node { Value = 1 };
            Expression<Func<Node, Node?>> nextSelector = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => LinkedListHelpers.ToList(node, nextSelector));
        }
    }
}