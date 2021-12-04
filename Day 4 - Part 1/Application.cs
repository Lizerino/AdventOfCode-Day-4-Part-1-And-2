namespace Day_4___Part_1
{
    internal class Application
    {
        public int BoardsLeft { get; set; }

        public void Run()
        {
            string[] input = File.ReadAllLines("Input.txt");

            string[] BingoNumbers = input[0].Split(',');

            List<BingoBoard> bingoBoards = CreateBingoBoards(input);

            foreach (var bingoNumber in BingoNumbers)
            {
                foreach (var bingoBoard in bingoBoards)
                {
                    // Mark Board
                    MarkBoard(bingoNumber, bingoBoard);

                    // Check all rows for bingo
                    CheckRows(bingoNumber, bingoBoard);

                    // Check all columns for bingo
                    CheckColumns(bingoNumber, bingoBoard);
                }
            }
        }

        private void CheckColumns(string bingoNumber, BingoBoard bingoBoard)
        {
            for (int column = 0; column < 5; column++)
            {
                int markCounter = 0;
                for (int row = 0; row < 5; row++)
                {
                    if (bingoBoard.Board[row, column] == "")
                    {
                        markCounter++;
                    }
                }
                if (markCounter == 5 && bingoBoard.AlreadyWon == false)
                {
                    Winner(bingoBoard,bingoNumber);
                    bingoBoard.AlreadyWon = true;
                    break;
                }
            }
        }

        private void CheckRows(string bingoNumber, BingoBoard bingoBoard)
        {
            for (int row = 0; row < 5; row++)
            {
                int markCounter = 0;
                for (int column = 0; column < 5; column++)
                {
                    if (bingoBoard.Board[row,column]=="")
                    {
                        markCounter++;
                    }
                }
                if (markCounter == 5 && bingoBoard.AlreadyWon==false) 
                {
                    Winner(bingoBoard,bingoNumber);
                    bingoBoard.AlreadyWon = true;
                    break; 
                }
            }
        }

        private void Winner(BingoBoard bingoBoard,string bingoNumber)
        {
            int sum = 0;
            for (int row = 0; row < 5; row++)
            {                
                for (int column = 0; column < 5; column++)
                {
                    if (bingoBoard.Board[row, column] != "")
                    {
                        sum = sum + int.Parse(bingoBoard.Board[row, column]);
                    }
                }
            }
            sum = sum * int.Parse(bingoNumber);
                Console.WriteLine($"Board: {bingoBoard.BoardNumber} has won with a sum of {sum}");
        }

        private void MarkBoard(string bingoNumber, BingoBoard bingoBoard)
        {
            for (int column = 0; column < 5; column++)
            {
                for (int row = 0; row < 5; row++)
                {
                    if (bingoBoard.Board[row, column] == bingoNumber)
                    {
                        bingoBoard.Board[row, column] = "";
                    }
                }
            }
        }

        private List<BingoBoard> CreateBingoBoards(string[] input)
        {
            List<BingoBoard> bingoBoards = new();

            int bingoboardNumber = 1;
            for (int boardStart = 2; boardStart < input.Length; boardStart += 6)
            {
                BingoBoard bingoBoard = new(bingoboardNumber);
                bingoboardNumber++;
                for (int numberRow = 0; numberRow < 5; numberRow++)
                {
                    var counter = 0;
                    var numbers = input[boardStart+numberRow].Split(' ');
                    foreach (var number in numbers)
                    {
                        if (number != "")
                        {
                            bingoBoard.Board[numberRow, counter] = number;
                            counter++;
                        }
                    }
                }
                bingoBoards.Add(bingoBoard);
            }
            return bingoBoards;
        }

        internal class BingoBoard
        {
            public string[,] Board { get; set; }

            public bool AlreadyWon { get; set; }

            public int BoardNumber { get; set; }

            public BingoBoard(int counter)
            {
                Board = new string[5, 5];
                BoardNumber = counter;
                AlreadyWon = false;
            }
        }
    }
}