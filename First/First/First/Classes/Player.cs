using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace First
{
    class Player : GameObject
    {
        private float velocity_x = 5;
		
		public int score;
        
        public bool AABBIntersect(float bx, float by, float bw, float bh) {
			return Position.X < bx+bw && Position.Y < by+bh && bx < Position.X+Texture.Width && by < Position.Y+Texture.Height;
		}

        public void Behaviour(int axis)
        {
            this.Move(new Vector2(0, velocity_x * axis));
        }
    }
}
