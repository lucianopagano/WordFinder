using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinderLibrary;

namespace WordFinderTests
{
    public class WordFinderTests
    {
        [Fact]
        public void MatrixNullTest()
        {
            MatrixException exception = Assert.Throws<MatrixException>(() => new WordFinder(null));
            Assert.Equal("Matrix can't be null", exception.Message);
        }

        [Fact]
        public void MatrixMaxColumnLengthTest()
        {
            //matrix with column of 65 chars
            List<string> testArray = new List<string>()
            {
                "FjjapbpgodIjgyRmXKRreNISYjgQMwmjxYFYWjKXtFaAjnKroNxvSyZYKEMMjgIKz",
            };

            MatrixException exception = Assert.Throws<MatrixException>(() => new WordFinder(testArray));
            Assert.Equal("Matrix out of range", exception.Message);
        }

        [Fact]
        public void MatrixMaxRowLengthTest()
        {
            string row = "FjjapbpgodI";

            //matrix with row of string 65 chars
            List<string> testArray = new List<string>();

            //add 65 rows
            for (int i = 1; i <= 65; i++)
            {
                testArray.Add(row);
            }


            MatrixException exception = Assert.Throws<MatrixException>(() => new WordFinder(testArray));
            Assert.Equal("Matrix out of range", exception.Message);
        }

        [Fact]
        public void MatrixColumsSizeTest()
        {
            //this matrix have not have the correct numbers of colums
            List<string> testArray = new List<string>()
            {
                "testColumOne",
                "testColumTwoAndMore",
            };

            MatrixException exception = Assert.Throws<MatrixException>(() => new WordFinder(testArray));
            Assert.Equal("Invalid rows size", exception.Message);
        }

        /// <summary>
        /// this test validate the case when no word its find it
        /// </summary>
        [Fact]
        public void FindNoWordsTest()
        {
            List<string> matrix = new List<string>();

            string row1 = "aaaaadbcec";
            string row2 = "aaaaafgwio";
            string row3 = "asdddchill";
            string row4 = "jmpsdpqnsd";
            string row5 = "uvdxyjjsjs";
            string row6 = "qhelloooos";
            string row7 = "sssssbyedd";
            string row8 = "ssssusyedd";
            string row9 = "ssssnuyedd";

            matrix.Add(row1);
            matrix.Add(row2);
            matrix.Add(row3);
            matrix.Add(row4);
            matrix.Add(row5);
            matrix.Add(row6);
            matrix.Add(row7);
            matrix.Add(row8);
            matrix.Add(row9);

            var wf = new WordFinder(matrix);

            List<string> streamWord = new List<string>() { "cat", "dog" };

            IEnumerable<string> result = wf.Find(streamWord);

            Assert.NotNull(result);

            Assert.Empty(result);

        }

        /// <summary>
        /// this test validate method find with words
        /// </summary>
        [Fact]
        public void FindWordsTest()
        {
            List<string> matrix = new List<string>();

            //1 sun appears 6 times 
            //2 moon appears 5 times
            //3 hello apears 2 time
            //4 cold appears 1 times
            //5 chill appears 1 times
            //6 wind appears 1 times
            //7 cat apears 1 time
            //8 dog apears 1 time
            //9 red appears 0 time
            //10 blue appears 1 time
            //11 word appears 1 time
            //12 CHARACTER appears 1 time

            string row1 = "saaaadbcec";//sun in the first colum
            string row2 = "uaaaafgwio";
            string row3 = "nsdddchill";
            string row4 = "jmpsdpqnsd";
            string row5 = "sundxyjddj";
            string row6 = "qhelloooos";
            string row7 = "sssssbyedd";
            string row8 = "ssssusyedd";
            string row9 = "moonnuyedd";
            string row10 = "catnnuyedd";
            string row11 = "dognnuyedd";
            string row12 = "hellouyedd";
            string row13 = "blueouyedd";
            string row14 = "aaaaouword";
            string row15 = "CHARACTERF";
            string row16 = "aareduword";


            matrix.Add(row1);
            matrix.Add(row2);
            matrix.Add(row3);
            matrix.Add(row4);
            matrix.Add(row5);
            matrix.Add(row5);
            matrix.Add(row5);
            matrix.Add(row5);
            matrix.Add(row6);
            matrix.Add(row7);
            matrix.Add(row8);
            matrix.Add(row9);
            matrix.Add(row9);
            matrix.Add(row9);
            matrix.Add(row9);
            matrix.Add(row9);
            matrix.Add(row10);
            matrix.Add(row11);
            matrix.Add(row12);
            matrix.Add(row13);
            matrix.Add(row14);
            matrix.Add(row15);
            matrix.Add(row16);

            //create word stream
            List<string> wordstream = new List<string>() { "sun", "moon", "hello", "cold", "chill", "cat", "dog", "red", "blue", "word", "character" };


            WordFinder wf = new WordFinder(matrix);

            IEnumerable<string> result = wf.Find(wordstream);

            Assert.NotNull(result);

            Assert.NotEmpty(result);

            //the first elemt should be the word "sun"
            Assert.Equal("sun", result.First());

            Assert.Equal(10, result.Count());
        }
    }
}
