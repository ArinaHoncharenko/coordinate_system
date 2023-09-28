using System;
using System.Drawing;
using System.Windows.Forms;

namespace oop4_coordinate
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Отримуємо значення координат x та y з текстових полів
                double x = double.Parse(textBox1.Text);
                double y = double.Parse(textBox2.Text);

                string quadrant;
                Color pointColor;

                // Визначаємо четверть системи координат
                if (x > 0 && y > 0)
                {
                    quadrant = "в I";
                    pointColor = Color.Red;
                }
                else if (x < 0 && y > 0)
                {
                    quadrant = "в II";
                    pointColor = Color.Blue;
                }
                else if (x < 0 && y < 0)
                {
                    quadrant = "в III";
                    pointColor = Color.Green;
                }
                else if (x > 0 && y < 0)
                {
                    quadrant = "в IV";
                    pointColor = Color.Orange;
                }
                else if (x == 0 && y != 0)
                {
                    quadrant = "на вісі Y";
                    pointColor = Color.Purple;
                }
                else if (x != 0 && y == 0)
                {
                    quadrant = "на вісі X";
                    pointColor = Color.Magenta;
                }
                else
                {
                    quadrant = "в початку координат";
                    pointColor = Color.Black;
                }

                // Відображення результату
                resultLabel.Text = $"Точка ({x}, {y}) знаходиться {quadrant} чверті.";

                // Візуальне відображення системи координат та точки з визначеним кольором
                DrawCoordinateSystem(x, y, pointColor);
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректні вхідні дані. Будь ласка, введіть числа для координат x та y.");
            }
        }

        private void DrawCoordinateSystem(double x, double y, Color pointColor)
        {
            Graphics g = pictureBox1.CreateGraphics();
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            // Малювання осей координат
            g.DrawLine(Pens.Black, centerX, 0, centerX, pictureBox1.Height); // Вертикальна вісь
            g.DrawLine(Pens.Black, 0, centerY, pictureBox1.Width, centerY); // Горизонтальна вісь
            // Масштабування координат, щоб відобразити точку в системі координат
            int pointX = centerX + (int)(x * 10); // Масштаб 1:10
            int pointY = centerY - (int)(y *10); // Масштаб 1:10

            // Малювання точки з визначеним кольором
            using (Brush brush = new SolidBrush(pointColor))
            {
                g.FillEllipse(brush, pointX - 2, pointY - 2, 5, 5);
            }
        }
    }
}
