using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface.Presentation.Controls;
using System.Windows;
using Touchtech.Surface.Presentation.Input;

namespace Touchtech.Surface.Presentation.Controls
{
    /// <summary>
    /// A <see cref="SurfaceWindow"/> that enables Native Touch automatically if <see cref="IsOptimizationEnabled"/> is set to <c>true</c> (default).
    /// </summary>
    public class OptimizedSurfaceWindow : SurfaceWindow
    {
        #region Dependency Properties

        #region IsOptimizationEnabled

        /// <summary>
        /// Gets or sets whether optimization is enabled on this <see cref="Window"/> through the use of the Native Touch features.
        /// </summary>
        /// <remarks>
        /// Native Touch will only be enabled if Surface Input is suppressed. Please make sure to suppress Surface Input through the use
        /// of <see cref="M:Touchtech.Surface.SurfaceEnvironmentHelper.SuppressSurfaceInput"/> or one of its helper methods in the same class.
        /// </remarks>
        public bool IsOptimizationEnabled
        {
            get { return (bool)GetValue(IsOptimizationEnabledProperty); }
            set { SetValue(IsOptimizationEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsOptimizationEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsOptimizationEnabledProperty =
            DependencyProperty.Register("IsOptimizationEnabled", typeof(bool), typeof(OptimizedSurfaceWindow), new UIPropertyMetadata(true));

        #endregion

        #endregion

        /// <summary>
        /// Overridden.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (IsOptimizationEnabled &&
                SurfaceEnvironmentHelper.IsSurfaceInputSuppressed &&
                !SurfaceEnvironmentHelper.IsRunningOnSurfaceHardware)
            {
                if (!this.TryEnableNativeTouch())
                {
                    throw new InvalidOperationException(Touchtech.Surface.Presentation.Properties.Resources.FailedToEnableNativeTouchOnWindow);
                }
            }
        }
    }
}
