using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watch.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnalogWatch : ContentView
    {
        #region DATA PROPERTIRES

        private SKPath  HourHandPath    { get; set; }
        private SKPath  MinuteHandPath  { get; set; }
        private SKPath  SecondHandPath  { get; set; }
        private SKPaint HandStrokePaint { get; set; }
        private SKPaint HandFillPaint   { get; set; }
        private SKPaint MinuteMarkPaint { get; set; }
        private SKPaint HourMarkPaint   { get; set; }
        private bool    ViewIsActive    { get; set; }
        private SKPaint DialCirclePaint { get; set; }

        #endregion


        #region BINDABLE PROPERTIES

        public static readonly BindableProperty TimeZoneProperty = BindableProperty.Create(
            propertyName : "TimeZone", 
            returnType   : typeof(TimeZoneInfo), 
            declaringType: typeof(AnalogWatch),
            defaultValue : TimeZoneInfo.Local);

        public TimeZoneInfo TimeZone
        {
            get => (TimeZoneInfo)this.GetValue(TimeZoneProperty);
            set => this.SetValue(TimeZoneProperty, value);
        }

        public static readonly BindableProperty ArrowsColorProperty = BindableProperty.Create(
            propertyName : "ArrowsColor", 
            returnType   : typeof(Color), 
            declaringType: typeof(AnalogWatch),
            defaultValue : Color.LightGray);

        public Color ArrowsColor
        {
            get => (Color)this.GetValue(ArrowsColorProperty);
            set => this.SetValue(ArrowsColorProperty, value);
        }

        public static readonly BindableProperty DialColorProperty = BindableProperty.Create(
            propertyName : "DialColor",
            returnType   : typeof(Color),
            declaringType: typeof(AnalogWatch),
            defaultValue : Color.LightPink);

        public Color DialColor
        {
            get => (Color)this.GetValue(DialColorProperty);
            set => this.SetValue(DialColorProperty, value);
        }

        #endregion


        #region CONSTRUCTORS & DESTRUCTOR

        public AnalogWatch ()
		{
			this.InitializeComponent    ();
            this.InitializeWatchElements();
            this.CreateTimer            ();
            this.SetViewContext         ();
        }

        ~AnalogWatch()
        {
            this.RemoveTimer();
        }

        #endregion


        #region METHODS

        private void InitializeWatchElements()
        {
            this.HourHandPath   = SKPath.ParseSvgPathData(
                "M 0 -60 C 0 -30 20 -30 5 -20 L 5 0" +
                "C 5 7.5 -5 7.5 -5 0 L -5 -20" +
                "C -20 -30 0 -30 0 -60 Z");

            this.MinuteHandPath = SKPath.ParseSvgPathData(
                "M 0 -80 C   0 -75  0 -70  2.5 -60 L  2.5   0" +
                "C   2.5 5 -2.5 5 -2.5   0 L -2.5 -60" +
                "C 0 -70  0 -75  0 -80 Z");

            this.SecondHandPath = SKPath.ParseSvgPathData(
                "M 0 10 L 0 -80");

            this.HandStrokePaint = new SKPaint
            {
                Style       = SKPaintStyle.Stroke,
                Color       = SKColors.Black,
                StrokeWidth = 2,
                StrokeCap   = SKStrokeCap.Round
            };

            this.HandFillPaint = new SKPaint
            {
                Style       = SKPaintStyle.Fill,
                Color       = this.ArrowsColor.ToSKColor()
            };

            this.MinuteMarkPaint = new SKPaint
            {
                Style       = SKPaintStyle.Stroke,
                Color       = SKColors.Black,
                StrokeWidth = 3,
                StrokeCap   = SKStrokeCap.Round,
                PathEffect  = SKPathEffect.CreateDash(new float[] { 0, 3 * 3.14f }, 0)
            };

            this.HourMarkPaint = new SKPaint
            {
                Style       = SKPaintStyle.Stroke,
                Color       = SKColors.Black,
                StrokeWidth = 6,
                StrokeCap   = SKStrokeCap.Round,
                PathEffect  = SKPathEffect.CreateDash(new float[] { 0, 15 * 3.14f }, 0)
            };

            this.DialCirclePaint = new SKPaint
            {
                Style       = SKPaintStyle.Fill,
                Color       = this.DialColor.ToSKColor(),
                StrokeWidth = 25
            };
        }
        private void CreateTimer            ()
        {
            this.ViewIsActive = true;

            Device.StartTimer(TimeSpan.FromSeconds(1f / 60), () =>
            {
                this.CanvasView.InvalidateSurface();
                return this.ViewIsActive;
            });
        }
        private void RemoveTimer            ()
        {
            this.ViewIsActive = false;
        }
        private void SetViewContext         ()
        {
            this.BindingContext = this;
        }
        private void UpdateWatchCanvasView  (SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;


            canvas.Clear();

            // Update watch arrows color.
            if (this.HandFillPaint.Color != this.ArrowsColor.ToSKColor())
            {
                this.HandFillPaint.Color = this.ArrowsColor.ToSKColor();
            }

            // Update watch dial color.
            if (this.DialCirclePaint.Color != this.DialColor.ToSKColor())
            {
                this.DialCirclePaint.Color = this.DialColor.ToSKColor();
            }

            canvas.DrawCircle(info.Width / 2f, info.Height / 2f, 100, this.DialCirclePaint);

            // Transform for 100-radius circle in center
            canvas.Translate(info.Width / 2, info.Height / 2);
            canvas.Scale(Math.Min(info.Width / 200, info.Height / 200));

            // Draw circles for hour and minute marks
            SKRect rect = new SKRect(-90, -90, 90, 90);

            canvas.DrawOval(rect, this.MinuteMarkPaint);
            canvas.DrawOval(rect, this.HourMarkPaint);

            // Get time
            DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, this.TimeZone);

            // Draw hour hand
            canvas.Save();
            canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
            canvas.DrawPath(this.HourHandPath, this.HandStrokePaint);
            canvas.DrawPath(this.HourHandPath, this.HandFillPaint);
            canvas.Restore();

            // Draw minute hand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Minute + dateTime.Second / 10f);
            canvas.DrawPath(this.MinuteHandPath, this.HandStrokePaint);
            canvas.DrawPath(this.MinuteHandPath, this.HandFillPaint);
            canvas.Restore();

            // Draw second hand
            double t = dateTime.Millisecond / 1000.0;

            if (t < 0.5)
            {
                t = 0.5 * Easing.SpringIn.Ease(t / 0.5);
            }
            else
            {
                t = 0.5 * (1 + Easing.SpringOut.Ease((t - 0.5) / 0.5));
            }

            canvas.Save();
            canvas.RotateDegrees(6 * (dateTime.Second + (float)t));
            canvas.DrawPath(this.SecondHandPath, this.HandStrokePaint);
            canvas.Restore();
        }

        #endregion


        #region EVENT HANDLERS

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            this.UpdateWatchCanvasView(args);
        }

        #endregion
    }
}