using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace jSpriteEngine
{
    public class Sprite
    {
        #region Private Members
        private readonly List<RectangleF> _frames = new List<RectangleF>();
        private readonly Dictionary<string, List<RectangleF>> _frameSets = new Dictionary<string, List<RectangleF>>(); 
        private readonly Image _sourceImage;
        private double _framesPerSecond = 1 / 10f;
        private DateTime _lastUpdateTime = DateTime.MinValue;
        #endregion

        #region Public Properties
        public double Scale { get; set; } = 1.0f;
        public PointF Position { get; set; } = new PointF(0, 0);
        public string ActiveFrameset { get; set; } = "";
        public int FramesPerSecond
        {
            get { return (int)(1 / _framesPerSecond); }
            set { _framesPerSecond = 1f / value; }
        }
        #endregion

        #region Constructors
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

        public Sprite(Image sourceImage, int framesAcross, int framesDown, params SpriteFrameset[] framesets)
            :this(sourceImage, framesAcross, framesDown)
        {
            foreach (var frameset in framesets)
            {
                var frames = frameset.FrameIndicies.Select(i => _frames[i]).ToList();
                _frameSets.Add(frameset.Name, frames);
            }
        }
        #endregion

        #region Public Methods
        public void Draw(Graphics g)
        {
            UpdateFrames();

            var frame = _frameSets.ContainsKey(ActiveFrameset) ? 
                _frameSets[ActiveFrameset].First() : 
                _frames.First();

            var sourceRect = frame;
            var destRect = new RectangleF(Position, new SizeF(sourceRect.Width * (float)Scale, sourceRect.Height * (float)Scale));

            g.DrawImage(_sourceImage, destRect, sourceRect, GraphicsUnit.Pixel);
        }
        #endregion

        #region Private Methods
        private void UpdateFrames()
        {
            if (_lastUpdateTime == DateTime.MinValue)
            {
                _lastUpdateTime = DateTime.Now;
                return;
            }

            var timeSinceLastUpdate = (DateTime.Now - _lastUpdateTime).TotalSeconds;
            if (timeSinceLastUpdate < _framesPerSecond) return;

            List<RectangleF> currentFrames;
            if (string.IsNullOrWhiteSpace(ActiveFrameset) || _frameSets.ContainsKey(ActiveFrameset) == false)
            {
                // No active frameset - use full set
                currentFrames = _frames;
            }
            else
            {
                currentFrames = _frameSets[ActiveFrameset];
            }

            _lastUpdateTime = DateTime.Now;
            var frame = currentFrames.First();
            currentFrames.Remove(frame);
            currentFrames.Add(frame);
        }
        #endregion
    }

    public class SpriteFrameset
    {
        public string Name { get; set; }
        public List<int> FrameIndicies { get; set; }

        public SpriteFrameset(){}

        public SpriteFrameset(string name, params int[] frameIndicies)
        {
            Name = name;
            FrameIndicies = new List<int>(frameIndicies);
        }
    }
}
