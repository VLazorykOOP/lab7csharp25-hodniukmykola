using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab7CSharp
{
    public partial class Form3 : Form
    {
        private Figure[] figures;
        private Random rand = new Random();
        private bool figuresCreated = false; // Чи створено фігури

        private Type selectedFigureType;
        private Color selectedColor = Color.Black;
        private int selectedSize;

        public Form3()
        {
            InitializeComponent();

            // Заповнюємо ComboBox при запуску форми
            comboBox1.Items.AddRange(new string[] { "Коло", "Квадрат", "Трикутник", "Зірка" });
        }

        // Обробник кнопки Створити фігуру
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxSize.Text, out selectedSize) || selectedSize <= 0)
            {
                MessageBox.Show("Будь ласка, введіть дійсне число для розміру фігури.");
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Будь ласка, виберіть тип фігури.");
                return;
            }

            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            // Вибираємо тип фігури
            switch (comboBox1.SelectedIndex)
            {
                case 0: selectedFigureType = typeof(Circle); break;
                case 1: selectedFigureType = typeof(Square); break;
                case 2: selectedFigureType = typeof(Triangle); break;
                case 3: selectedFigureType = typeof(Star); break;
            }

            // Створення масиву фігур
            figures = new Figure[1];
            figures[0] = CreateFigure(selectedFigureType, selectedSize, selectedColor);

            DrawFigures(bmp);
            figuresCreated = true;
        }

        // Метод створення фігури
        private Figure CreateFigure(Type figureType, int size, Color color)
        {
            int x = rand.Next(Math.Max(1, pictureBox2.Width - size));
            int y = rand.Next(Math.Max(1, pictureBox2.Height - size));

            if (figureType == typeof(Circle))
                return new Circle(x, y, size, color);
            else if (figureType == typeof(Square))
                return new Square(x, y, size, color);
            else if (figureType == typeof(Triangle))
                return new Triangle(x, y, size, color);
            else if (figureType == typeof(Star))
                return new Star(x, y, size, color);
            else
                return null;
        }

        // Обробник кнопки Перемістити фігуру
        private void buttonMove_Click(object sender, EventArgs e)
        {
            if (!figuresCreated)
            {
                MessageBox.Show("Спочатку створіть фігуру!");
                return;
            }

            foreach (var fig in figures)
            {
                fig.Move(rand.Next(-10, 10), rand.Next(-10, 10));
            }

            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            DrawFigures(bmp);
        }

        // Малювання фігур
        private void DrawFigures(Bitmap bmp)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                foreach (var fig in figures)
                {
                    fig.Draw(g);
                }
            }

            pictureBox2.Image = bmp;
        }

        // Зміна розміру вручну
        private void textBoxSize_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSize.Text) && !int.TryParse(textBoxSize.Text, out int _))
            {
                MessageBox.Show("Введіть коректне число!");
                textBoxSize.Clear();
            }
        }

        // Вибір кольору
        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog1.Color;
            }
        }

        // Вибір типу фігури
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFigure = comboBox1.SelectedItem.ToString();
            MessageBox.Show($"Вибрано фігуру: {selectedFigure}");
        }
    }
}
