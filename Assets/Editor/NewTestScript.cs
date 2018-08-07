using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class NewTestScript {

    [Test]
    public void NewTestScriptSimplePasses() {
        // Use the Assert class to test conditions.
    }

    [Test]
    public void TestGenerate()
    {
        Chess960 chess = new Chess960();
        GameObject boardGo = new GameObject();
        Board board = boardGo.AddComponent<Board>();
        List<Square> squares = new List<Square>();
        for (int k = 0; k < 64; k++)
        {
            GameObject square = new GameObject();
            square.AddComponent<Square>();
            square.AddComponent<MeshRenderer>();
            squares.Add(square.GetComponent<Square>());
        }
        board.setSquares(squares);
        Piece piece = new Piece();
        piece.piece_name = "Tower";
        piece.team = 1;
        chess.GenerateSetUp(board.getSquares());
        Assert.IsNotEmpty(chess.getStartForPiece(piece.piece_name, 1));
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
