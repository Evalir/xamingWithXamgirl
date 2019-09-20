using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CrossPaint.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CrossPaint.Views
{
    public partial class PaintPage : ContentPage
    {
        private List<SKPath> paths = new List<SKPath>();
        private Dictionary<long, SKPath> temporaryPaths = new Dictionary<long, SKPath>();
        private List<SKColor> usedColors = new List<SKColor>();
        private SKColor activeColor = new SKColor();
        private SKBitmap saveBitmap;
        private SKBitmap libBitmap;
        private Stack<SKPath> undonePaths = new Stack<SKPath>();
        private Stack<SKColor> undoneColors = new Stack<SKColor>();

        public PaintPage()
        {
            InitializeComponent();
            activeColor = SKColors.Red;
            UndoButton.IsEnabled = false;
            RedoButton.IsEnabled = false;
        }

        private void OnColorClick(object sender, EventArgs e)
        {
            activeColor = (sender as Button).BackgroundColor.ToSKColor();
            Debug.WriteLine(activeColor);
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            paths.Clear();
            usedColors.Clear();
            saveBitmap = null;
            libBitmap = null;
            canvasView.InvalidateSurface();
        }

        private void OnUndoClicked(object sender, EventArgs e)
        {
            if (paths.Count > 0)
            {
                undonePaths.Push(paths[paths.Count - 1]);
                paths.RemoveAt(paths.Count - 1);
                undoneColors.Push(usedColors[usedColors.Count - 1]);
                usedColors.RemoveAt(usedColors.Count - 1);

            }
            if (paths.Count == 0)
                UndoButton.IsEnabled = false;
            else
                UndoButton.IsEnabled = true;
            RedoButton.IsEnabled = true;
            canvasView.InvalidateSurface();
        }

        private void OnRedoClicked(object sender, EventArgs e)
        {
            if (undonePaths.Count > 0)
            {
                paths.Add(undonePaths.Pop());
                activeColor = undoneColors.Pop();
                usedColors.Add(activeColor);
            }
            if (undonePaths.Count == 0)
                RedoButton.IsEnabled = false;
            else
                RedoButton.IsEnabled = true;
            UndoButton.IsEnabled = true;
            canvasView.InvalidateSurface();
        }


        private async void OnSaveClicked(object sender, EventArgs e)
        {
            using (SKImage image = SKImage.FromBitmap(saveBitmap))
            {
                SKData data = image.Encode();
                DateTime dt = DateTime.Now;
                string filename = String.Format("FingerPaint-{0:D4}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}{6:D3}.png",
                                                dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);

                IPhotoLibrary photoLibrary = DependencyService.Get<IPhotoLibrary>();
                bool result = await photoLibrary.SavePhotoAsync(data.ToArray(), "FingerPaint", filename);

                if (!result)
                {
                    await DisplayAlert("FingerPaint", "Artwork could not be saved. Sorry!", "OK");
                }
            }

        }

        private async void OnLoadClicked(object sender, EventArgs e)
        {
            IPhotoLibrary picturePicker = DependencyService.Get<IPhotoLibrary>();

            using (Stream stream = await picturePicker.PickPhotoAsync())
            {
                if (stream != null)
                {
                    libBitmap = SKBitmap.Decode(stream);
                    canvasView.InvalidateSurface();
                }
            }
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            if (libBitmap != null)
            {
                canvas.DrawBitmap(libBitmap, info.Rect);
                //saveBitmap = libBitmap;
            }
            // Create bitmap the size of the display surface
            if (saveBitmap == null)
            {
               saveBitmap = new SKBitmap(info.Width, info.Height);
                if (libBitmap != null)
                    using (SKCanvas newCanvas = new SKCanvas(saveBitmap))
                    {
                        newCanvas.Clear();
                        newCanvas.DrawBitmap(libBitmap, info.Rect);
                    }

            }
            // Or create new bitmap for a new size of display surface
            else if (saveBitmap.Width < info.Width || saveBitmap.Height < info.Height)
            {
                SKBitmap newBitmap = new SKBitmap(Math.Max(saveBitmap.Width, info.Width),
                                                  Math.Max(saveBitmap.Height, info.Height));

                using (SKCanvas newCanvas = new SKCanvas(newBitmap))
                {
                    newCanvas.Clear();
                    newCanvas.DrawBitmap(saveBitmap, 0, 0);
                    if (libBitmap != null)
                        newCanvas.DrawBitmap(libBitmap, info.Rect);
                }

                saveBitmap = newBitmap;
            }

            var bitmapCanvas = new SKCanvas(saveBitmap);
            bitmapCanvas.Clear();
            if(libBitmap != null)
                        bitmapCanvas.DrawBitmap(libBitmap, info.Rect);
            for (var i = 0; i < paths.Count; i++)
            {
                var currentStroke = new SKPaint
                {
                    Color = usedColors[i],
                    StrokeWidth = 5,
                    Style = SKPaintStyle.Stroke
                };
                canvas.DrawPath(paths[i], currentStroke);
                bitmapCanvas.DrawPath(paths[i], currentStroke);
            }

        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    var p = new SKPath();
                    p.MoveTo(e.Location);
                    temporaryPaths[e.Id] = p;
                    break;
                case SKTouchAction.Moved:
                    if (e.InContact)
                        temporaryPaths[e.Id].LineTo(e.Location);
                    break;
                case SKTouchAction.Released:
                    paths.Add(temporaryPaths[e.Id]);
                    temporaryPaths.Remove(e.Id);
                    usedColors.Add(activeColor);
                    break;
            }
            //Touched, so all cant redo now
            while (undonePaths.Count != 0)
                undonePaths.Pop();
            while (undoneColors.Count != 0)
                undoneColors.Pop();

            RedoButton.IsEnabled = false;
            UndoButton.IsEnabled = true;

            e.Handled = true;

            // update the UI on the screen
            ((SKCanvasView)sender).InvalidateSurface();
        }



    }
}
