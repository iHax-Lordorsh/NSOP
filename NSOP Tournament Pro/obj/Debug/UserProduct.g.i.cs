﻿#pragma checksum "..\..\UserProduct.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7A2E966C8F1C9B5F4195C7B60564084AAAB7A3E1BC102B598A2A969C3B70C9DE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NSOP_Tournament_Pro;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace NSOP_Tournament_Pro {
    
    
    /// <summary>
    /// UserProduct
    /// </summary>
    public partial class UserProduct : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Info;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Qty;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Buy;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Module;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border brdPicture;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_Description;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl__Cost_Info;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl__Cost_Cost;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl__Cost_Value;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl__Cost_Text;
        
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
            System.Uri resourceLocater = new System.Uri("/NSOP Tournament Pro;component/userproduct.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserProduct.xaml"
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
            this.Btn_Info = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\UserProduct.xaml"
            this.Btn_Info.Click += new System.Windows.RoutedEventHandler(this.Info_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_Qty = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\UserProduct.xaml"
            this.btn_Qty.Click += new System.Windows.RoutedEventHandler(this.Buy_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Btn_Buy = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\UserProduct.xaml"
            this.Btn_Buy.Click += new System.Windows.RoutedEventHandler(this.Buy_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_Module = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.brdPicture = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.lbl_Description = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lbl__Cost_Info = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.lbl__Cost_Cost = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.lbl__Cost_Value = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.lbl__Cost_Text = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

