using GalaSoft.MvvmLight;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TestTask200125.Models;

namespace TestTask200125.ViewModel
{
    public class ObjectViewModel : ViewModelBase
    {
        private readonly ObjectInfo _model;
               
        public ObjectViewModel(ObjectInfo model)
        {
            _model = model; 
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                if (_model.Name != value)
                {
                    _model.Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public double? Distance
        {
            get { return _model.Distance; }
            set
            {
                if (_model.Distance != value)
                {
                    _model.Distance = value;
                    RaisePropertyChanged(nameof(Distance));
                    RaisePropertyChanged(nameof(Image));
                }
            }
        }

        public double? Angle
        {
            get { return _model.Angle; }
            set
            {
                if (_model.Angle != value)
                {
                    _model.Angle = value;
                    RaisePropertyChanged(nameof(Angle));
                    RaisePropertyChanged(nameof(Image));
                }
            }
        }

        public double? Width
        {
            get { return _model.Width; }
            set
            {
                if (_model.Width != value)
                {
                    _model.Width = value;
                    RaisePropertyChanged(nameof(Width));
                    RaisePropertyChanged(nameof(Image));
                }
            }
        }

        public double? Hegth
        {
            get { return _model.Hegth; }
            set
            {
                if (_model.Hegth != value)
                {
                    _model.Hegth = value;
                    RaisePropertyChanged(nameof(Hegth));
                    RaisePropertyChanged(nameof(Image));
                }
            }
        }

        public bool? IsDefect
        {
            get { return _model.IsDefect; }
            set
            {
                if (_model.IsDefect != value)
                {
                    _model.IsDefect = value;
                    RaisePropertyChanged(nameof(IsDefect));
                    RaisePropertyChanged(nameof(IsDefectInfo));
                    RaisePropertyChanged(nameof(Image));
                }
            }
        }

        public ImageSource Image
        {
            get { return DrawImage(); }
        }

        public string IsDefectInfo
        {
            get { return _model.IsDefect.HasValue ? _model.IsDefect.Value ? "yes" : "no" : null; }
        }

        private BitmapSource DrawImage()
        {
            if (!_model.Distance.HasValue || !_model.Angle.HasValue || !_model.Width.HasValue || !_model.Hegth.HasValue)
                return null;

            const int totalWidth = 20;
            const int totalHeight = 12;
            const int scaleFactor = 10;

            //double x = _model.Distance * Math.Cos(_model.Angle);
            Rect rect = new Rect(_model.Distance.Value * scaleFactor,
                _model.Angle.Value * scaleFactor,
                _model.Width.Value * scaleFactor,
                _model.Hegth.Value * scaleFactor
                );
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext ctx = visual.RenderOpen())
            {
                ctx.DrawRectangle(Brushes.Black, new Pen(), rect);
            }
            RenderTargetBitmap bitmap = new RenderTargetBitmap(totalWidth * scaleFactor, totalHeight * scaleFactor, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }
    }
}