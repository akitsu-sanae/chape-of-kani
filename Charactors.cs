/*============================================================================
  Copyright (C) 2016 akitsu sanae
  https://github.com/akitsu-sanae/shape-of-kani
  Distributed under the Boost Software License, Version 1.0. (See accompanying
  file LICENSE or copy at http://www.boost.org/LICENSE_1_0.txt)
============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shape_of_kani
{
    class Charactor : asd.TextObject2D
    {
        public Charactor()
        {
            Font = Resource.Font;
            Text = Resource.Words[Resource.Rand.Next(Resource.Words.Length)].ToString();
            CenterPosition = new asd.Vector2DF(1, 1) * Resource.FontSize / 2;
        }
    }

    class Player : Charactor
    {
        protected override void OnUpdate()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.KeyState.Hold)
                speed.Y -= 0.5f;
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.KeyState.Hold)
                speed.Y += 0.5f;
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.KeyState.Hold)
                speed.X -= 0.5f;
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.KeyState.Hold)
                speed.X += 0.5f;
            if ((Position + speed).X < 0 || (Position + speed).X > asd.Engine.WindowSize.X)
                speed.X *= -0.5f;
            if ((Position + speed).Y < 0 || (Position + speed).Y > asd.Engine.WindowSize.Y)
                speed.Y *= -0.5f;
            Position += speed;

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
            {
                counter = (counter + 1) % Resource.Words.Length;
                Text = Resource.Words[counter].ToString();
            }

        }
        private asd.Vector2DF speed = new asd.Vector2DF();
        private int counter = 0;
    }
}
