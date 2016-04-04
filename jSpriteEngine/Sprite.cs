using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jSpriteEngine
{
    public class Sprite
    {
        private readonly List<Image> _frames = new List<Image>();

        public double SecondsPerFrame = 0.5;
        private DateTime _lastUpdateTime = DateTime.MinValue;
        public float Scale { get; set; } = 1.0f;
        public PointF Position { get; set; } = new PointF(0, 0);

        public Sprite(Image sourceImage, Rectangle frameSize)
        {
            for (var y = 0; y < sourceImage.Height; y += frameSize.Height)
            {
                for (var x = 0; x < sourceImage.Width; x += frameSize.Width)
                {
                    var img = new Bitmap(sourceImage, frameSize.Width, frameSize.Height);
                    _frames.Add(img);
                }
            }
        }

        public void Draw(Graphics g)
        {
            UpdateFrames();

            var img = _frames.First();
            var sourceRect = new RectangleF(0, 0, img.Width, img.Height);
            var destRect = new RectangleF(Position, new SizeF(img.Width * Scale, img.Height * Scale));

            g.DrawImage(_frames.First(), destRect, sourceRect, GraphicsUnit.Pixel);
        }

        private void UpdateFrames()
        {
            if (_lastUpdateTime == DateTime.MinValue)
            {
                _lastUpdateTime = DateTime.Now;
                return;
            }

            var timeSinceLastUpdate = (DateTime.Now - _lastUpdateTime).TotalSeconds;
            if (timeSinceLastUpdate < SecondsPerFrame) return;

            _lastUpdateTime = DateTime.Now;
            var img = _frames.First();
            _frames.Remove(img);
            _frames.Add(img);
        }
    }
}
