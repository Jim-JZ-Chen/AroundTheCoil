using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    public BoardPlace[,] board;
    public List<PlacementZoneScript> places = new List<PlacementZoneScript>(); // make the board through this

    //board size
    int maxX = 3;
    int maxY = 3;

	// Use this for initialization
	void Start () {
        board = new BoardPlace[maxX, maxY];
        int placedLoopNumb = 0;
        for (int x = 0; x < maxX; x++) {
            for (int y = 0; y < maxY; y++) {
                if (placedLoopNumb > places.Count) { break; }
                board[x,y] = new BoardPlace(places[placedLoopNumb].transform.position,new Vector2(x,y));
                places[placedLoopNumb].RecieveController(this);
                placedLoopNumb++;
            }
        }



	}



    public void PlacementUpdate(BoardPlace.BoardFill fill, Vector3 placeRef ) {
        //update placement
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (board[x, y].mainRef == placeRef)
                {
                    board[x, y].piece = fill;
                }
            }
        }
        //check for win then check for draw
        if (CheckWin(fill))
        {
            EndState(fill);
        }
        else if(CheckDraw()){
            EndState(BoardPlace.BoardFill.none);

        }
    }

    //this can probably be better, look into more elegant design!
    bool CheckWin(BoardPlace.BoardFill test) {
        //check cols
        for (int i = 0; i < maxX; i++) {
           // print(i+" | "+board[i, 0].piece +" | " + board[i, 1].piece + " | " + board[i, 2].piece);
                if (board[i,0].piece == test && board[i, 1].piece == test && board[i, 2].piece == test){
            return true;
             }
        }
        //check rows
        for (int i = 0; i < maxY; i++)
        {
            if (board[0, i].piece == test && board[1, i].piece == test && board[2, i].piece == test)
            {
                return true;
            }
        }
        //check diags
        if (board[0,0 ].piece == test && board[1, 1].piece == test && board[2, 2].piece == test)
        {
            return true;
        }
        if (board[0,2].piece == test && board[1, 1].piece == test && board[2, 0].piece == test)
        {
            return true;
        }
        // test failed
        return false;
    }

    //make this more robust as to end when conditions dictate a draw scenario
    bool CheckDraw() {
        //this is backwards, probably should be renamed checkNotDraw
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (board[x, y].piece == BoardPlace.BoardFill.none) {
                    return false;
                }
            }
        }
                return true;
    }


    void BoardWipe() {


    }

    void EndState(BoardPlace.BoardFill result) {
        //check who the winner is

        //and a winner is
        print("the winner is "+ result);


        //wipe board

    }
}

public struct BoardPlace {
    public enum BoardFill {Naught, Cross, none };
    public BoardFill piece;
   public Vector3 mainRef; // main reference idenification
    Vector2 gridPosition; // X and Y ref in grid

    public BoardPlace(Vector3 reference,Vector2 gridRef) {
        piece = BoardFill.none;
        mainRef = reference;
        gridPosition = gridRef;
    }
}
