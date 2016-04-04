using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace jSpriteEngine
{
    public class Sprite
    {
        private readonly List<RectangleF> _frames = new List<RectangleF>();
        private readonly Image _sourceImage;
        //public double FramesPerSecond { get; set; } = 1 / 30f; // 1 / [x] frames per second
        private DateTime _lastUpdateTime = DateTime.MinValue;
        public float Scale { get; set; } = 1.0f;
        public PointF Position { get; set; } = new PointF(0, 0);

        private double _framesPerSecond = 1 / 10f;
        public int FramesPerSecond
        {
            get { return (int)(1 / _framesPerSecond); }
            set { _framesPerSecond = 1f / value; }
        }

        public Sprite(Image sourceImage, Size frameSize)
        {
            for (var y = 0; y < sourceImage.Height; y += frameSize.Height)
            {
                for (var x = 0; x < sourceImage.Width; x += frameSize.Width)
                {
                    var frame = new RectangleF(x, y, frameSize.Width, frameSize.Height);
                    _frames.Add(frame);
                }
            }
            _sourceImage = sourceImage;
        }

        public Sprite(Image sourceImage, int framesAcross, int framesDown)
            : this(sourceImage, new Size(sourceImage.Width / framesAcross, sourceImage.Height / framesDown))
        { }

        public void Draw(Graphics g)
        {
            UpdateFrames();

            //g.Clear(Color.Transparent);

            var sourceRect = _frames.First();
            var destRect = new RectangleF(Position, new SizeF(sourceRect.Width * Scale, sourceRect.Height * Scale));

            g.DrawImage(_sourceImage, destRect, sourceRect, GraphicsUnit.Pixel);
        }

        private void UpdateFrames()
        {
            if (_lastUpdateTime == DateTime.MinValue)
            {
                _lastUpdateTime = DateTime.Now;
                return;
            }

            var timeSinceLastUpdate = (DateTime.Now - _lastUpdateTime).TotalSeconds;
            if (timeSinceLastUpdate < _framesPerSecond) return;

            _lastUpdateTime = DateTime.Now;
            var frame = _frames.First();
            _frames.Remove(frame);
            _frames.Add(frame);
        }
    }
}
