// C# program to find the 
// next optimal move for a player 
using System; 
using System.Collections.Generic; 

class Program 
{ 
class Move 
{ 
	public int row, col; 
}; 

static char player = 'x', opponent = 'o'; 
static bool playerMode = true, gameOver = false;

// This function returns true if there are moves 
// remaining on the board. It returns false if 
// there are no moves left to play. 
static Boolean isMovesLeft(char [,]board) 
{ 
	for (int i = 0; i < 3; i++) 
		for (int j = 0; j < 3; j++) 
			if (board[i, j] == '_') 
				return true; 
	return false; 
} 

// This is the evaluation function as discussed 
// in the previous article ( http://goo.gl/sJgv68 ) 
static int evaluate(char [,]b) 
{ 
	// Checking for Rows for X or O victory. 
	for (int row = 0; row < 3; row++) 
	{ 
		if (b[row, 0] == b[row, 1] && 
			b[row, 1] == b[row, 2]) 
		{ 
			if (b[row, 0] == opponent) 
				return +10; 
			else if (b[row, 0] == player) 
				return -10; 
		} 
	} 

	// Checking for Columns for X or O victory. 
	for (int col = 0; col < 3; col++) 
	{ 
		if (b[0, col] == b[1, col] && 
			b[1, col] == b[2, col]) 
		{ 
			if (b[0, col] == opponent) 
				return +10; 

			else if (b[0, col] == player) 
				return -10; 
		} 
	} 

	// Checking for Diagonals for X or O victory. 
	if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2]) 
	{ 
		if (b[0, 0] == opponent) 
			return +10; 
		else if (b[0, 0] == player) 
			return -10; 
	} 

	if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0]) 
	{ 
		if (b[0, 2] == opponent) 
			return +10; 
		else if (b[0, 2] == player) 
			return -10; 
	} 

	// Else if none of them have won then return 0 
	return 0; 
} 

// This is the minimax function. It considers all 
// the possible ways the game can go and returns 
// the value of the board 
static int minimax(char [,]board, 
				int depth, Boolean isMax) 
{ 
	int score = evaluate(board); 

	// If Maximizer has won the game 
	// return his/her evaluated score 
	if (score == 10) 
		return score; 

	// If Minimizer has won the game 
	// return his/her evaluated score 
	if (score == -10) 
		return score; 

	// If there are no more moves and 
	// no winner then it is a tie 
	if (isMovesLeft(board) == false) 
		return 0; 

	// If this maximizer's move 
	if (isMax) 
	{ 
		int best = -1000; 

		// Traverse all cells 
		for (int i = 0; i < 3; i++) 
		{ 
			for (int j = 0; j < 3; j++) 
			{ 
				// Check if cell is empty 
				if (board[i, j] == '_') 
				{ 
					// Make the move 
					board[i, j] = opponent; 

					// Call minimax recursively and choose 
					// the maximum value 
					best = Math.Max(best, minimax(board, 
									depth + 1, !isMax)); 

					// Undo the move 
					board[i, j] = '_'; 
				} 
			} 
		} 
		return best; 
	} 

	// If this minimizer's move 
	else
	{ 
		int best = 1000; 

		// Traverse all cells 
		for (int i = 0; i < 3; i++) 
		{ 
			for (int j = 0; j < 3; j++) 
			{ 
				// Check if cell is empty 
				if (board[i, j] == '_') 
				{ 
					// Make the move 
					board[i, j] = player; 

					// Call minimax recursively and choose 
					// the minimum value 
					best = Math.Min(best, minimax(board, 
									depth + 1, !isMax)); 

					// Undo the move 
					board[i, j] = '_'; 
				} 
			} 
		} 
		return best; 
	} 
} 

// This will return the best possible 
// move for the player 
static Move findBestMove(char [,]board) 
{ 
	int bestVal = -1000; 
	Move bestMove = new Move(); 
	bestMove.row = -1; 
	bestMove.col = -1; 

	// Traverse all cells, evaluate minimax function 
	// for all empty cells. And return the cell 
	// with optimal value. 
	for (int i = 0; i < 3; i++) 
	{ 
		for (int j = 0; j < 3; j++) 
		{ 
			// Check if cell is empty 
			if (board[i, j] == '_') 
			{ 
				// Make the move 
				board[i, j] = opponent; 

				// compute evaluation function for this 
				// move. 
				int moveVal = minimax(board, 0, false); 

				// Undo the move 
				board[i, j] = '_'; 

				// If the value of the current move is 
				// more than the best value, then update 
				// best/ 
				if (moveVal > bestVal) 
				{ 
					bestMove.row = i; 
					bestMove.col = j; 
					bestVal = moveVal; 
				} 
			} 
		} 
	} 

	Console.Write("The value of the best Move " + 
						"is : {0}\n\n", bestVal); 

	return bestMove; 
} 

