﻿#pragma checksum "C:\Users\Henri\Documents\Projects\SocialApps\NutrientCalculator\NutrientCalculator\NutrientCalculator\Source\Pages\NewProfile.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6BF4A86130CEC3BDC69D8707BD0662DE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace NutrientCalculator.Source.Pages {
    
    
    public partial class NewProfile : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox nameTextBox;
        
        internal System.Windows.Controls.TextBlock nameLabel;
        
        internal System.Windows.Controls.Image profileImage;
        
        internal System.Windows.Controls.TextBlock profileImageLabel;
        
        internal System.Windows.Controls.Button chooseButton;
        
        internal System.Windows.Controls.RadioButton femaleRadioButton;
        
        internal System.Windows.Controls.RadioButton maleRadioButton;
        
        internal System.Windows.Controls.Button rotateImageButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/NutrientCalculator;component/Source/Pages/NewProfile.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.nameTextBox = ((System.Windows.Controls.TextBox)(this.FindName("nameTextBox")));
            this.nameLabel = ((System.Windows.Controls.TextBlock)(this.FindName("nameLabel")));
            this.profileImage = ((System.Windows.Controls.Image)(this.FindName("profileImage")));
            this.profileImageLabel = ((System.Windows.Controls.TextBlock)(this.FindName("profileImageLabel")));
            this.chooseButton = ((System.Windows.Controls.Button)(this.FindName("chooseButton")));
            this.femaleRadioButton = ((System.Windows.Controls.RadioButton)(this.FindName("femaleRadioButton")));
            this.maleRadioButton = ((System.Windows.Controls.RadioButton)(this.FindName("maleRadioButton")));
            this.rotateImageButton = ((System.Windows.Controls.Button)(this.FindName("rotateImageButton")));
        }
    }
}

