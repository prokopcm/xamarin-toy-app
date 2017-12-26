using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using XamarinTestApp.Droid.Models;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace XamarinTestApp.Droid.Views
{
    public class CanvasView : View
    {
        public List<Particle> ParticleList;

        public List<Particle> RecycleList;

        public int Width { get; set; }

        public int Height { get; set; }

        private Bitmap Bitmap;

        private Canvas Canvas;

        private Context Context;

        private Paint Paint;

        private Bitmap[] ParticleImages = new Bitmap[3];

        public CanvasView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }

        public CanvasView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            ParticleList = new List<Particle>();
            RecycleList = new List<Particle>();

            ParticleImages[0] = BitmapFactory.DecodeResource(Resources, Resource.Drawable.red_particle);
            ParticleImages[1] = BitmapFactory.DecodeResource(Resources, Resource.Drawable.green_particle);
            ParticleImages[2] = BitmapFactory.DecodeResource(Resources, Resource.Drawable.blue_particle);

            this.Context = context;
            Paint = new Paint();
            Paint.AntiAlias = true;
            Paint.Color = Color.White;
        }

        public void ClearCanvas()
        {
            Invalidate();
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            Width = w;
            Height = h;
            Bitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
            Canvas = new Canvas(Bitmap);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            // TODO

            for (int i = 0; i < ParticleList.Count; i++)
            {
                Particle p = ParticleList.ElementAt(i);
                p.update();
                canvas.DrawBitmap(ParticleImages[p.Color], p.X - 10, p.Y - 10, Paint);

                // kill if out of bounds, add to recycle list for better memory usage
                // decrement i so we don't get an out of bounds error
                if (p.X <= 0 || p.Y >= Width || p.Y <= 0 || p.Y >= Height)
                {
                    RecycleList.Add(ParticleList.ElementAt(i));
                    ParticleList.RemoveAt(i);
                    i--;
                }
            }

            if (ParticleList.Count > 0)
            {
                Invalidate();
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            Particle p;
            int recycleCount = 0;

            if (RecycleList == null || ParticleList == null)
            {
                return false;
            }

            if (RecycleList.Count > 1)
            {
                recycleCount = 2;
            }
            else
            {
                recycleCount = RecycleList.Count;
            }

            for (int i = 0; i < recycleCount; i++)
            {
                p = RecycleList.ElementAt(0);
                RecycleList.RemoveAt(0);
                p.Init((int)e.GetX(), (int)e.GetY());
                ParticleList.Add(p);
            }

            for (int i = 0; i < 2 - recycleCount; i++)
            {
                ParticleList.Add(new Particle((int)e.GetX(), (int)e.GetY()));
            }

            Invalidate();
            return true;
        }
    }
}