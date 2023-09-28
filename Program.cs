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
            // Ініціалізуємо Windows Forms додаток
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Запускаємо форму Form1
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

                // Відображення результату у resultLabel
                resultLabel.Text = $"Точка ({x}; {y}) знаходиться {quadrant} чверті.";

                // Візуальне відображення системи координат та точки з визначеним кольором
                DrawCoordinateSystem(x, y, pointColor);
            }
            catch (FormatException)
            {
                // Обробка помилки при некоректних вхідних даних
                resultLabel.Text = "Некоректні вхідні дані. Будь ласка, введіть числа для координат x та y.";
            }
        }

        private void DrawCoordinateSystem(double x, double y, Color pointColor)
        {
            // Створюємо графічний об'єкт для малювання на PictureBox "pictureBox1"
            Graphics g = pictureBox1.CreateGraphics();
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            // Малюємо осі координат
            g.DrawLine(Pens.Black, centerX, 0, centerX, pictureBox1.Height); // Вертикальна вісь
            g.DrawLine(Pens.Black, 0, centerY, pictureBox1.Width, centerY); // Горизонтальна вісь

            // Масштабуємо координати, щоб відобразити точку в системі координат
            int pointX = centerX + (int)(x * 10); // Масштаб 1:10
            int pointY = centerY - (int)(y * 10); // Масштаб 1:10

            // Малюємо точку з визначеним кольором
            using (Brush brush = new SolidBrush(pointColor))
            {
                g.FillEllipse(brush, pointX - 2, pointY - 2, 5, 5);
            }
        }
    }
}
