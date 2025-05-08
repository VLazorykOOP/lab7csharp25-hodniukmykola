using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorComponentExtraction
{
    public partial class Form2 : Form
    {
        private Bitmap originalImage;  // Оригінальне зображення
        private Bitmap processedImage; // Оброблене зображення

        public Form2()
        {
            InitializeComponent();  // Ініціалізація всіх компонентів
        }

        // Відкриття зображення
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "BMP Files|*.bmp|All Files|*.*",
                Title = "Select an image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    originalImage = new Bitmap(openFileDialog.FileName);  // Завантаження зображення
                    pictureBox.Image = originalImage;  // Відображення зображення в PictureBox
                    processedImage = new Bitmap(originalImage);  // Копія для обробки
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }

        // Збереження обробленого зображення
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Перевірка, чи зображення було оброблене
            if (processedImage != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "BMP Files|*.bmp",
                    Title = "Save Image"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        processedImage.Save(saveFileDialog.FileName);  // Збереження обробленого зображення
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving image: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please load and process an image first.");
            }
        }

        // Обробка зображення для виділення червоної складової
        private void radioButtonRed_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRed.Checked)
            {
                ProcessImage("Red");  // Процес виділення червоного компоненту
            }
        }

        // Обробка зображення для виділення зеленої складової
        private void radioButtonGreen_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGreen.Checked)
            {
                ProcessImage("Green");  // Процес виділення зеленого компоненту
            }
        }

        // Обробка зображення для виділення синьої складової
        private void radioButtonBlue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBlue.Checked)
            {
                ProcessImage("Blue");  // Процес виділення синього компоненту
            }
        }

        // Процес виділення колірної складової
        private void ProcessImage(string color)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;  // Якщо зображення не завантажено, припиняємо обробку
            }

            processedImage = new Bitmap(originalImage);  // Створюємо копію для обробки

            // Проходимо по кожному пікселю
            for (int y = 0; y < processedImage.Height; y++)
            {
                for (int x = 0; x < processedImage.Width; x++)
                {
                    Color pixelColor = processedImage.GetPixel(x, y);  // Отримуємо колір пікселя

                    switch (color)
                    {
                        case "Red":
                            processedImage.SetPixel(x, y, Color.FromArgb(pixelColor.R, 0, 0));  // Залишаємо лише червоний компонент
                            break;
                        case "Green":
                            processedImage.SetPixel(x, y, Color.FromArgb(0, pixelColor.G, 0));  // Залишаємо лише зелений компонент
                            break;
                        case "Blue":
                            processedImage.SetPixel(x, y, Color.FromArgb(0, 0, pixelColor.B));  // Залишаємо лише синій компонент
                            break;
                    }
                }
            }

            pictureBox.Image = processedImage;  // Відображаємо оброблене зображення
        }
    }
}
