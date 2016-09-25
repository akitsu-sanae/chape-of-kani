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
    static class Resource
    {
        static public Random Rand { get; set; } = new Random();
        static public asd.Font Font { get; private set; } = null;
        static public asd.Font SmallFont { get; private set; } = null;
        public const string Words = "蟹聲贄贅贊聱聳";
        public const int FontSize = 48;

        static public void Init()
        {
            Font = asd.Engine.Graphics.CreateDynamicFont("", FontSize, new asd.Color(255, 255, 255), 0, new asd.Color());
            SmallFont = asd.Engine.Graphics.CreateDynamicFont("", FontSize / 2, new asd.Color(255, 255, 255), 0, new asd.Color());
        }
    }
}
