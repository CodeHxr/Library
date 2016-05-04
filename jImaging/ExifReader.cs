using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jImaging
{
    public class ExifReader : IDisposable
    {
        public Image Image { get; set; }
        private PropertyItem[] props;

        public ExifReader(Image image)
        {
            Image = image;
            props = image.PropertyItems;
        }

        public ExifReader(string fileName):this(Image.FromFile(fileName))
        {}

        public object this[int id]
        {
            get
            {
                try
                {
                    var tagName = (ExifTagName)id;
                    return this[tagName];
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public object this[ExifTagName tagId]
        {
            get
            {
                try
                {
                    var property = props.FirstOrDefault(p => p.Id.ToString().Equals(((int) tagId).ToString()));
                    return ExifTagValue.GetValueObject(property);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Dictionary<string, object> AllData()
        {
            var allData = new Dictionary<string, object>();

            foreach (ExifTagName tagName in Enum.GetValues(typeof (ExifTagName)))
            {
                var data = this[tagName];
                if(data != null)
                    allData.Add(tagName.ToString(), data);
            }

            return allData;
        }

        public void Dispose()
        {
            Image?.Dispose();
        }
    }
}
