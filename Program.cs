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
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            asd.Engine.Initialize("蟹の形", 640, 480, new asd.EngineOption());
            Resource.Init();
            asd.Engine.ChangeScene(new Title());

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }
    }
}
