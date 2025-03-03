using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls
{
	/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="Type[@FullName='Microsoft.Maui.Controls.ImageButton']/Docs/*" />
	public partial class ImageButton : View, IImageController, IElementConfiguration<ImageButton>, IBorderElement, IButtonController, IViewController, IPaddingElement, IButtonElement, IImageElement
	{
		const int DefaultCornerRadius = -1;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='CommandProperty']/Docs/*" />
		public static readonly BindableProperty CommandProperty = ButtonElement.CommandProperty;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='CommandParameterProperty']/Docs/*" />
		public static readonly BindableProperty CommandParameterProperty = ButtonElement.CommandParameterProperty;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='CornerRadiusProperty']/Docs/*" />
		public static readonly BindableProperty CornerRadiusProperty = BorderElement.CornerRadiusProperty;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='BorderWidthProperty']/Docs/*" />
		public static readonly BindableProperty BorderWidthProperty = BorderElement.BorderWidthProperty;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='BorderColorProperty']/Docs/*" />
		public static readonly BindableProperty BorderColorProperty = BorderElement.BorderColorProperty;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='SourceProperty']/Docs/*" />
		public static readonly BindableProperty SourceProperty = ImageElement.SourceProperty;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='AspectProperty']/Docs/*" />
		public static readonly BindableProperty AspectProperty = ImageElement.AspectProperty;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='IsOpaqueProperty']/Docs/*" />
		public static readonly BindableProperty IsOpaqueProperty = ImageElement.IsOpaqueProperty;

		internal static readonly BindablePropertyKey IsLoadingPropertyKey = BindableProperty.CreateReadOnly(nameof(IsLoading), typeof(bool), typeof(ImageButton), default(bool));

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='IsLoadingProperty']/Docs/*" />
		public static readonly BindableProperty IsLoadingProperty = IsLoadingPropertyKey.BindableProperty;

		internal static readonly BindablePropertyKey IsPressedPropertyKey = BindableProperty.CreateReadOnly(nameof(IsPressed), typeof(bool), typeof(ImageButton), default(bool));

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='IsPressedProperty']/Docs/*" />
		public static readonly BindableProperty IsPressedProperty = IsPressedPropertyKey.BindableProperty;

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='PaddingProperty']/Docs/*" />
		public static readonly BindableProperty PaddingProperty = PaddingElement.PaddingProperty;

		public event EventHandler Clicked;
		public event EventHandler Pressed;
		public event EventHandler Released;

		readonly Lazy<PlatformConfigurationRegistry<ImageButton>> _platformConfigurationRegistry;


		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='.ctor']/Docs/*" />
		public ImageButton()
		{
			_platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<ImageButton>>(() => new PlatformConfigurationRegistry<ImageButton>(this));
		}

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='BorderColor']/Docs/*" />
		public Color BorderColor
		{
			get { return (Color)GetValue(BorderElement.BorderColorProperty); }
			set { SetValue(BorderElement.BorderColorProperty, value); }
		}

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='CornerRadius']/Docs/*" />
		public int CornerRadius
		{
			get { return (int)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='BorderWidth']/Docs/*" />
		public double BorderWidth
		{
			get { return (double)GetValue(BorderWidthProperty); }
			set { SetValue(BorderWidthProperty, value); }
		}

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='Aspect']/Docs/*" />
		public Aspect Aspect
		{
			get { return (Aspect)GetValue(AspectProperty); }
			set { SetValue(AspectProperty, value); }
		}

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='IsLoading']/Docs/*" />
		public bool IsLoading => (bool)GetValue(IsLoadingProperty);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='IsPressed']/Docs/*" />
		public bool IsPressed => (bool)GetValue(IsPressedProperty);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='IsOpaque']/Docs/*" />
		public bool IsOpaque
		{
			get { return (bool)GetValue(IsOpaqueProperty); }
			set { SetValue(IsOpaqueProperty, value); }
		}
		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='Command']/Docs/*" />
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='CommandParameter']/Docs/*" />
		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='Source']/Docs/*" />
		[System.ComponentModel.TypeConverter(typeof(ImageSourceConverter))]
		public ImageSource Source
		{
			get { return (ImageSource)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		bool IButtonElement.IsEnabledCore
		{
			set { SetValueCore(IsEnabledProperty, value); }
		}

		protected override void OnBindingContextChanged()
		{
			ImageElement.OnBindingContextChanged(this, this);
			base.OnBindingContextChanged();
		}

		protected internal override void ChangeVisualState()
		{
			if (IsEnabled && IsPressed)
			{
				VisualStateManager.GoToState(this, ButtonElement.PressedVisualState);
			}
			else
			{
				base.ChangeVisualState();
			}
		}

		protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
		{
			SizeRequest desiredSize = base.OnMeasure(double.PositiveInfinity, double.PositiveInfinity);
			return ImageElement.Measure(this, desiredSize, widthConstraint, heightConstraint);
		}

		/// <inheritdoc/>
		public IPlatformElementConfiguration<T, ImageButton> On<T>() where T : IConfigPlatform => _platformConfigurationRegistry.Value.On<T>();

		int IBorderElement.CornerRadiusDefaultValue => (int)CornerRadiusProperty.DefaultValue;

		Color IBorderElement.BorderColorDefaultValue => (Color)BorderColorProperty.DefaultValue;

		double IBorderElement.BorderWidthDefaultValue => (double)BorderWidthProperty.DefaultValue;

		void IBorderElement.OnBorderColorPropertyChanged(Color oldValue, Color newValue)
		{
		}

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='SetIsLoading']/Docs/*" />
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetIsLoading(bool isLoading) => SetValue(IsLoadingPropertyKey, isLoading);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='SetIsPressed']/Docs/*" />
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetIsPressed(bool isPressed) =>
			SetValue(IsPressedPropertyKey, isPressed);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='SendClicked']/Docs/*" />
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendClicked() =>
			ButtonElement.ElementClicked(this, this);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='SendPressed']/Docs/*" />
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendPressed() =>
			ButtonElement.ElementPressed(this, this);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='SendReleased']/Docs/*" />
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendReleased() =>
			ButtonElement.ElementReleased(this, this);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='PropagateUpClicked']/Docs/*" />
		public void PropagateUpClicked() =>
			Clicked?.Invoke(this, EventArgs.Empty);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='PropagateUpPressed']/Docs/*" />
		public void PropagateUpPressed() =>
			Pressed?.Invoke(this, EventArgs.Empty);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='PropagateUpReleased']/Docs/*" />
		public void PropagateUpReleased() =>
			Released?.Invoke(this, EventArgs.Empty);

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='RaiseImageSourcePropertyChanged']/Docs/*" />
		public void RaiseImageSourcePropertyChanged() =>
			OnPropertyChanged(nameof(Source));

		/// <include file="../../docs/Microsoft.Maui.Controls/ImageButton.xml" path="//Member[@MemberName='Padding']/Docs/*" />
		public Thickness Padding
		{
			get { return (Thickness)GetValue(PaddingElement.PaddingProperty); }
			set { SetValue(PaddingElement.PaddingProperty, value); }
		}

		Thickness IPaddingElement.PaddingDefaultValueCreator()
		{
			return default(Thickness);
		}

		void IPaddingElement.OnPaddingPropertyChanged(Thickness oldValue, Thickness newValue)
		{
			InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);
		}

		void IImageElement.OnImageSourceSourceChanged(object sender, EventArgs e) =>
			ImageElement.ImageSourceSourceChanged(this, e);

		void IButtonElement.OnCommandCanExecuteChanged(object sender, EventArgs e) =>
			ButtonElement.CommandCanExecuteChanged(this, EventArgs.Empty);

		bool IImageElement.IsAnimationPlaying
		{
			get => false;
		}

		bool IBorderElement.IsCornerRadiusSet() => IsSet(CornerRadiusProperty);
		bool IBorderElement.IsBackgroundColorSet() => IsSet(BackgroundColorProperty);
		bool IBorderElement.IsBackgroundSet() => IsSet(BackgroundProperty);
		bool IBorderElement.IsBorderColorSet() => IsSet(BorderColorProperty);
		bool IBorderElement.IsBorderWidthSet() => IsSet(BorderWidthProperty);

		bool IImageController.GetLoadAsAnimation() => false;
	}
}