static void Draw(char[,] board)
{
    Console.Clear();
    string output = "   0   1   2\n";
    output += "   ----------\n";
    output += $"0| {board[0, 0]} | {board[0, 1]} | {board[0, 2]} \n";
    output += " | ---------\n";
    output += $"1| {board[1, 0]} | {board[1, 1]} | {board[1, 2]} \n";
    output += " | ---------\n";
    output += $"2| {board[2, 0]} | {board[2, 1]} | {board[2, 2]} \n";
    Console.WriteLine(output);    
}

static void Input(char[,] board)
{
    string[] input;
    int i, j;

    //Player
    if (playerMode == true)
    {
        Console.WriteLine("Player");
        Console.WriteLine($"Choose your position(row,column) {player}: ");
        input = Console.ReadLine().Split(',', ' ');

        i = Convert.ToInt32(input[0]);
        j = Convert.ToInt32(input[1]);

        if (board[i,j] == '_')
        {
            board[i, j] = player;
            playerMode = false;
        }
        else
        {
            Console.WriteLine("This cell is already taken");
            Console.ReadKey();
            playerMode = true;
        }

    }

    //Opponent
    else
    {
            Move bestMove = findBestMove(board);
            i = bestMove.row;
            j = bestMove.col; 
            board[i, j] = opponent;
            playerMode = true;
    }
}
static void CheckWinState(char[,] board)
{
    // rows - player
    if (board[0,0] == player && board[0,1] == player && board[0,2] == player)
    {
        Console.WriteLine("Player has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[1, 0] == player && board[1, 1] == player && board[1, 2] == player)
    {
        Console.WriteLine("Player has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[2, 0] == player && board[2, 1] == player && board[2, 2] == player)
    {
        Console.WriteLine("Player has won");
        Console.ReadKey();
        gameOver = true;
    }

    //columns - player
    if (board[0, 0] == player && board[1, 0] == player && board[2, 0] == player)
    {
        Console.WriteLine("Player has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[0, 1] == player && board[1, 1] == player && board[2, 1] == player)
    {
        Console.WriteLine("Player has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[0, 2] == player && board[1, 2] == player && board[2, 2] == player)
    {
        Console.WriteLine("Player has won");
        Console.ReadKey();
        gameOver = true;
    }

    // diagonal - player
    if (board[0,0] == player && board[1,1] == player && board[2,2] == player)
    {
        Console.WriteLine("Player has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
    {
        Console.WriteLine("Player has won");
        Console.ReadKey();
        gameOver = true;
    }

    // rows - opponent
    if (board[0, 0] == opponent && board[0, 1] == opponent && board[0, 2] == opponent)
    {
        Console.WriteLine("Opponent has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[1, 0] == opponent && board[1, 1] == opponent && board[1, 2] == opponent)
    {
        Console.WriteLine("Opponent has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[2, 0] == opponent && board[2, 1] == opponent && board[2, 2] == opponent)
    {
        Console.WriteLine("Opponent has won");
        Console.ReadKey();
        gameOver = true;
    }

    // columns - opponent
    if (board[0, 0] == opponent && board[1, 0] == opponent && board[2, 0] == opponent)
    {
        Console.WriteLine("Opponent has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[0, 1] == opponent && board[1, 1] == opponent && board[2, 1] == opponent)
    {
        Console.WriteLine("Opponent has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[0, 2] == opponent && board[1, 2] == opponent && board[2, 2] == opponent)
    {
        Console.WriteLine("Opponent has won");
        Console.ReadKey();
        gameOver = true;
    }

    // diagonal - opponent
    if (board[0, 0] == opponent && board[1, 1] == opponent && board[2, 2] == opponent)
    {
        Console.WriteLine("Opponent has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if (board[0, 2] == opponent && board[1, 1] == opponent && board[2, 0] == opponent)
    {
        Console.WriteLine("Opponent has won");
        Console.ReadKey();
        gameOver = true;
    }
    else if(board[0,0] != '_' && board[0,1] != '_' && board[0,2] != '_' 
        && board[1,0] != '_' && board[1,1] != '_' && board[1,2] != '_'
        && board[2,0] != '_' && board[2,1] != '_' && board[2,2] != '_')
    {
        Console.WriteLine("Draw");
        Console.ReadKey();
        gameOver = true;
    }
}

        static void CheckRestartGame(char[,] board)
        {
            if(gameOver == true)
            {
                Console.WriteLine("Do you want to play again? y/n");
                char input = Console.ReadLine()[0];
                if(input == 'y')
                {                    
                    gameOver = false;
                    playerMode = true;
                    for(int i = 0; i < 3; i++)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            board[i,j] = '_';
                        }
                    }
                    Draw(board);
                } 
            }
        }

// Driver code 
public static void Main(String[] args) 
{ 
    char [,]board = {{ '_', '_', '_' }, 
                     { '_', '_', '_' }, 
                     { '_', '_', '_' }}; 

    Draw(board);

    while(gameOver == false)
    {
        Input(board);
        Draw(board);
        CheckWinState(board);
        CheckRestartGame(board);
    }
} 
} 


