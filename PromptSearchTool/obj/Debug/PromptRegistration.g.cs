﻿#pragma checksum "..\..\PromptRegistration.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D7C7F3FBED3C7CD86F775B408E4884FBCDB29BC394EAB68B5FEE407311B954DA"
//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

using PromptSearchTool;
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


namespace PromptSearchTool {
    
    
    /// <summary>
    /// PromptRegistration
    /// </summary>
    public partial class PromptRegistration : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\PromptRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox title_textBox;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\PromptRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description_textBox;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\PromptRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox type_comboBox;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\PromptRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox content_textBox;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\PromptRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox output_textBox;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\PromptRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save_button;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\PromptRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button close_button;
        
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
            System.Uri resourceLocater = new System.Uri("/PromptSearchTool;component/promptregistration.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PromptRegistration.xaml"
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
            this.title_textBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.description_textBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.type_comboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.content_textBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.output_textBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.save_button = ((System.Windows.Controls.Button)(target));
            
            #line 125 "..\..\PromptRegistration.xaml"
            this.save_button.Click += new System.Windows.RoutedEventHandler(this.btnRegistration_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.close_button = ((System.Windows.Controls.Button)(target));
            
            #line 133 "..\..\PromptRegistration.xaml"
            this.close_button.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

