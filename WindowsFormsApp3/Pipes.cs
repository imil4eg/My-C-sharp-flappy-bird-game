using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace flappy_bird_game
{
    class Pipes
    {
        private Image topPipeSkin;
        private Image botPipeSkin;
        private Rectangle drawTopPipeRectangle;
        private Rectangle drawBotPipeRectangle;
        private int speed = 5;
        private Size screenSize;
        public static int spaceBetweenPipes = 400;
        private int oldLocation;
        private bool haveOuted = true;
        private static Random rnd = new Random();

        /// <summary>
        /// constucter
        /// </summary>
        /// <param name="topPipeSkin">skin of top pipe</param>
        /// <param name="botPipeSkin">skin of bottom pipe</param>
        /// <param name="topPipeLocation">location of top pipe</param>
        /// <param name="botPipeLocation">location of bottom pipe</param>
        /// <param name="screenSize">size of screen</param>
        public Pipes(Image topPipeSkin, Image botPipeSkin, Size screenSize)
        {
            this.topPipeSkin = topPipeSkin;
            this.botPipeSkin = botPipeSkin;
            this.screenSize = screenSize;

            oldLocation = spaceBetweenPipes;

            //create rectangle of pipes
            drawTopPipeRectangle = new Rectangle(spaceBetweenPipes, 0, topPipeSkin.Width, rnd.Next(200, 280));
            drawBotPipeRectangle = new Rectangle(spaceBetweenPipes, screenSize.Height- 200, botPipeSkin.Width, botPipeSkin.Height - 500);
        }

        /// <summary>
        /// get rectangle of toppom pipe
        /// </summary>
        public Rectangle TopPipeRectangle
        {
            get { return drawTopPipeRectangle; }
        }

        /// <summary>
        /// get rectangle of bottom pipe
        /// </summary>
        public Rectangle BottomPipeRectangle
        {
            get { return drawBotPipeRectangle; }
        }

        /// <summary>
        /// check pipes out of screen
        /// </summary>
        public bool HaveOuted
        {
            get { return haveOuted; }
            set { haveOuted = value; }
        }

        /// <summary>
        /// update pipes
        /// </summary>
        public void Update()
        {
            drawTopPipeRectangle.X -= speed;
            drawBotPipeRectangle.X -= speed;

            PipeOutOfScreen();
        }

        /// <summary>
        /// draw pipes
        /// </summary>
        /// <param name="e">paint support</param>
        public void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(topPipeSkin, drawTopPipeRectangle);
            e.Graphics.DrawImage(botPipeSkin, drawBotPipeRectangle);
        }

        /// <summary>
        /// return pipes to first position
        /// </summary>
        public void EndGame()
        {
            drawTopPipeRectangle.X = oldLocation;
            drawBotPipeRectangle.X = oldLocation;
        }

        /// <summary>
        /// check pipes out of screen
        /// </summary>
        private void PipeOutOfScreen()
        {
            if (drawTopPipeRectangle.X + topPipeSkin.Width < 0)
            {
                drawTopPipeRectangle.X = screenSize.Width + topPipeSkin.Width;
                drawBotPipeRectangle.X = screenSize.Width + topPipeSkin.Width;

                haveOuted = true;

                drawTopPipeRectangle.Height = rnd.Next(200, 280);
            }
        }
    }
}
