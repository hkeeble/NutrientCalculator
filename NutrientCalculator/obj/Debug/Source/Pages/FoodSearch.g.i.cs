﻿#pragma checksum "C:\Users\Henri\Documents\Projects\SocialApps\NutrientCalculator\NutrientCalculator\NutrientCalculator\Source\Pages\FoodSearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DBF602FFDA6657EB104BB50B19CDBF84"
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
    
    
    public partial class FoodSearch : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ListBox foodList;
        
        internal System.Windows.Controls.Button searchButton;
        
        internal System.Windows.Controls.TextBox searchTerms;
        
        internal System.Windows.Controls.Button previousPageButton;
        
        internal System.Windows.Controls.Button nextPageButton;
        
        internal System.Windows.Controls.TextBlock pageLabel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/NutrientCalculator;component/Source/Pages/FoodSearch.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.foodList = ((System.Windows.Controls.ListBox)(this.FindName("foodList")));
            this.searchButton = ((System.Windows.Controls.Button)(this.FindName("searchButton")));
            this.searchTerms = ((System.Windows.Controls.TextBox)(this.FindName("searchTerms")));
            this.previousPageButton = ((System.Windows.Controls.Button)(this.FindName("previousPageButton")));
            this.nextPageButton = ((System.Windows.Controls.Button)(this.FindName("nextPageButton")));
            this.pageLabel = ((System.Windows.Controls.TextBlock)(this.FindName("pageLabel")));
        }
    }
}

