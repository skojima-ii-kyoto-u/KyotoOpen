using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Round3E
{
    public class ShowWindowBoadPanel : WrapPanel
    {
        private const int padding = 25;
        private const int boadSize = 450;
        private const int strokeThickness = 4;

        private Line[] line = new Line[60];
        private Rectangle[] rectangle = new Rectangle[25];

        private bool isOpacityChanged = false;

        private int selecting = 0;
        public int Selecting
        {
            get
            {
                return selecting;
            }
        }

        private Brush lineBrushDefault = new SolidColorBrush(Colors.Gray);
        private Brush lineBrushChosen = new SolidColorBrush(Colors.Black);
        private Brush lineBrushSelecting = new SolidColorBrush(Colors.Orange);

        private Brush squareBrushDefault = new SolidColorBrush(Colors.LightGray);
        private Brush squareBrush1 = new SolidColorBrush(Colors.DarkRed);
        private Brush squareBrush2 = new SolidColorBrush(Colors.OrangeRed);
        private Brush squareBrush3 = new SolidColorBrush(Colors.Yellow);
        private Brush squareBrush4 = new SolidColorBrush(Colors.Green);
        private Brush squareBrush5 = new SolidColorBrush(Colors.Blue);
        private Brush squareBrush6 = new SolidColorBrush(Colors.Violet);

        public ShowWindowBoadPanel(Logic.Boad boad)
        {
            Width = Height = boadSize;
            Background = new SolidColorBrush(Colors.White);

            Brush lineBrush = new SolidColorBrush(Colors.LightGray);
            Brush smallRectBrush = new SolidColorBrush(Colors.Black);

            int lineLength = (boadSize - padding * 2 - strokeThickness * 6) / 5;

            //縦線のラベル
            {
                TextBlock tb = new TextBlock();
                tb.Height = padding;
                tb.Width = padding;
                tb.Text = "";
                tb.FontSize = 20;

                this.Children.Add(tb);
            }
            for(int i = 0; i < 6; i++)
            {
                TextBlock tb = new TextBlock();
                tb.Height = padding;
                tb.Width = (i < 5) ? lineLength + strokeThickness : padding;
                tb.Text =
                    (i == 0) ? "A" :
                    (i == 1) ? "B" :
                    (i == 2) ? "C" :
                    (i == 3) ? "D" :
                    (i == 4) ? "E" :
                    (i == 5) ? "F" :
                    "";
                tb.FontSize = 20;

                this.Children.Add(tb);
            }
            for(int i = 0; i < 5; i++)
            {
                //横線のラベル
                {
                    TextBlock tb = new TextBlock();
                    tb.Height = lineLength + strokeThickness;
                    tb.Width = padding;
                    tb.Text = (i + 1).ToString();
                    tb.FontSize = 20;

                    Children.Add(tb);
                }

                WrapPanel wp = new WrapPanel();
                wp.Width = boadSize - padding * 2;
                wp.Height = lineLength + strokeThickness;
                //横線
                for (int j = 0; j < 5; j++)
                {
                    var nowrect = new Rectangle();
                    nowrect.Width = nowrect.Height = strokeThickness;
                    nowrect.Fill = smallRectBrush;
                    wp.Children.Add(nowrect);

                    var nowline = line[i * 11 + j] = new Line();
                    nowline.X1 = 0;
                    nowline.Y1 = strokeThickness * 0.5;
                    nowline.X2 = lineLength;
                    nowline.Y2 = strokeThickness * 0.5;
                    nowline.Stroke = lineBrush;
                    nowline.StrokeThickness = strokeThickness;
                    wp.Children.Add(nowline);
                }
                {
                    var nowrect = new Rectangle();
                    nowrect.Width = nowrect.Height = strokeThickness;
                    nowrect.Fill = smallRectBrush;
                    wp.Children.Add(nowrect);
                }
                //縦線と正方形を交互に
                for (int j = 0; j < 5; j++)
                {
                    var nowline = line[i * 11 + j + 5] = new Line();
                    nowline.X1 = strokeThickness * 0.5;
                    nowline.Y1 = 0;
                    nowline.X2 = strokeThickness * 0.5;
                    nowline.Y2 = lineLength;
                    nowline.Stroke = lineBrush;
                    nowline.StrokeThickness = strokeThickness;
                    wp.Children.Add(nowline);

                    var nowrect = rectangle[i * 5 + j] = new Rectangle();
                    nowrect.Width = lineLength;
                    nowrect.Height = lineLength;
                    wp.Children.Add(nowrect);
                }
                {
                    var nowline = line[i * 11 + 10] = new Line();
                    nowline.X1 = strokeThickness * 0.5;
                    nowline.Y1 = 0;
                    nowline.X2 = strokeThickness * 0.5;
                    nowline.Y2 = lineLength;
                    nowline.Stroke = lineBrush;
                    nowline.StrokeThickness = strokeThickness;
                    wp.Children.Add(nowline);
                }
                this.Children.Add(wp);

                //右パディング分
                {
                    TextBlock tb = new TextBlock();
                    tb.Height = lineLength + strokeThickness;
                    tb.Width = padding;
                    tb.Text = "";
                    tb.FontSize = 20;

                    this.Children.Add(tb);
                }
            }
            //横線のラベル
            {
                TextBlock tb = new TextBlock();
                tb.Height = padding;
                tb.Width = padding;
                tb.Text = "6";
                tb.FontSize = 20;
                
                this.Children.Add(tb);
            }

            {
                WrapPanel wp = new WrapPanel();
                wp.Width = boadSize - padding * 2;
                wp.Height = padding;

                //一番下の横線
                for (int j = 0; j < 5; j++)
                {
                    var nowrect = new Rectangle();
                    nowrect.Width = nowrect.Height = strokeThickness;
                    nowrect.Fill = smallRectBrush;
                    wp.Children.Add(nowrect);

                    var nowline = line[55 + j] = new Line();
                    nowline.X1 = 0;
                    nowline.Y1 = strokeThickness * 0.5;
                    nowline.X2 = lineLength;
                    nowline.Y2 = strokeThickness * 0.5;
                    nowline.Stroke = lineBrush;
                    nowline.StrokeThickness = strokeThickness;
                    wp.Children.Add(nowline);
                }
                {
                    var nowrect = new Rectangle();
                    nowrect.Width = nowrect.Height = strokeThickness;
                    nowrect.Fill = smallRectBrush;
                    wp.Children.Add(nowrect);
                }

                this.Children.Add(wp);
            }

            LineColorChange(boad.LineFlag());
            SquareColorChange(boad.SquareNum());
        }

        public void SwitchOpacity(int size)
        {
            if (isOpacityChanged)
            {
                isOpacityChanged = false;
                Opacity = 1.0;
            }
            else
            {
                //RenderTransform = new ScaleTransform((double)size / boadSize, (double)size / boadSize);
                isOpacityChanged = true;
                Opacity = 0.5;
            }
        }

        public void ColorChange(Logic.Boad boad)
        {
            LineColorChange(boad.LineFlag());
            SquareColorChange(boad.SquareNum());
        }

        public void LineColorChange(bool[] lineFlag)
        {
            for(int i = 0; i < ((lineFlag.Length < line.Length) ? lineFlag.Length : line.Length); i++)
            {
                if (i == selecting && lineFlag[i])
                    selecting++;

                line[i].Stroke = 
                    lineFlag[i] ? lineBrushChosen : 
                    (i == selecting) ? lineBrushSelecting:
                    lineBrushDefault;
            }

            //全ての色を塗り終えたあと、選択中の線が0に戻っている場合に選択し直す
            if(selecting == 60)
            {
                while (lineFlag[selecting = (selecting + 1) % 60])
                {
                }
                line[selecting].Stroke = lineBrushSelecting;
            }
        }

        public void SquareColorChange(int[] squareNum)
        {
            for (int i = 0; i < ((squareNum.Length < rectangle.Length) ? squareNum.Length : rectangle.Length); i++)
            {
                switch (squareNum[i])
                {
                    case -1:
                        rectangle[i].Fill = new SolidColorBrush(Colors.Black);
                        break;
                    case 0:
                        rectangle[i].Fill = squareBrushDefault;
                        break;
                    case 1:
                        rectangle[i].Fill = squareBrush1;
                        break;
                    case 2:
                        rectangle[i].Fill = squareBrush2;
                        break;
                    case 3:
                        rectangle[i].Fill = squareBrush3;
                        break;
                    case 4:
                        rectangle[i].Fill = squareBrush4;
                        break;
                    case 5:
                        rectangle[i].Fill = squareBrush5;
                        break;
                    case 6:
                        rectangle[i].Fill = squareBrush6;
                        break;
                    default:
                        break;
                }
            }
        }

        public string SelectLineForward(Logic.Boad boad)
        {
            var lineFlag = boad.LineFlag();
            line[selecting].Stroke = lineBrushDefault;
            while (lineFlag[selecting = (selecting + 1) % 60])
            {
            }
            line[selecting].Stroke = lineBrushSelecting;

            return lineName(selecting);
        }

        public string SelectLineBack(Logic.Boad boad)
        {
            var lineFlag = boad.LineFlag();
            line[selecting].Stroke = lineBrushDefault;
            while (lineFlag[selecting = (selecting + 59) % 60])
            {
            }
            line[selecting].Stroke = lineBrushSelecting;

            return lineName(selecting);
        }

        private static string[] fileLetter = new string[] { "A", "B", "C", "D", "E", "F" };
        private static string[] rankLetter = new string[] { "1", "2", "3", "4", "5", "6" };
        private string lineName(int selecting)
        {
            string point1 = "", point2 = "";
            int rank = selecting / 11;
            int file = selecting % 11;

            point1 = ((file == 10) ? "F" : fileLetter[file % 5]) + rankLetter[rank];
            point2 =
                ((file <= 4)) ? ((fileLetter[file % 5 + 1]) + rankLetter[rank]) :
                ((fileLetter[file % 5]) + rankLetter[rank + 1]);

            return point1 + point2;
        }
    }
}
