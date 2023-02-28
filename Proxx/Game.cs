namespace Proxx
{
    public class Game
    {
        private int _size;
        private int _blackHoles;
        private Cell[,] _board;

        public Game(int size, int blackHoles)
        {
            _size = size;
            _blackHoles = blackHoles;
            InitBoard();
            IniiBlackHoles();
            ComputeAdjacentBlackHoles();
        }

        // we update a Cell as open and recursively update all adjacent Cells that have zero adjacent black holes.
        public void UpdateVisibleCells(int row, int col)
        {
            if (!_board[row, col].isOpen)
            {
                _board[row, col].isOpen = true;
                if (_board[row, col].adjacentBlackHoles == 0)
                {
                    for (int i = row - 1; i <= row + 1; i++)
                    {
                        for (int j = col - 1; j <= col + 1; j++)
                        {
                            if (i >= 0 && i < _size && j >= 0 && j < _size)
                            {
                                UpdateVisibleCells(i, j);
                            }
                        }
                    }
                }
            }
        }

        // in this method we loop over all cells on board that are not black holes and count how many adjacent Cells are black holes.
        private void ComputeAdjacentBlackHoles()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (!_board[i, j].isBlackHole)
                    {
                        int count = 0;
                        for (int k = i - 1; k <= i + 1; k++)
                        {
                            for (int l = j - 1; l <= j + 1; l++)
                            {
                                if (k >= 0 && k < _size && l >= 0 && l < _size && _board[k, l].isBlackHole)
                                {
                                    count++;
                                }
                            }
                        }
                        _board[i, j].adjacentBlackHoles = count;
                    }
                }
            }
        }

        // init board 
        private void InitBoard()
        {
            _board = new Cell[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _board[i, j] = new Cell();
                }
            }
        }

        //we randomly place black holes on the board until we have placed the desired number
        private void IniiBlackHoles()
        {
            int placedBlackHoles = 0;
            var rand = new Random();
            while (placedBlackHoles < _blackHoles)
            {
                var row = rand.Next(_size);
                var col = rand.Next(_size);
                if (!_board[row, col].isBlackHole)
                {
                    _board[row, col].isBlackHole = true;
                    placedBlackHoles++;
                }
            }
        }
    }
}
