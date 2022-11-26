using System;
using System.Collections.Generic;

namespace Parson
{
    internal class ParsonElement //: BaseExercise
    {
        public ParsonElement(string code,
            int index)
        {
            Snippet = code;
            Index = index;
        }

        public string Snippet { get; }

        public int Index { get; }
    }

    internal class Parson //:BaseExercise
    {
        private readonly string originalCode;
        private readonly List<ParsonElement> puzzle;

        public Parson(string input)
        {
            originalCode = input;
            puzzle = new List<ParsonElement>();
            Blocksize();
        }

        private int CountLines()
        {
            var linecount = 0;
            for (var i = 0; i < originalCode.Length; i++)
                if (originalCode[i] == '\n')
                    linecount++;
            return linecount;
        }

        private void Blocksize()
        {
            var lines = CountLines();
            int blocksize;
            if (lines <= 4)
                blocksize = 1;
            else if (lines <= 10)
                blocksize = 2;
            else if (lines <= 21)
                blocksize = 3;
            else
                blocksize = 5;

            Split(blocksize);
        }

        private void Split(int blocksize)
        {
            var counter = 0;
            var currentBlock = 0;
            var startindex = 0;
            for (var i = 0; i < originalCode.Length; i++)
            {
                if (originalCode[i] == '\n')
                {
                    counter++;
                    if (counter == blocksize)
                    {
                        var temp = new ParsonElement(originalCode.Substring(startindex, i - startindex), currentBlock);
                        puzzle.Add(temp);
                        counter = 0;
                        startindex = i + 1;
                        currentBlock++;
                    }
                }

                if (i == originalCode.Length - 1)
                {
                    var temp = new ParsonElement(originalCode.Substring(startindex), currentBlock);
                    puzzle.Add(temp);
                }
            }
        }

        public void Shuffle()
        {
            var rng = new Random();
            var n = puzzle.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var temp = puzzle[k];
                puzzle[k] = puzzle[n];
                puzzle[n] = temp;
            }
        }

        public int FinalScore(int maxScore)
        {
            var correctBlocks = 0;
            for (var i = 0; i < puzzle.Count; i++)
                if (i == puzzle[i].Index)
                    correctBlocks++;
            var achievedPoints = (float)correctBlocks / puzzle.Count;
            return (int)(maxScore * achievedPoints + 0.5f);
        }

        public void ShowContent()
        {
            for (var i = 0; i < puzzle.Count; i++) Console.WriteLine(puzzle[i].Index + " " + puzzle[i].Snippet);
        }

        private static void Main(string[] args)
        {
            var input = "line1\nline2\nline3 \nline4\nline5 \nline6 \nline7";
            var test = new Parson(input);
            test.ShowContent();
            test.Shuffle();
            Console.WriteLine();
            test.ShowContent();
            Console.WriteLine("\n Score:" + test.FinalScore(10));
            test.Shuffle();
            Console.WriteLine();
            test.ShowContent();
            Console.Read();
        }
    }
}