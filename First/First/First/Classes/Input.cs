using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace First
{
    class Input
    {
        private int left_axis;

        public int Left_axis
        {
            get { return left_axis; }
        }

        private int right_axis;

        public int Right_axis
        {
            get { return right_axis; }
        }

        public void Update(){
            KeyboardState input_Keyboard = Keyboard.GetState();

            //Movimentação dos Players
            if (input_Keyboard.IsKeyDown(Keys.W))
            {
                this.left_axis = -1;
            }
            else if (input_Keyboard.IsKeyDown(Keys.S))
            {
                this.left_axis = 1;
            }
            else
            {
                this.left_axis = 0;
            }

            if (input_Keyboard.IsKeyDown(Keys.Up))
            {
                this.right_axis = -1;
            }
            else if (input_Keyboard.IsKeyDown(Keys.Down))
            {
                this.right_axis = 1;
            }
            else
            {
                this.right_axis = 0;
            }
        }
    }
}
