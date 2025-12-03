using CosmicLexicon.Foundation.Structures;

public class LinkedListHelpersTests
{
    [Fact]
    public void ToList_ConvertsLinkedListToList()
    {
        // Arrange
        var node1 = new Node { Value = 1 };
        var node2 = new Node { Value = 2 };
        var node3 = new Node { Value = 3 };
        node1.Next = node2;
        node2.Next = node3;

        // Act
        var list = LinkedListHelpers.ToList(node1, n => n.Next);

        // Assert
        Assert.Equal(3, list.Count);
        Assert.Equal(1, list[0].Value);
        Assert.Equal(2, list[1].Value);
        Assert.Equal(3, list[2].Value);
    }

    [Fact]
    public void ToList_HandlesCircularReference()
    {
        // Arrange
        var node1 = new Node { Value = 1 };
        var node2 = new Node { Value = 2 };
        var node3 = new Node { Value = 3 };
        node1.Next = node2;
        node2.Next = node3;
        node3.Next = node1; // Circular reference

        // Act
        var list = LinkedListHelpers.ToList(node1, n => n.Next);

        // Assert
        Assert.Equal(3, list.Count);
        Assert.Equal(1, list[0].Value);
        Assert.Equal(2, list[1].Value);
        Assert.Equal(3, list[2].Value);
    }

    private class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
    }
}
