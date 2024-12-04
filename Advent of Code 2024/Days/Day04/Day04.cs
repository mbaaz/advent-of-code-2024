namespace AoC.Y24.days;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/4</see>
public class Day04() : DaySolution(day: 4), IDaySolutionImplementation
{
    public bool IsActive => true;

    public override void Run(Action<string> output)
    {
        UseTestFile = false;

        base.Run(output);
    }

    public override void RunPart1(string[] input, Action<string> output)
    {
        RunWithTimer(output, () =>
        {
            var grid = new WordSearchGrid(input);
            
            var xmasOccurrences = grid.CalculateXmasOccurrences();

            output($"""
PART 1
         rows: {grid.Rows}
      columns: {grid.Columns}
       XMAS occurs this many times: {xmasOccurrences:n0} 
""");
        });
    }

    public override void RunPart2(string[] input, Action<string> output)
    {
        RunWithTimer(output, () =>
        {
            var grid = new WordSearchGrid(input);

            var crossMasOccurrences = grid.CalculateCrossMasOccurrences();

            output($"""
PART 2
         rows: {grid.Rows}
      columns: {grid.Columns}
       X-MAS occurs this many times: {crossMasOccurrences:n0} 
""");
        });
    }

    // ########################################################################################

    public class WordSearchGrid
    {
        private char[][] _grid;

        public int Rows { get; }
        public int Columns { get; }

        public WordSearchGrid(string[] input)
        {
            _grid = input.Select(row => row.ToCharArray()).ToArray();

            Rows = _grid.Length;
            Columns = _grid[0].Length;
        }

        public int CalculateXmasOccurrences()
        {
            var xmasOccurrences = 0;

            for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
            for (var columnIndex = 0; columnIndex < Columns; columnIndex++)
            {
                xmasOccurrences += PartOfHowManyXmas(rowIndex, columnIndex);
            }

            return xmasOccurrences;
        }

        public int CalculateCrossMasOccurrences()
        {
            var crossMasOccurrences = 0;

            for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
            for (var columnIndex = 0; columnIndex < Columns; columnIndex++)
            {
                crossMasOccurrences += PartOfHowManyCrossMas(rowIndex, columnIndex);
            }

            return crossMasOccurrences;
        }

        private char Get(int rowIndex, int columnIndex)
        {
            if(rowIndex < 0 || rowIndex>=Rows)
                return '.'; // rowIndex Out of Bounds

            if (columnIndex < 0 || columnIndex >= Columns)
                return '.'; // columnIndex Out of Bounds


            return _grid[rowIndex][columnIndex];
        }

        /// <summary>
        /// This will search the grid for X's, and sum how many XMAS
        /// they are a part of. All other letters will return 0 - even
        /// though they could be considered a part of XMAS as well.
        /// But we do not need to find all letters like that.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public int PartOfHowManyXmas(int rowIndex, int columnIndex)
        {
            if (Get(rowIndex, columnIndex) != 'X')
            {
                return 0;
            }

            var occurrences = 0;

            // Check horizontally forwards
            if(
                Get(rowIndex, columnIndex + 1) == 'M' && 
                Get(rowIndex, columnIndex + 2) == 'A' && 
                Get(rowIndex, columnIndex + 3) == 'S')
            {
                occurrences++;
            }

            // Check horizontally backwards
            if (
                Get(rowIndex, columnIndex - 1) == 'M' && 
                Get(rowIndex, columnIndex - 2) == 'A' && 
                Get(rowIndex, columnIndex - 3) == 'S')
            {
                occurrences++;
            }

            // Check vertically down
            if (
                Get(rowIndex + 1, columnIndex) == 'M' && 
                Get(rowIndex + 2, columnIndex) == 'A' && 
                Get(rowIndex + 3, columnIndex) == 'S')
            {
                occurrences++;
            }

            // Check vertically up
            if (
                Get(rowIndex - 1, columnIndex) == 'M' && 
                Get(rowIndex - 2, columnIndex) == 'A' && 
                Get(rowIndex - 3, columnIndex) == 'S')
            {
                occurrences++;
            }

            // Check diagonally up right
            if (
                Get(rowIndex - 1, columnIndex + 1) == 'M' && 
                Get(rowIndex - 2, columnIndex + 2) == 'A' && 
                Get(rowIndex - 3, columnIndex + 3) == 'S')
            {
                occurrences++;
            }

            // Check diagonally up left
            if (
                Get(rowIndex - 1, columnIndex - 1) == 'M' && 
                Get(rowIndex - 2, columnIndex - 2) == 'A' && 
                Get(rowIndex - 3, columnIndex - 3) == 'S')
            {
                occurrences++;
            }

            // Check diagonally down right
            if (
                Get(rowIndex + 1, columnIndex + 1) == 'M' && 
                Get(rowIndex + 2, columnIndex + 2) == 'A' && 
                Get(rowIndex + 3, columnIndex + 3) == 'S')
            {
                occurrences++;
            }

            // Check diagonally down left
            if (
                Get(rowIndex + 1, columnIndex - 1) == 'M' && 
                Get(rowIndex + 2, columnIndex - 2) == 'A' && 
                Get(rowIndex + 3, columnIndex - 3) == 'S')
            {
                occurrences++;
            }

            return occurrences;
        }

        /// <summary>
        /// This will search the grid for A's, and sum how many X-MAS
        /// they are a part of. All other letters will return 0 - even
        /// though they could be considered a part of X-MAS as well.
        /// But we do not need to find all letters like that.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public int PartOfHowManyCrossMas(int rowIndex, int columnIndex)
        {
            if (Get(rowIndex, columnIndex) != 'A')
            {
                return 0;
            }

            // Check for MAS cross MAS (top to bottom)
            if(
                Get(rowIndex - 1, columnIndex - 1) == 'M' && 
                Get(rowIndex + 1, columnIndex + 1) == 'S' && 
                Get(rowIndex - 1, columnIndex + 1) == 'M' && 
                Get(rowIndex + 1, columnIndex - 1) == 'S')
            {
                return 1;
            }

            // Check for MAS cross SAM (top to bottom)
            if (
                Get(rowIndex - 1, columnIndex - 1) == 'M' && 
                Get(rowIndex + 1, columnIndex + 1) == 'S' && 
                Get(rowIndex - 1, columnIndex + 1) == 'S' && 
                Get(rowIndex + 1, columnIndex - 1) == 'M')
            {
                return 1;
            }

            // Check for SAM cross MAS (top to bottom)
            if (
                Get(rowIndex - 1, columnIndex - 1) == 'S' && 
                Get(rowIndex + 1, columnIndex + 1) == 'M' && 
                Get(rowIndex - 1, columnIndex + 1) == 'M' && 
                Get(rowIndex + 1, columnIndex - 1) == 'S')
            {
                return 1;
            }

            // Check for SAM cross SAM (top to bottom)
            if (
                Get(rowIndex - 1, columnIndex - 1) == 'S' && 
                Get(rowIndex + 1, columnIndex + 1) == 'M' && 
                Get(rowIndex - 1, columnIndex + 1) == 'S' && 
                Get(rowIndex + 1, columnIndex - 1) == 'M')
            {
                return 1;
            }

            return 0;
        }
    }
}