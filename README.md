[![NuGet](https://img.shields.io/nuget/v/ElegantTabs.svg)](https://www.nuget.org/packages/ElegantTabs)

<img src="./nuget/ElegantTabs.png" width="100">

# ElegantTabs for Xamarin.Forms

Elegant Tabs adds some of nifty features that are missing in Xamarin Forms Tabbed Pages

-   Options to Hide / Show Title (If No title is given, option to center the icon)
-   To Defer Loading the Tab on Click (Useful in scenarios, where you don't want to show pages in Tabs, instead use some sort of popups)
-   Give selected and unselected icons seperately
-   Keep original colours of the given tab icon (remove tinting)
-   Tab Icon Clicked Event

### Note
- This Library disables the default Sliding behaviour in Android Tabs
- Removes the tinting (removing colour properties) from icons.
- This library will only work with Bottom Navigation Bar in Android and only supports Xamarin Forms Version 3.1 Above.
- This library does not support legacy NON AppCompat Main Activity.

## Supported Platforms

-   iOS
-   Android

## How to Use?

### Setup

Available on NuGet: http://www.nuget.org/packages/ElegantTabs.
Install into your PCL/NETStandard project and Client projects

You have to register the custom renderer. You canput this in the AssemblyInfo.cs file of my Platform specific project (iOS, Android) csproj.

```csharp
[assembly: ExportRenderer(typeof(TabbedPage), typeof(ElegantTabs.Droid.ElegantTabRenderer))]
```

### NOTE: Custom TabbedPage / Custom Renderers

If you are using a custom renderer for TabbedPage please change it to inherit from ElegantTabRenderer and you are all set. Of course dont forget to register your own renderer.

In Tabbed Page XAML, add

```xaml
xmlns:plugin="clr-namespace:ElegantTabs.Abstraction;assembly=ElegantTabs.Abstraction"
```

**In any tab childrens you can add the following properties**

| Property                               |  Type  |                                                                         |
| -------------------------------------- | :----: | :---------------------------------------------------------------------- |
| Icon                                   | string | Original Xamarin.Forms property. Compulsory if you want to display Icon |
| plugin:Transforms.SelectedIcon         | string | Icon file source when tab is selected                                   |
| plugin:Transforms.KeepIconColourIntact |  bool  | If true, use the original file for icon without any extra tinting.      |
| plugin:Transforms.DisableLoad          |  bool  | To avoid navigation / loading the page (suitable for popups)            |
| plugin:Transforms.HideTitle            |  bool  | To remove or ignore title, and center the icon                          |

**Events**

| Event                     | Usage                                                                                                                    |
| :------------------------ | :----------------------------------------------------------------------------------------------------------------------- |
| Transforms.TabIconClicked | Subscribe to get icon click. Can be used from anywhere in the project (including tabbed page's code behind or viewmodel) |

eg.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
            xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
            x:Class="Demo.Views.HomePage"
            xmlns:android="clr-namespace:Xamarin.Forms.PlatformConfiguration.AndroidSpecific;assembly=Xamarin.Forms.Core"
            android:TabbedPage.ToolbarPlacement="Bottom"
            android:TabbedPage.IsSwipePagingEnabled="False"
            android:TabbedPage.BarSelectedItemColor="{StaticResource AppOrangeColor}"
            xmlns:plugin="clr-namespace:ElegantTabs.Abstraction;assembly=ElegantTabs.Abstraction">

    <TabbedPage.Children>
       <ContentPage Icon="base.png" plugin:Transforms.SelectedIcon="base_selected.png" Title="Base" />
       <ContentPage Icon="quickbook.png"  plugin:Transforms.HideTitle="True" plugin:Transforms.DisableLoad="True" plugin:Transforms.KeepIconColourIntact="True"/>
       <ContentPage Icon="settings.png" plugin:Transforms.HideTitle="True" />
     </TabbedPage.Children>
</TabbedPage>
```

### DISCLAIMER
This is a prerelease, I have not tested many cases like adding tabs dynamically, changing properties from view model, etc.
I don't guarantee the performance and code quality of this library, this is an experimentation and all are welcome to send PR for improving performance or finding more simplified ways to solve this problem

Heavily inspired from [Xamarin Forms](https://github.com/xamarin/Xamarin.Forms) source code and     [xamarin-forms-tab-badge](https://github.com/xabre/xamarin-forms-tab-badge).
