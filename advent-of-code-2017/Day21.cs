using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/21 ("Fractal Art")
    /// </summary>
    public class Day21
    {
        Dictionary<string, string> _enhancementRules;

        public void Run()
        {
            string[] inputLines = File.ReadAllLines("day21input.txt");
            PopulateEnhancementRules(inputLines);

            Console.WriteLine("Part 1 solution: " + Part1());
        }

        private void PopulateEnhancementRules(string[] inputLines)
        {
            _enhancementRules = new Dictionary<string, string>();
            foreach (string inputLine in inputLines)
            {
                string[] entities = inputLine.Split(new char[] { '/', ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);
                string pattern;
                string enhanced;
                List<string> permutations;
                if (entities.Length == 5)
                {
                    pattern = entities[0] + entities[1];
                    enhanced = entities[2] + entities[3] + entities[4];
                    permutations = GetPermutations2(pattern);
                }
                else
                {
                    pattern = entities[0] + entities[1] + entities[2];
                    enhanced = entities[3] + entities[4] + entities[5] + entities[6];
                    permutations = GetPermutations3(pattern);
                }

                foreach (string permutation in permutations)
                {
                    _enhancementRules[permutation] = enhanced;
                }
            }
        }

        private List<string> GetPermutations2(string pattern)
        {
            List<string> permutations = new List<string>();

            AddRotations2(pattern, permutations);

            // Mirror:
            // 01 => 10
            // 23    32
            string mirrored = "" + pattern[1] + pattern[0] + pattern[3] + pattern[2];

            AddRotations2(mirrored, permutations);

            return permutations;
        }

        private void AddRotations2(string pattern, List<string> permutations)
        {
            string rotated = pattern;
            for (int i = 0; i < 4; i++)
            {
                permutations.Add(rotated);

                // Rotate:
                // 01 => 20
                // 23    31
                rotated = "" + rotated[2] + rotated[0] + rotated[3] + rotated[1];
            }
        }

        private List<string> GetPermutations3(string pattern)
        {
            List<string> permutations = new List<string>();

            AddRotations3(pattern, permutations);

            // Mirror:
            // 012    210
            // 345 => 543
            // 678    876
            string mirrored = "" + pattern[2] + pattern[1] + pattern[0] + pattern[5] + pattern[4] + pattern[3] + pattern[8] + pattern[7] + pattern[6];
            AddRotations3(mirrored, permutations);

            // Mirror vertical:
            // 012    678
            // 345 => 345
            // 678    012
            mirrored = "" + pattern[6] + pattern[7] + pattern[8] + pattern[3] + pattern[4] + pattern[5] + pattern[0] + pattern[1] + pattern[2];
            AddRotations3(mirrored, permutations);

            return permutations;
        }

        private void AddRotations3(string pattern, List<string> permutations)
        {
            string rotated = pattern;
            for (int i = 0; i < 4; i++)
            {
                permutations.Add(rotated);

                // Rotate:
                // 012    630
                // 345 => 741
                // 678    852
                rotated = "" + rotated[6] + rotated[3] + rotated[0] + rotated[7] + rotated[4] + rotated[1] + rotated[8] + rotated[5] + rotated[2];
            }
        }

        private char[,] GetInitialArt()
        {
            char[,] art = new char[3, 3];
            art[0, 0] = '.';
            art[0, 1] = '#';
            art[0, 2] = '.';
            art[1, 0] = '.';
            art[1, 1] = '.';
            art[1, 2] = '#';
            art[2, 0] = '#';
            art[2, 1] = '#';
            art[2, 2] = '#';
            return art;
        }

        private int Part1()
        {
            char[,] art = GetInitialArt();
            
            for (int i = 0; i < 5; i++)
            {
                art = Enhance(art);                    
            }

            return GetOnPixelCount(art);
        }

        private int GetOnPixelCount(char[,] art)
        {
            int onPixels = 0;
            foreach (char c in art)
            {
                if (c == '#')
                {
                    onPixels += 1;
                }
            }
            return onPixels;
        }

        private char[,] Enhance(char[,] art)
        {
            int pieceSize;
            if (Size(art) % 2 == 0)
            {
                pieceSize = 2;
            }
            else
            {
                pieceSize = 3;
            }

            List<char[,]> artPieces = Divide(art, pieceSize);
            List<char[,]> enhancedPieces = EnhancePieces(artPieces);
            char[,] enhancedArt = Join(enhancedPieces);
            return enhancedArt;
        }

        private int Size(char[,] art)
        {
            // It'll always be a square
            return art.GetLength(0);
        }

        private List<char[,]> Divide(char[,] art, int dividedArtSize)
        {
            List<char[,]> dividedArt = new List<char[,]>();
            for (int y = 0; y < Size(art); y += dividedArtSize)
            {
                for (int x = 0; x < Size(art); x += dividedArtSize)
                {
                    char[,] artPiece = new char[dividedArtSize,dividedArtSize];
                    for (int j = 0; j < dividedArtSize; j++)
                    {
                        for (int i = 0; i < dividedArtSize; i++)
                        {
                            artPiece[j, i] = art[y + j, x + i];
                        }
                    }
                    dividedArt.Add(artPiece);
                }
            }
            return dividedArt;
        }

        private List<char[,]> EnhancePieces(List<char[,]> artPieces)
        {
            List<char[,]> enhancedPieces = new List<char[,]>();
            foreach (char[,] piece in artPieces)
            {
                enhancedPieces.Add(EnhancePiece(piece));
            }
            return enhancedPieces;
        }

        private char[,] EnhancePiece(char[,] artPiece)
        {
            string unrolledArtPiece = Unroll(artPiece);
            string enhancedUnrolledArtPiece = _enhancementRules[unrolledArtPiece];
            char[,] enhancedArtPiece = Roll(enhancedUnrolledArtPiece);
            return enhancedArtPiece;
        }

        private string Unroll(char[,] artPiece)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int y = 0; y < Size(artPiece); y++)
            {
                for (int x = 0; x < Size(artPiece); x++)
                {
                    stringBuilder.Append(artPiece[y, x]);
                }
            }
            return stringBuilder.ToString();
        }

        private char[,] Roll(string unrolledArtPiece)
        {
            int size = (int)Math.Sqrt(unrolledArtPiece.Length);
            char[,] artPiece = new char[size, size];
            int i = 0;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    artPiece[y, x] = unrolledArtPiece[i];
                    i++;
                }
            }
            return artPiece;
        }

        private char[,] Join(List<char[,]> artPieces)
        {
            int artPiecesSqrt = (int)Math.Sqrt(artPieces.Count);

            int pieceSize = Size(artPieces[0]);

            int size = Size(artPieces[0]) * artPiecesSqrt;
            char[,] art = new char[size, size];

            for (int j = 0; j < artPiecesSqrt; j++)
            {
                for (int i = 0; i < artPiecesSqrt; i++)
                {
                    char[,] artPiece = artPieces[j * artPiecesSqrt + i];
                    for (int py = 0; py < pieceSize; py++)
                    {
                        for (int px = 0; px < pieceSize; px++)
                        {
                            int x = (i * pieceSize) + px;
                            int y = (j * pieceSize) + py;
                            art[y, x] = artPiece[py, px];
                        }
                    }
                }
            }
            return art;
        }
    }
}
