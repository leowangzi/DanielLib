using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Diagnostics;

namespace AmazingWPFControls.HandWritingToText
{

    /// <summary>
    /// This control is abble to recognize hand written text 
    /// on its surface and to raise an event which gives the written text.
    /// </summary>
    /// 
    [TemplatePart(Name = "PART_theInkCanvas", Type = typeof(InkCanvas))]
    public class HandWritingToText : Control, IDisposable
    {
        static HandWritingToText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HandWritingToText), new FrameworkPropertyMetadata(typeof(HandWritingToText)));
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InkedTextRecognizer"/> class.
        /// </summary>
        public HandWritingToText()
        {
            //Don't forget to dispose the unmanaged ressources
            Application.Current.Exit += new ExitEventHandler(Current_Exit);

            //Create the ink analyzer
            theInkAnalyzer = new InkAnalyzer();
            theInkAnalyzer.AnalysisModes = AnalysisModes.StrokeCacheAutoCleanupEnabled | AnalysisModes.AutomaticReconciliationEnabled;
            theInkAnalyzer.ResultsUpdated += new ResultsUpdatedEventHandler(theInkAnalyzer_ResultsUpdated);
            var hint = theInkAnalyzer.CreateAnalysisHint();
            hint.Location.MakeInfinite();

            hint.WordMode = true;
            hint.CoerceToFactoid = true;

        }

        #region Fields
        private bool _isDisposed = false;
        private Object _locker = new object();
        DateTime lastResetOfInkCanvas = DateTime.MinValue;
        InkAnalyzer theInkAnalyzer;
        InkCanvas theInkCanvas;
        #endregion


        #region LastRecognizedWord

        /// <summary>
        /// LastRecognizedWord Dependency Property
        /// </summary>
        public static readonly DependencyProperty LastRecognizedWordProperty =
            DependencyProperty.Register("LastRecognizedWord", typeof(String), typeof(HandWritingToText),
                new FrameworkPropertyMetadata((String)String.Empty));

        /// <summary>
        /// Gets or sets the LastRecognizedWord property. This dependency property 
        /// indicates the last recognized word.
        /// </summary>
        public String LastRecognizedWord
        {
            get { return (String)GetValue(LastRecognizedWordProperty); }
            set { SetValue(LastRecognizedWordProperty, value); }
        }

        #endregion



        #region StrokeColor

        /// <summary>
        /// StrokeColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.Register("StrokeColor", typeof(Color), typeof(HandWritingToText),
                new FrameworkPropertyMetadata((Color)Colors.Black,
                    new PropertyChangedCallback(OnStrokeColorChanged)));

        /// <summary>
        /// Gets or sets the StrokeColor property. This dependency property 
        /// indicates the color of the strokes.
        /// </summary>
        public Color StrokeColor
        {
            get { return (Color)GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

        /// <summary>
        /// Handles changes to the StrokeColor property.
        /// </summary>
        private static void OnStrokeColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HandWritingToText target = (HandWritingToText)d;
            Color oldStrokeColor = (Color)e.OldValue;
            Color newStrokeColor = target.StrokeColor;
            target.OnStrokeColorChanged(oldStrokeColor, newStrokeColor);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the StrokeColor property.
        /// </summary>
        protected virtual void OnStrokeColorChanged(Color oldStrokeColor, Color newStrokeColor)
        {
            if (theInkCanvas != null && theInkCanvas.DefaultDrawingAttributes != null)
            {
                theInkCanvas.DefaultDrawingAttributes.Color = newStrokeColor;
            }
        }

        #endregion





        #region TimeToEnterText

        /// <summary>
        /// TimeToEnterText Dependency Property
        /// </summary>
        public static readonly DependencyProperty TimeToEnterTextProperty =
            DependencyProperty.Register("TimeToEnterText", typeof(int), typeof(HandWritingToText),
                new FrameworkPropertyMetadata((int)10000));

        /// <summary>
        /// Gets or sets the TimeToEnterText property. This dependency property 
        /// indicates the time in miliseconds to enter the words.
        /// </summary>
        public int TimeToEnterText
        {
            get { return (int)GetValue(TimeToEnterTextProperty); }
            set { SetValue(TimeToEnterTextProperty, value); }
        }

        #endregion

        /// <summary>
        /// Create a custom routed event by first registering a RoutedEventID
        /// This event uses the bubbling routing strategy
        /// </summary>
        public static readonly RoutedEvent TextEnteredEvent = EventManager.RegisterRoutedEvent(
            "TextEntered", RoutingStrategy.Bubble, typeof(TextEnteredEventHandler), typeof(HandWritingToText));

        /// <summary>
        /// Occurs when [text entered].
        /// </summary>
        public event TextEnteredEventHandler TextEntered
        {
            add { AddHandler(TextEnteredEvent, value); }
            remove { RemoveHandler(TextEnteredEvent, value); }
        }

        // This method raises the Tap event
        void RaiseTextEnteredEvent(String textEntered)
        {
            TextEnteredEventArgs newEventArgs = new TextEnteredEventArgs(HandWritingToText.TextEnteredEvent, textEntered);
            RaiseEvent(newEventArgs);
        }

        /// <summary>
        /// The TextEnteredEventHandler
        /// </summary>
        public delegate void TextEnteredEventHandler(object sender, TextEnteredEventArgs e);





        void Current_Exit(object sender, ExitEventArgs e)
        {
            ClearAllRessources();
        }

        /// <summary>
        /// Clears all ressources : abort the current analyses, call dispose , ...
        /// </summary>
        private void ClearAllRessources()
        {
            lock (_locker)
            {
                if (!_isDisposed)
                {
                    theInkAnalyzer.Abort();
                    theInkAnalyzer.Dispose();
                    _isDisposed = true;
                }
            }
        }


        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            theInkCanvas = base.Template.FindName("PART_theInkCanvas", this) as InkCanvas;
            if (theInkCanvas == null) throw new ArgumentException("Cannot find the PART_theInkCanvas InkCanvas.");

            theInkCanvas.StrokeCollected += new InkCanvasStrokeCollectedEventHandler(theInkCanvas_StrokeCollected);
            theInkCanvas.DefaultDrawingAttributes.Color = StrokeColor;
        }

        void theInkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            //Check if the time has passes before reset
            if (DateTime.Now - lastResetOfInkCanvas > TimeSpan.FromMilliseconds(TimeToEnterText))
            {

                try
                {
                    theInkAnalyzer.RemoveStrokes(theInkCanvas.Strokes);
                }
                catch (Exception)
                {
                    Debug.WriteLine(e);
                    throw;

                }
                //Remove old strokes
                this.theInkCanvas.Strokes.Clear();
                //Add the last one added
                theInkCanvas.Strokes.Add(e.Stroke);
                lastResetOfInkCanvas = DateTime.Now;
            }
            theInkAnalyzer.AddStroke(e.Stroke);
            theInkAnalyzer.SetStrokeType(e.Stroke, StrokeType.Writing);
            theInkAnalyzer.SetStrokeLanguageId(e.Stroke, 0x09);//Use EN-US languageID

            //Launch the analysis
            theInkAnalyzer.BackgroundAnalyze();
        }



        private void InkCanvas_StrokeErasing(object sender, InkCanvasStrokeErasingEventArgs e)
        {
            theInkAnalyzer.RemoveStroke(e.Stroke);
        }


        void theInkAnalyzer_ResultsUpdated(object sender, ResultsUpdatedEventArgs e)
        {

            if (e.Status.Successful)
            {
                ContextNodeCollection nodes = ((InkAnalyzer)sender).FindLeafNodes();
                foreach (ContextNode node in nodes)
                {
                    if (node is InkWordNode)
                    {
                        InkWordNode t = node as InkWordNode;

                        string recognizedString = t.GetRecognizedString();
                        RaiseTextEnteredEvent(recognizedString);
                        LastRecognizedWord = recognizedString;

                    }
                    //For demonstration purpose only
                    else if (node is InkDrawingNode)
                    {
                        InkDrawingNode d = node as InkDrawingNode;
                        Shape shape = d.GetShape();
                        //Shape may be null here...

                    }
                }
            }
        }



        public void Dispose()
        {
            ClearAllRessources();
        }
    }

}
