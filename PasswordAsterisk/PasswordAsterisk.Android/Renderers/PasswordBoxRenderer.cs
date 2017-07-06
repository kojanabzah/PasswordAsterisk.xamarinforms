using System.Collections.Generic;
using PasswordAsterisk.Controls;
using Xamarin.Forms;
using PasswordAsterisk.Droid.Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PasswordBox), typeof(PasswordBoxRenderer))]
namespace PasswordAsterisk.Droid.Renderers
{
    public class PasswordBoxRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.InputType = Android.Text.InputTypes.TextVariationPassword |
                          Android.Text.InputTypes.ClassText;

                Control.TransformationMethod = new HiddenPasswordTransformationMethod();
            }
        }
    }
    internal class HiddenPasswordTransformationMethod : Android.Text.Method.PasswordTransformationMethod
    {
        public override Java.Lang.ICharSequence GetTransformationFormatted(Java.Lang.ICharSequence source, Android.Views.View view)
        {
            return new PasswordCharSequence(source);
        }
    }

    internal class PasswordCharSequence : Java.Lang.Object, Java.Lang.ICharSequence
    {
        private Java.Lang.ICharSequence _source;
        public PasswordCharSequence(Java.Lang.ICharSequence source)
        {
            _source = source;
        }

        public char CharAt(int index)
        {
            return '*';
        }

        public int Length()
        {
            return _source.Length();
        }

        public Java.Lang.ICharSequence SubSequenceFormatted(int start, int end)
        {
            return _source.SubSequenceFormatted(start, end); // Return default
        }

        public IEnumerator<char> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _source.GetEnumerator();
        }
    }
}