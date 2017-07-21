using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;


namespace flappy_bird_game
{
    class Bird
    {
        private Image defaultSkin;
        private Rectangle drawRectangle;
        private int gravity = 3;

        /// <summary>
        /// default constructer
        /// </summary>
        public Bird() : this(Image.FromFile("bird.png"), new Point(0, 0)) { }

        /// <summary>
        /// constructer
        /// </summary>
        /// <param name="defaultSkin">default skin of bird</param>
        /// <param name="startPosition">start position of bird</param>
        /// <param name="flySkin">fly skin of bird</param>
        public Bird(Image defaultSkin, Point startPosition)
        {
            this.defaultSkin = defaultSkin;
            drawRectangle = new Rectangle(startPosition.X - defaultSkin.Width / 2, startPosition.Y - defaultSkin.Height / 2, defaultSkin.Width, defaultSkin.Height);

        }

        /// <summary>
        /// get rectangle of bird
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// update method
        /// </summary>
        public void Update()
        {
            drawRectangle.Y += gravity;
        }

        public void SpaceKeyPressed()
        {
            drawRectangle.Y -= 50;
        }

        /// <summary>
        /// draw method
        /// </summary>
        public void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(defaultSkin, drawRectangle);
        }
    }
}
