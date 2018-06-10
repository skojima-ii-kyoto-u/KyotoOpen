using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round3E.Logic
{
    public class Boad
    {
        private line[] lines = new line[60];
        private square[] squares = new square[25];

        public Boad() : this(
                new bool[]
                {
                    true, true, true, true, true,
                    true, false, false, false, false, true,
                    false, false, false, false, false,
                    true, false, false, false, false, true,
                    false, false, true, false, false,
                    true, false, true, true, false, true,
                    false, false, true, false, false,
                    true, false, false, false, false, true,
                    false, false, false, false, false,
                    true, false, false, false, false, true,
                    true, true, true, true, true
                },
                new int[]
                {
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, -1, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0
                })
        {
        }

        public Boad(bool[] lineFlag, int[] squareNum)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new line();

                if ((i >= 0 && i <= 4) ||
                    i % 11 == 5 ||
                    i % 11 == 10 ||
                    (i >= 55 && i <= 59))
                    lines[i].Chosen = true;
            }

            for (int i = 0; i < squares.Length; i++)
            {
                int line1index = (i / 5) * 11 + i % 5;
                squares[i] = new square(
                    lines[line1index],
                    lines[line1index + 5],
                    lines[line1index + 6],
                    lines[line1index + 11]);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 11 < 5)
                {
                    int sq1index = (i / 11 - 1) * 5 + i % 11;
                    lines[i].Sq1 = (i <= 4) ? null : squares[sq1index];
                    lines[i].Sq2 = (i >= 55) ? null : squares[sq1index + 5];
                }
                else
                {
                    int sq1index = (i / 11) * 5 + i % 11 - 6;
                    lines[i].Sq1 = ((i % 11) == 5) ? null : squares[sq1index];
                    lines[i].Sq2 = ((i % 11) == 10) ? null : squares[sq1index + 1];
                }
            }

            for(int i = 0; i < ((lineFlag.Length < lines.Length) ? lineFlag.Length : lines.Length); i++)
            {
                lines[i].Chosen = lineFlag[i];
            }
            for (int i = 0; i < ((squareNum.Length < squares.Length) ? squareNum.Length : squares.Length); i++)
            {
                squares[i].Num = squareNum[i];
            }
        }

        public bool[] LineFlag()
        {
            return lines.Select(l => l.Chosen).ToArray();
        }

        public int[] SquareNum()
        {
            return squares.Select(s => s.Num).ToArray();
        }

        /// <summary>
        /// 正常実行で0、既に選択されている線であれば-1
        /// </summary>
        /// <param name="lineIndex"></param>
        /// <param name="playerIndex"></param>
        /// <returns></returns>
        public int ChooseLine(int lineIndex, int playerIndex)
        {
            var targetl = lines[lineIndex % 60];

            if (targetl.Chosen)
                return -1;

            targetl.Chosen = true;

            if (targetl.Sq1.IsAllLineChosen())
            {
                targetl.Sq1.Num = playerIndex;
            }

            if (targetl.Sq2.IsAllLineChosen())
            {
                targetl.Sq2.Num = playerIndex;
            }

            return 0;
        }

        public Dictionary<int, int> BoadPointTable()
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            List<int> indexList = new List<int>();
            for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].Num != -1 && squares[i].Num != 0)
                    indexList.Add(i);
            }

            while (indexList.Count > 0)
            {
                int target = indexList[0];
                indexList.RemoveAt(0);

                int squareNum = squares[target].Num;
                List<int> adjacentSquare = 
                    (new int[] { target - 5, target - 1, target + 1, target + 5 })
                    .ToList();
                int count = 1;
                while(adjacentSquare.Count > 0)
                {
                    int adj = adjacentSquare[0];
                    adjacentSquare.RemoveAt(0);

                    if(indexList.Contains(adj) && squares[adj].Num == squareNum)
                    {
                        indexList.Remove(adj);
                        count++;
                        adjacentSquare.AddRange(
                            new int[] { adj - 5, adj - 1, adj + 1, adj + 5 });
                    }
                }

                int value;
                if (result.TryGetValue(squareNum, out value))
                {
                    result[squareNum] += count * (count + 1) / 2;
                }
                else
                {
                    result.Add(squareNum, count * (count + 1) / 2);
                }
            }

            return result;
        }

        public Boad Clone()
        {
            return new Boad(this.LineFlag(), this.SquareNum());
        }

        class line
        {
            public bool Chosen = false;
            public square Sq1, Sq2;
        }

        class square
        {
            public int Num = 0;
            private line line1, line2, line3, line4;

            public square(line line1, line line2, line line3, line line4)
            {
                this.line1 = line1;
                this.line2 = line2;
                this.line3 = line3;
                this.line4 = line4;

            }

            public bool IsAllLineChosen()
            {
                return line1.Chosen && line2.Chosen && line3.Chosen && line4.Chosen;
            }
        }
    }
}
