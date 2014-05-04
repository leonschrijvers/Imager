using Imager.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Imager
{
    public partial class MainForm : Form
    {
        private bool _isRunning;

        private Image _sourceImage;
        private GenerationImage _currentResultImage;
        private double _currentResultScore;

        private Color[,] _sourceColors;

        private Fitness _fitness;

        private const int NumberOfChildren = 32;

        private int _generationCount;
        private Thread _t;
        private DateTime _startTime;

        private ColorMode ColorMode
        {
            get
            {
                if (radioButtonColor.Checked)
                {
                    return ColorMode.Color;
                }
                else
                {
                    return ColorMode.Grayscale;
                }
            }
        }

        public MainForm()
        {
            _isRunning = false;
            _generationCount = 0;

            InitializeComponent();

            radioButtonDot.Select();
            radioButtonGrayscale.Select();

            timerUpdateResult.Interval = 1000;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isRunning = !_isRunning;

            if (_isRunning)
            {
                _startTime = DateTime.Now;

                runToolStripMenuItem.Text = "&Stop";

                _t = new Thread(new ThreadStart(Run));
                _t.Start();

                timerUpdateResult.Enabled = true;
            }
            else
            {
                runToolStripMenuItem.Text = "&Run";
            }
        }

        private void Run()
        {
            while (_isRunning)
            {
                RunGeneration();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogSource.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream stream = null;
                    if ((stream = openFileDialogSource.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            InitializeImages(Image.FromStream(stream));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error: could not read file from disk. ({0})", ex.Message));
                }
            }
        }

        private void InitializeImages(Image image)
        {
            _sourceImage = image;

            pictureBoxSource.Image = image;

            int width = image.Width;
            int height = image.Height;

            _sourceColors = new Color[width, height];

            using (Bitmap b = new Bitmap(image))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color c = b.GetPixel(x, y);

                        _sourceColors[x, y] = c;
                    }
                }
            }

            _fitness = new Fitness(_sourceColors);

            _currentResultImage = new GenerationImage(width, height);
            _currentResultScore = double.MaxValue;
        }

        private void RunGeneration()
        {
            GenerationImage bestNewImage = null;
            double bestNewScore = double.MaxValue;

            for (int i = 0; i < 40; i++)
            {
                GenerationImage newImage = _currentResultImage.Clone();
                MutateImage(newImage);

                double score = _fitness.Calculate(newImage);

                if (score < bestNewScore)
                {
                    bestNewImage = newImage.Clone();
                    bestNewScore = score;
                }
            }

            if (bestNewScore < _currentResultScore)
            {
                _currentResultImage = bestNewImage.Clone();
                _currentResultScore = bestNewScore;
            }

            _generationCount++;
        }

        private void MutateImage(GenerationImage image)
        {
            if (radioButtonDot.Checked)
            {
                image.Mutate<Dot>(ColorMode);
            }
            else if (radioButtonLine.Checked)
            {
                image.Mutate<Line>(ColorMode);
            }
            else if (radioButtonPolygon.Checked)
            {
                image.Mutate<Polygon>(ColorMode);
            }
        }

        private void UpdateStatusBar()
        {
            toolStripStatusLabel1.Text = string.Format("End of generation {0}. Score {1}. Runtime {2}", _generationCount, _currentResultScore, DateTime.Now.Subtract(_startTime).ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateStatusBar();

            Bitmap bitmap = _currentResultImage.GetBitmap();

            int width = bitmap.Width;
            int height = bitmap.Height;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                _currentResultImage.Render(g);
            }

            pictureBoxResult.Image = bitmap;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseApplication();
        }

        private void CloseApplication()
        {
            _isRunning = false;
            if (_t != null)
            {
                _t.Abort();
                _t = null;
            }

            Application.Exit();
        }
    }
}
