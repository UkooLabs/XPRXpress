﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3053
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("XPRExpress.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;!DOCTYPE html PUBLIC &quot;-//W3C//DTD XHTML 1.1//EN&quot; &quot;http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd&quot;&gt;
        '''&lt;html xmlns=&quot;http://www.w3.org/1999/xhtml&quot;&gt;
        '''&lt;head&gt;
        '''    &lt;title&gt;About Page&lt;/title&gt;
        '''&lt;/head&gt;
        '''&lt;body&gt;
        '''    &lt;span style=&quot;font-size: 8pt; font-family: Verdana&quot;&gt;This program was written by |EqUiNoX|
        '''        as part of XBMC SkinEditor and in association with Team Blackbolt.
        '''        &lt;br /&gt;
        '''        &lt;br /&gt;
        '''        Many Thanks go to :-&lt;br /&gt;
        '''        &lt;br /&gt;
        '''        &lt;strong&gt;&lt;span style=&quot;text-decoration: underline&quot;&gt;T [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property About() As String
            Get
                Return ResourceManager.GetString("About", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property AppHeaderMain() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("AppHeaderMain", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Back() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Back", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Grid() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Grid", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;!DOCTYPE html PUBLIC &quot;-//W3C//DTD XHTML 1.1//EN&quot; &quot;http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd&quot;&gt;
        '''&lt;html xmlns=&quot;http://www.w3.org/1999/xhtml&quot; &gt;
        '''&lt;head&gt;
        '''    &lt;title&gt;Help Page&lt;/title&gt;
        '''&lt;/head&gt;
        '''&lt;body&gt;
        '''    &lt;span style=&quot;font-family: Verdana&quot;&gt;&lt;span style=&quot;font-size: 8pt&quot;&gt;&lt;strong&gt;&lt;span style=&quot;text-decoration: underline&quot;&gt;
        '''        What Is XPR Express?&lt;/span&gt;&lt;/strong&gt;&lt;br /&gt;
        '''        &lt;br /&gt;
        '''        XPR Express is a tool for extracting XBMC XPR files (non-protected) back into their
        '''        original image files or  [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property Help() As String
            Get
                Return ResourceManager.GetString("Help", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property splashscreen() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("splashscreen", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property XBMCTex() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("XBMCTex", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
    End Module
End Namespace
