using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinTestApp.Droid.Models
{
    public class Particle
    {
        public int DistFromOrigin { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Color { get; set; }

        private double Direction;

        private double DirectionCosine;

        private double DirectionSine;

        private int InitX;

        private int InitY;

        private static int NUM_OF_DIRECTIONS = 400;

        public Particle(int x, int y)
        {
            Init(x, y);
            Direction = 2 * Math.PI * new Random().Next(NUM_OF_DIRECTIONS) / NUM_OF_DIRECTIONS;
            DirectionSine = Math.Sin(Direction);
            DirectionCosine = Math.Cos(Direction);
            Color = new Random().Next(3);
        }

        public void Init(int x, int y)
        {
            InitX = x;
            InitY = y;
            DistFromOrigin = 0;
        }

        public void update()
        {
            DistFromOrigin += 2;
            X = (int)(InitX + (DistFromOrigin * DirectionCosine));
            Y = (int)(InitY + (DistFromOrigin * DirectionSine));
        }
    }
}