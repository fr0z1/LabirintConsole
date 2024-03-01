using System.Collections.Generic;
using System;

namespace ConsoleRender
{
    class Program
    {
        static int width = 120;
        static int heigh = 29;
        static int widthLabirint = 20;
        static int heighLabirint = 11;
        static bool[,] Map = new bool[width, heigh];

        static Random rand = new Random();

        static int[] point = new int[2];

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                if (Menu() == 1)
                {
                    break;
                }
                GenerateMap();
                Console.ReadLine();
                Map = new bool[width, heigh];
            }
        }
        
        static int Menu()
        {
            Console.WriteLine("Выберете уровень сложности(1-5, 0 - выход): ");
            string i = Console.ReadLine();
            if (i == "1")
            {
                widthLabirint = 16;
                heighLabirint = 9;
            }
            else if (i == "2")
            {
                widthLabirint = 42;
                heighLabirint = 17;
            }
            else if (i == "3")
            {
                widthLabirint = 66;
                heighLabirint = 19;
            }
            else if (i == "4")
            {
                widthLabirint = 90;
                heighLabirint = 23;
            }
            else if (i == "5")
            {
                widthLabirint = 120;
                heighLabirint = 29;
            }
            else if (i == "0")
            {
                return 1;
            }
            else
            {
                Console.Clear();
                Menu();
            }
            Console.Clear();
            return 0;
        }

        static void MapDraw()
        {
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x > widthLabirint-2 || y > heighLabirint-1)
                    {
                        Console.Write(' ');
                    }
                    else if(x == point[0] && y == point[1] && x > 0 && y > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('■');
                        Console.ResetColor();
                    }
                    else if(x == 1 && y == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('■');
                        Console.ResetColor();
                    }
                    else if (Map[x, y] == false)
                    {
                        Console.Write('█');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }
        }

        static void GenerateMap()
        {
            GenerateBorder();
            GenerateLabirint();
            MapDraw();
        }

        static void GenerateBorder()
        {
            for (int y = 0; y < heighLabirint; y++)
            {
                for (int x = 0; x < widthLabirint; x++)
                {
                    if(y == 0 || y == heighLabirint-1 || x == 0 || x == widthLabirint-1)
                    {
                        Map[x,y] = false;
                    }
                }
            }
        }

        static void GenerateLabirint()
        {
            Stack<int> cellsX = new Stack<int>();
            Stack<int> cellsY = new Stack<int>();

            int maxSteps = -1;

            cellsX.Push(1);
            cellsY.Push(1);

            while (cellsX.Count > 0)
            {
                List<int> currentCellsX = new List<int>();
                List<int> currentCellsY = new List<int>();

                if (cellsX.Peek() < widthLabirint - 3)
                {
                    if (Map[cellsX.Peek() + 2, cellsY.Peek()] == false)
                    {
                        currentCellsX.Add(cellsX.Peek() + 2);
                        currentCellsY.Add(cellsY.Peek());
                    }
                }

                if (cellsX.Peek() > 2)
                {
                    if (Map[cellsX.Peek() - 2, cellsY.Peek()] == false)
                    {
                        currentCellsX.Add(cellsX.Peek() - 2);
                        currentCellsY.Add(cellsY.Peek());
                    }
                }

                if (cellsY.Peek() < heighLabirint - 3)
                {
                    if (Map[cellsX.Peek(), cellsY.Peek() + 2] == false)
                    {
                        currentCellsX.Add(cellsX.Peek());
                        currentCellsY.Add(cellsY.Peek() + 2);
                    }
                }

                if (cellsY.Peek() > 2)
                {
                    if (Map[cellsX.Peek(), cellsY.Peek() - 2] == false)
                    {
                        currentCellsX.Add(cellsX.Peek());
                        currentCellsY.Add(cellsY.Peek() - 2);
                    }
                }

                if (currentCellsX.Count != 0)
                {
                    int cellNumber = rand.Next(0, currentCellsX.Count);

                    Map[currentCellsX[cellNumber], currentCellsY[cellNumber]] = true;
                    Map[(currentCellsX[cellNumber] + cellsX.Peek()) / 2,
                        (currentCellsY[cellNumber] + cellsY.Peek()) / 2] = true;

                    cellsX.Push(currentCellsX[cellNumber]);
                    cellsY.Push(currentCellsY[cellNumber]);
                }
                else
                {
                    if (cellsX.Count >= maxSteps && cellsX.Peek() != 1)
                    {
                        point[0] = cellsX.Peek();
                        point[1] = cellsY.Peek();
                        maxSteps = cellsX.Count;
                    }
                    cellsX.Pop();
                    cellsY.Pop();
                }
            }
        }
    }
}
