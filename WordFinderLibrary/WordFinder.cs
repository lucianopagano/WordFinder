namespace WordFinderLibrary
{
    public class WordFinder : IWordFinder
    {
        /// <summary>
        /// this property contains the matrix
        /// </summary>
        private IEnumerable<string> _matrix { get; set; }


        /// <summary>
        /// The WordFinder constructor receives a set of strings which represents a character matrix. 
        /// The matrix size does not exceed 64x64, all strings contain the same number of characters.
        /// </summary>
        /// <param name="matrix"></param>
        public WordFinder(IEnumerable<string> matrix)
        {
            //first I validate the entrance matrix
            this.ValidateMatrix(matrix);

            _matrix = matrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordstream"></param>
        /// <returns></returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            List<string> top10Word = new List<string>();

            //get all the columns
            List<string> rowAndColumns = this.GetColumns();

            //I create a list with all the colums and the rows together
            rowAndColumns.AddRange(this._matrix.Select(row => row));

            List<WordStreamProcessResult> processResults = new List<WordStreamProcessResult>();

            //ask for the ocurrences of all the words in the streams
            processResults = wordstream.Select(word => new WordStreamProcessResult()
            {
                Word = word,
                Ocurrences = rowAndColumns.Where(s => s.ToLower().Contains(word.ToLower())).ToList().Count()
            }).ToList();

            //quit the element that not have ocurrences
            processResults.RemoveAll(pr => pr.Ocurrences == 0);

            //order the elements by the most occurrences to the lower occurrence and take the first 10
            top10Word = processResults.OrderByDescending(pr => pr.Ocurrences).Select(pr => pr.Word).Take(10).ToList();

            return top10Word;
        }

        /// <summary>
        /// this methods validate that the matrix is in the correct form
        /// </summary>
        /// <param name="matrix"></param>
        private void ValidateMatrix(IEnumerable<string> matrix)
        {
            if (matrix == null)
            {
                throw new MatrixException("Matrix can't be null");
            }

            //I get the Length of the first row and then I 
            //validate that all the rows are of same size of the
            //first element
            int countElements = matrix.First().Length;
            if (matrix.Any(row => row.Length != countElements))
            {
                //if any row is difrent of the first
                //it's because they dont have the same number of chars
                throw new MatrixException("Invalid rows size");
            }

            if (matrix.Count() > 64)
            {
                //validation of count of rows
                //if any row of the matrix its more bigger than 64 char throw execption
                throw new MatrixException("Matrix out of range");
            }

            if (matrix.Any(row => row.Length > 64))
            {
                //validation of count of columns
                //if any row of the matrix its more bigger than 64 char throw execption
                throw new MatrixException("Matrix out of range");
            }
        }

        /// <summary>
        /// get the columns of the matrix in a list of string
        /// </summary>
        /// <returns>a IEnumerable with values of the columns in string format</returns>
        private List<string> GetColumns()
        {
            List<string> columns = new List<string>();

            for (int i = 0; i < this._matrix.First().Length; i++)
            {
                //with linq I select the charcters in the position i of all string in the matrix
                //I can do this because all the string on the matrix are from the same size
                string column = new String(_matrix.Select(row => row.ElementAt(i)).ToArray());

                columns.Add(column);
            }

            return columns;
        }
    }
}
