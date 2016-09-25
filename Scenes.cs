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
    class Title : asd.Scene
    {
        public Title()
        {
            var title = new asd.TextObject2D();
            title.Font = Resource.Font;
            title.Text = "蟹の形";
            title.Position = new asd.Vector2DF(100, 100);
            layer.AddObject(title);

            var label = new asd.TextObject2D();
            label.Font = Resource.Font;
            label.Text = "Press Z to Start!!";
            label.Position = new asd.Vector2DF(200, 200);
            layer.AddObject(label);

            AddLayer(layer);
        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
                asd.Engine.ChangeScene(new Game());
        }
        private asd.Layer2D layer = new asd.Layer2D();
    }

    class Game : asd.Scene
    {
        public Game()
        {
            for (var i=0; i<50; i++)
            {
                var target = new Charactor();
                target.Position = new asd.Vector2DF(
                    (float)Resource.Rand.NextDouble() * asd.Engine.WindowSize.X,
                    (float)Resource.Rand.NextDouble() * asd.Engine.WindowSize.Y);
                targets.Add(target);
                gameLayer.AddObject(target);
            }
            gameLayer.AddObject(player);

            AddLayer(gameLayer);

            scoreLabel.Font = Resource.SmallFont;
            scoreLabel.Text = "Score: ";
            scoreLayer.AddObject(scoreLabel);
            AddLayer(scoreLayer);

            stopWatch.Start();
        }

        protected override void OnUpdated()
        {
            foreach (var target in targets)
            {
                if (target.Text == player.Text && (target.Position - player.Position).Length < 32)
                {
                    stopWatch.Stop();
                    score += 100000 / (stopWatch.ElapsedMilliseconds == 0 ? 1 : stopWatch.ElapsedMilliseconds);
                    scoreLabel.Text = "Score: " + score.ToString();
                    stopWatch.Restart();
                    target.Dispose();
                }
            }
            targets.RemoveAll(t => !t.IsAlive);

            if (targets.Count == 0)
                asd.Engine.ChangeScene(new Clear(score));
        }
        Player player = new Player();
        List<Charactor> targets = new List<Charactor>();
        private asd.Layer2D gameLayer = new asd.Layer2D();
        private asd.Layer2D scoreLayer = new asd.Layer2D();
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        private asd.TextObject2D scoreLabel = new asd.TextObject2D();
        long score = 0;
    }

    class Clear : asd.Scene
    {
        public Clear(long score)
        {
            List<long> scores = new List<long>();
            scores.Add(score);
            using (var reader = new System.IO.StreamReader("score.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    scores.Add(int.Parse(line));
            }
            scores.Sort();
            scores.Reverse();
            scores = scores.Take(10).ToList();
            System.IO.File.WriteAllLines("score.txt", scores.Select(s => s.ToString()));

            var rank = scores.IndexOf(score);

            var layer = new asd.Layer2D();
            if (rank != -1)
            {
                var new_record_label = new asd.TextObject2D();
                new_record_label.Text = "New Record!!!";
                new_record_label.Font = Resource.SmallFont;
                new_record_label.Position = new asd.Vector2DF(100, 50);
                layer.AddObject(new_record_label);
            }

            for (int i = 0; i < 10; i++)
            {
                var score_label = new asd.TextObject2D();
                score_label.Text = scores[i].ToString();
                score_label.Font = Resource.SmallFont;
                score_label.Position = new asd.Vector2DF(100, 100 + Resource.FontSize / 2 * i);
                if (i == rank)
                    score_label.Color = new asd.Color(255, 255, 0);
                layer.AddObject(score_label);
            }
            AddLayer(layer);
        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
                asd.Engine.ChangeScene(new Title());
        }
    }
}
