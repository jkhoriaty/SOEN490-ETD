﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A48E0464BA6750717FCB4E7D583488FF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DragAndDropRectangle {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rect;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rect2;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rect3;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock box;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock box2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DragAndDropRectangle;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.rect = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 15 "..\..\MainWindow.xaml"
            this.rect.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.rect_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 16 "..\..\MainWindow.xaml"
            this.rect.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.rect_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 17 "..\..\MainWindow.xaml"
            this.rect.MouseMove += new System.Windows.Input.MouseEventHandler(this.rect_MouseMove);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rect2 = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 27 "..\..\MainWindow.xaml"
            this.rect2.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.rect_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 28 "..\..\MainWindow.xaml"
            this.rect2.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.rect_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 29 "..\..\MainWindow.xaml"
            this.rect2.MouseMove += new System.Windows.Input.MouseEventHandler(this.rect_MouseMove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.rect3 = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 39 "..\..\MainWindow.xaml"
            this.rect3.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.rect_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 40 "..\..\MainWindow.xaml"
            this.rect3.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.rect_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 41 "..\..\MainWindow.xaml"
            this.rect3.MouseMove += new System.Windows.Input.MouseEventHandler(this.rect_MouseMove);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 45 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.box = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.box2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            
            #line 48 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Generate_rectangle);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

