namespace WordFinderLibrary
{
    /// <summary>
    /// this class it's used for process the result of Find methods of WordFinder
    /// this class is internal and shouldnt use outside this library
    /// </summary>
    internal class WordStreamProcessResult
    {
        public string Word { get; set; }

        /// <summary>
        /// ocurrences of the word in the matrix
        /// </summary>
        public int Ocurrences { get; set; }
    }
}
