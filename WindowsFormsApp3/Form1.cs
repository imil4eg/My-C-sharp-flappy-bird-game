using flappy_bird_game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private List<Pipes> pipes = new List<Pipes>();
        private Bird bird;
        private int numberOfPipes = 3;
        private Button startGame;
        private Label score;
        private int scoreNum = 0;

        public Form1()
        {
            InitializeComponent();

            //set resolution of window and set background image
            this.Size = new Size(800, 600);
            this.BackgroundImage = Image.FromFile(@"C:\Users\zis\Documents\Visual Studio 2017\Projects\WindowsFormsApp3\WindowsFormsApp3\Resources\bg.png");
            this.Text = "Flappy Bird";

            //create score 
            score = new Label();
            score.Text = "";
            score.Size = new Size(50, 50);
            score.Location = new Point(50, 50);
            score.BackColor = Color.Transparent;
            Controls.Add(score);

            //create start game button
            startGame = new Button();
            startGame.Text = "Start";
            startGame.Size = new Size(100, 50);
            startGame.Location = new Point(this.Size.Width / 2 - startGame.Width / 2, this.Size.Height / 2 - startGame.Height / 2);
            startGame.MouseDown += StartGame_MouseDown;
            Controls.Add(startGame);

            //create bird
            bird = new Bird(Image.FromFile(@"C:\Users\zis\Documents\Visual Studio 2017\Projects\WindowsFormsApp3\WindowsFormsApp3\Resources\bird.png"), 
                new Point(this.Size.Width - 700, this.Size.Height - 300));

            //create pipes
            for(int i = 0;i < numberOfPipes; i++)
            {
                pipes.Add(new Pipes(Image.FromFile(@"C:\Users\zis\Documents\Visual Studio 2017\Projects\WindowsFormsApp3\WindowsFormsApp3\Resources\pipeTop.png"), 
                    Image.FromFile(@"C:\Users\zis\Documents\Visual Studio 2017\Projects\WindowsFormsApp3\WindowsFormsApp3\Resources\pipeBottom.png"),
                    this.Size));
                Pipes.spaceBetweenPipes += 350;
            }

            //add double buffering
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(Form1_Paint);

            gameTimer.Enabled = false;
        }

        private void StartGame_MouseDown(object sender, MouseEventArgs e)
        {
            gameTimer.Enabled = true;
            startGame.Visible = false;
            score.Text = "0";
            scoreNum = 0;

            foreach (Pipes pipe in pipes)
            {
                pipe.EndGame();
            }
            Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// update method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            bird.Update();

            foreach(Pipes pipe in pipes)
            {
                pipe.Update();
            }

            CheckForBound();
            AddScore();

            Invalidate();
        }

        /// <summary>
        /// paint method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            bird.Draw(e);

            foreach(Pipes pipe in pipes)
            {
                pipe.Draw(e);
            }

            
        }

        /// <summary>
        /// check have bird intersect with pipe
        /// </summary>
        private void CheckForBound()
        {
            foreach(Pipes pipe in pipes)
            {
                if(pipe.TopPipeRectangle.IntersectsWith(bird.CollisionRectangle) || pipe.BottomPipeRectangle.IntersectsWith(bird.CollisionRectangle))
                {
                    EndGame();
                }
            }
        }

        /// <summary>
        /// game position end
        /// </summary>
        private void EndGame()
        {
            gameTimer.Stop();
            startGame.Visible = true;
        }

        /// <summary>
        /// check for press buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Space)
            {
                bird.SpaceKeyPressed();
            }
        }

        /// <summary>
        /// check for add score
        /// </summary>
        private void AddScore()
        {
            foreach(Pipes pipe in pipes)
            {
                if(pipe.TopPipeRectangle.X <= bird.CollisionRectangle.X && pipe.HaveOuted)
                {
                    scoreNum++;
                    score.Text = Convert.ToString(scoreNum);

                    pipe.HaveOuted = false;
                }
            }
        }

    }
}
